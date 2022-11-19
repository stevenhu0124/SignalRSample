using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.Owin.Hosting;
using SignalRServer.Helpers;
using SignalRServer.Hubs;
using System.Reflection;
using System.Windows;


namespace SignalRServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SERVER_URI = "http://localhost:8888";
        

        public MainWindow()
        {
            InitializeComponent();
            StartServer();

            SignalHelper.Instance.ServerReceived += Received_ClientRequest;
            SignalHelper.Instance.ServerConnected += Received_ClientConnect;
            SignalHelper.Instance.ServerBroadcast += Received_ServerBroadcast;
        }

        private void Received_ServerBroadcast(string message)
        {
            WriteToConsole(string.Format("Broadcast all client: {0}", message));
        }

        private void Received_ClientConnect(string connectedStatus, string clinetID)
        {
            WriteToConsole(string.Format("Client <{0}> is {1}", clinetID, connectedStatus));
        }

        private void Received_ClientRequest(string clientID, string message)
        {
            WriteToConsole(string.Format("Received form Client: <{0}>, message: {1}", clientID, message));
        }

        private void StartServer()
        {
            try
            {
                WebApp.Start<Startup>(SERVER_URI);
                WriteToConsole("Server started at " + SERVER_URI);
            }
            catch (TargetInvocationException)
            {
                WriteToConsole("A server is already running at " + SERVER_URI);
                return;
            }       
        }

        private void WriteToConsole(string message)
        {
            if (!(OutputRichTextBox.CheckAccess()))
            {
                this.Dispatcher.Invoke(() =>
                    WriteToConsole(message)
                );
                return;
            }
            OutputRichTextBox.AppendText(message + "\r");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            SignalHelper.Instance.ServerReceived -= Received_ClientRequest;
            SignalHelper.Instance.ServerConnected -= Received_ClientConnect;
        }

        private void BroadcastBtn_Click(object sender, RoutedEventArgs e)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ConnectionHub>();
            context.Clients.All.NotifyClient(TextBoxMessage.Text);

            WriteToConsole("Broadcast button click to all client");

            TextBoxMessage.Text = string.Empty;
            TextBoxMessage.Focus();
        }
    }
}
