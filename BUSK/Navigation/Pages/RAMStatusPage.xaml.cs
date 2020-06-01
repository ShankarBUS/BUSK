namespace BUSK.Navigation.Pages
{
    public partial class RAMStatusPage : ModernWpf.Controls.Page
    {
        public RAMStatusPage()
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
