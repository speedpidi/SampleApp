namespace SampleApp.Desktop
{
    using System.Linq;
    using System.Windows;
    using CommunityToolkit.Mvvm.Messaging;
    using SampleApp.Logic.Messages;

    public class MessageListener
    {
        private static MainWindow MainWindow => Application.Current.Windows.OfType<MainWindow>().First();

        public MessageListener()
        {
            InitMessenger();
        }

        private void InitMessenger()
        {
            WeakReferenceMessenger.Default.Register<CloseApplicationMessage>(this, (r, m) =>
            {
                Application.Current.Shutdown();
            });
            WeakReferenceMessenger.Default.Register<ShowProgressControlMessage>(this, (r, m) =>
            {
                MainWindow.MainContent.Content = new ProgressContentControl();
            });
            WeakReferenceMessenger.Default.Register<ShowHomeControlMessage>(this, (r, m) =>
            {
                MainWindow.MainContent.Content = new HomeContentControl();
            });
        }

        public static bool IsValid => true;
    }
}