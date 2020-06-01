using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace BUSK
{
    public partial class SplashScreen : Window, ISplashScreen
    {
        public Dispatcher AppDispatcher { get; set; }

        public SplashScreen()
        {
            InitializeComponent();
            this.MouseDown += SplashScreen_MouseDown;
        }

        private void SplashScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public void AddMessage(string message)
        {
            Dispatcher.Invoke(() => 
            {
                MessageBlock.Text = message;
            });
        }

        public void LoadComplete()
        {
            Dispatcher.InvokeShutdown();
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void CloseApp(object sender, RoutedEventArgs e)
        {
            AppDispatcher?.InvokeShutdown();
        }

    }

    public interface ISplashScreen
    {
        Dispatcher AppDispatcher { get; set; }

        void AddMessage(string message);

        void LoadComplete();
    }
}
