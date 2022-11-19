using Microsoft.Owin.Hosting;
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
            if (!(RichTextBoxConsole.CheckAccess()))
            {
                this.Dispatcher.Invoke(() =>
                    WriteToConsole(message)
                );
                return;
            }
            RichTextBoxConsole.AppendText(message + "\r");
        }
    }
}
