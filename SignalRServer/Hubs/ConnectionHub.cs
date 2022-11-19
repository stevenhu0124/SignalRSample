using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRServer.Hubs
{
    [HubName("ConnectionHub")]
    public class ConnectionHub : Hub
    {
        private static readonly Lazy<ConnectionHub> lazy = new Lazy<ConnectionHub>(() => new ConnectionHub());
        public static ConnectionHub Instance { get { return lazy.Value; } }

        public ConnectionHub()
        {
        }

        /// <summary>
        /// Client will call this method to notify message.
        /// </summary>
        /// <param name="message"></param>
        public void NotifyServer(string message)
        {
            // Client need to handle the method of Broadcast(string message) to receive the message from server 
            Clients.All.Broadcast(message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool ss)
        {
            return base.OnDisconnected(ss);
        }
    }
}
