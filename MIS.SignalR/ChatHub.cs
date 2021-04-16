using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace MIS.SignalR
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public ChatHub(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public override Task OnConnectedAsync()
        {
            var connectedUsersManager = (IManageConnectedUsers)httpContextAccessor.HttpContext.RequestServices.
                                        GetService(typeof(IManageConnectedUsers));
            connectedUsersManager.UserConnected();
            int connected = connectedUsersManager.GetConnectedUsers();
            
            return Clients.All.SendAsync("UserConnected" , connected);
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectedUsersManager = (IManageConnectedUsers)httpContextAccessor.HttpContext.RequestServices.
                                        GetService(typeof(IManageConnectedUsers));
            connectedUsersManager.UserDisconnected();
            int connected = connectedUsersManager.GetConnectedUsers();
            return Clients.All.SendAsync("UserDisconnected" , connected);
        }
    }
}
