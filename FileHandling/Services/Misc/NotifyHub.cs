using Microsoft.AspNetCore.SignalR;

public class NotifyHub : Hub
{
    public async Task NotifyNewDocument(string fileName)
    {
        await Clients.All.SendAsync("NewDocument", fileName);
    }
}
