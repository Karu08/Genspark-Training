using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using OnlineGroceryPortal.Models.DTOs;
using OnlineGroceryPortal.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using OnlineGroceryPortal.Services.Misc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OnlineGroceryPortal.Controllers
{
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

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto dto)
        {
            if (dto == null || dto.CustomerId == 0 || dto.Items == null || !dto.Items.Any())
                return BadRequest("Invalid order data. Please provide customerId and at least one order item.");

            var result = await _service.PlaceOrderAsync(dto.CustomerId, dto);

            if (result == null)
                return BadRequest("Order could not be placed. Possibly due to insufficient stock or invalid product.");

            return Ok(result);
        }


        [HttpGet("status/{orderId:long}")]
        [SwaggerOperation(Summary = "Track order status", Description = "Returns the current status of the specified order.")]
        public async Task<IActionResult> TrackOrderStatus(long orderId)
        {
            var status = await _service.GetOrderStatusAsync(orderId);

            if (status == null)
                return NotFound("Order not found.");

            return Ok(new { OrderId = orderId, Status = status });
        }

        [Authorize(Roles = "Agent")]
        [HttpPut("status/{orderId:long}")]
        [SwaggerOperation(Summary = "Update order status", Description = "Allows delivery agent to update the order status.")]
        public async Task<IActionResult> UpdateOrderStatus(long orderId, [FromBody] UpdateOrderStatusDto dto)
        {
            var role = User.FindFirst("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
            var nameId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Console.WriteLine($"[DEBUG] Authenticated user ID: {nameId}, Role: {role}");

            var result = await _service.UpdateOrderStatusAsync(orderId, dto.Status);

            if (!result)
                return NotFound("Order not found.");

            await _hubContext.Clients.All.SendAsync("ReceiveOrderStatusUpdate", orderId, dto.Status);

            return Ok("Order status updated.");
        }
    }
}
