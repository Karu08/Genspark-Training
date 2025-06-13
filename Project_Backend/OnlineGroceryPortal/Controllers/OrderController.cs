using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using OnlineGroceryPortal.Services.Misc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IHubContext<OrderHub> _hubContext;

    public OrderController(IOrderService service, IHubContext<OrderHub> hubContext)
    {
        _service = service;
        _hubContext = hubContext;
    }

    //[Authorize(Roles = "Customer")]
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] OrderDto dto)
    {
        if (dto.CustomerId == Guid.Empty)
            return BadRequest("Customer ID is required.");

        var result = await _service.PlaceOrderAsync(dto.CustomerId, dto);

        await _hubContext.Clients.All.SendAsync("OrderPlaced", new
        {
            OrderId = result.Id,
            Status = "Pending",
            Time = DateTime.Now
        });

        return Ok(result);
    }


    [HttpGet("status/{orderId}")]
    [SwaggerOperation(Summary = "Track order status", Description = "Returns the current status of the specified order.")]
    public async Task<IActionResult> TrackOrderStatus(Guid orderId)
    {
        var status = await _service.GetOrderStatusAsync(orderId);

        if (status == null)
            return NotFound("Order not found.");

        return Ok(new { OrderId = orderId, Status = status });
    }

    //[Authorize(Roles = "Agent")]
    [HttpPut("status/{orderId}")]
    [SwaggerOperation(Summary = "Update order status", Description = "Allows delivery agent to update the order status.")]
    public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] UpdateOrderStatusDto dto)
    {
        var role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
        var nameId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        Console.WriteLine($"[DEBUG] Authenticated user ID: {nameId}, Role: {role}");

        var result = await _service.UpdateOrderStatusAsync(orderId, dto.Status);

        if (!result)
            return NotFound("Order not found.");

        await _hubContext.Clients.All.SendAsync("ReceiveOrderStatusUpdate", orderId, dto.Status);

        return Ok("Order status updated.");
    }

}
