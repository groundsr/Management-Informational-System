using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.SignalR
{
    public class ManageConnectedUsers : IManageConnectedUsers
    {
        private int connectedUsers = 0;
        public int GetConnectedUsers()
        {
            return connectedUsers;
        }

        public void UserConnected()
        {
            connectedUsers++;
        }

        public void UserDisconnected()
        {
            connectedUsers--;
        }
    }
}
