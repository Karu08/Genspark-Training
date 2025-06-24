using Microsoft.AspNetCore.SignalR;

namespace OnlineGroceryPortal.Services.Misc
{
    public class OrderHub : Hub
    {
        public async Task SendOrderStatusUpdate(long orderId, string status)
        {
            await Clients.All.SendAsync("ReceiveOrderStatusUpdate", orderId, status);
        }
    }
}
