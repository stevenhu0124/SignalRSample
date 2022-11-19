using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Threading.Tasks;
using SignalRServer.Helpers;
using Microsoft.AspNet.SignalR.Messaging;

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
        public void ClientRequest(string message)
        {
            SignalHelper.Instance.ReceivedFromClient(Context.ConnectionId, message);

            // Client need to handle the method of Broadcast(string message) to receive the message from server 
            Clients.All.NotifyClient(message);

            SignalHelper.Instance.BroadcastToAllClients(message);
        }

        public override Task OnConnected()
        {
            SignalHelper.Instance.ClientConnected(Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool ss)
        {
            return base.OnDisconnected(ss);
        }
    }
}
