using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MIS.SignalR
{
    public class ChatHub : Hub
    {
     
        public async Task AfterConnected()
        {
            await Clients.All.SendAsync("UserConnected");
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return Clients.All.SendAsync("UserDisconnected");
        }
    }
}
