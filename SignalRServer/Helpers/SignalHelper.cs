
using System;

namespace SignalRServer.Helpers
{
    public sealed class SignalHelper
    {
        private static readonly Lazy<SignalHelper> lazy = new Lazy<SignalHelper>(() => new SignalHelper());

        public static SignalHelper Instance { get { return lazy.Value; } }

        private SignalHelper() { }

        public event Action<string, string> ServerReceived;

        public event Action<string, string> ServerConnected;

        public event Action<string> ServerBroadcast;


        public void ReceivedFromClient(string clientID, string message)
        {
            ServerReceived?.Invoke(clientID, message);
        }

        public void ClientConnected(string connected, string clientID)
        {
            ServerConnected?.Invoke(connected, clientID);
        }

        public void BroadcastToAllClients(string message) 
        {
            ServerBroadcast?.Invoke(message);
        }
    }
}
