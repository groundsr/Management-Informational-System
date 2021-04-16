using System;
using System.Collections.Generic;
using System.Text;

namespace MIS.SignalR
{
    public interface IManageConnectedUsers
    {
        int GetConnectedUsers();
        void UserConnected();
        void UserDisconnected();
          
    }
}
