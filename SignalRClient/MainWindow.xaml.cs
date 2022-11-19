using Microsoft.AspNet.SignalR.Client;
using System.Net;
using System.Net.Http;
using System.Windows;

namespace SignalRClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IHubProxy HubProxy { get; set; }

        private const string SERVER_URI = "http://localhost:8888";
        public HubConnection hubConnection { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            ConnectAsync();
        }

        private async void ConnectAsync()
        {
            StatusText.Content = "Connecting to server...";
            hubConnection = new HubConnection(SERVER_URI);
            hubConnection.Closed += Connection_Closed;
            HubProxy = hubConnection.CreateHubProxy("ConnectionHub");
            HubProxy.On<string>("NotifyClient", message =>
                Dispatcher.Invoke(() =>
                    OutputRichTextBox.AppendText(string.Format("{0}: {1}\r", "Received from Server", message))
                )
            );
            try
            {
                await hubConnection.Start();
            }
            catch (HttpRequestException)
            {
                StatusText.Content = string.Format("Unable to connect to server {0}: Start server before connecting clients.", SERVER_URI);
                ReconnectedBtn.IsEnabled = true;
                return;
            }

           
            ButtonSend.IsEnabled = true;
            TextBoxMessage.Focus();
            StatusText.Content = "Connected to server at " + SERVER_URI;
        }


        void Connection_Closed()
        {
            var dispatcher = Application.Current.Dispatcher;
            dispatcher.Invoke(() => ButtonSend.IsEnabled = false);
            dispatcher.Invoke(() => StatusText.Content = "You have been disconnected.");
            dispatcher.Invoke(() => ReconnectedBtn.IsEnabled = false);
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("ClientRequest", TextBoxMessage.Text);
            TextBoxMessage.Text = string.Empty;
            TextBoxMessage.Focus();
        }

        private void Reconnected_Click(object sender, RoutedEventArgs e)
        {
            ReconnectedBtn.IsEnabled = false;
            ConnectAsync();
        }
    }
}
