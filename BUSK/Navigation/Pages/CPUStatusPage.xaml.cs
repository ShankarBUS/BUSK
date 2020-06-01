namespace BUSK.Navigation.Pages
{
    public partial class CPUStatusPage : ModernWpf.Controls.Page
    {
        public CPUStatusPage()
        {
            InitializeComponent();
            Unloaded += CPUStatusPage_Unloaded;
        }

        private void CPUStatusPage_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Host.Content = null;
        }
    }
}
