using System.Windows;
using System.Windows.Controls;

namespace BUSK.Navigation.Pages
{
    public partial class NetStatusPage : Page
    {
        public NetStatusPage()
        {
            InitializeComponent();
            Unloaded += DiskStatusPage_Unloaded;
        }

        private void DiskStatusPage_Unloaded(object sender, RoutedEventArgs e)
        {
            Host.Content = null;
        }
    }
}
