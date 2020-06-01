using System.Windows;
using System.Windows.Controls;

namespace BUSK.Navigation.Pages
{
    public partial class ExtensionsPage : Page
    {
        public ExtensionsPage()
        {
            InitializeComponent();
        }

        private void ButConfig_Click(object sender, RoutedEventArgs e)
        {
            new Controls.ExtConfigMaker().Show();
        }

        private void ButReload_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
