using BUSK.Core.Utilities;
using BUSK.Input;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.Controls
{
    public partial class DiskItem : UserControl
    {
        public DiskItem()
        {
            InitializeComponent();

            Loaded += DiskItem_Loaded;
            Unloaded += DiskItem_Unloaded;
        }

        private void DiskItem_Unloaded(object sender, RoutedEventArgs e)
        {
            InputHelper.SetIsTapEnabled(this, false);
            InputHelper.RemoveTappedHandler(this, UserControl_Tapped);
        }

        private void DiskItem_Loaded(object sender, RoutedEventArgs e)
        {
            InputHelper.SetIsTapEnabled(this, true);
            InputHelper.AddTappedHandler(this, UserControl_Tapped);
        }

        public DriveInfo DriveInfo { get; set; }

        public async void LoadDriveInfos()
        {
            if (!DriveInfo.IsReady) return;

            (string title, string info1, string info2, string driveType, double percentage) = await GetInfosAsync();
            Dispatcher.Invoke(() =>
            {
                IsEnabled = true;
                Tbtitle.Text = title; Tbinfo1.Text = info1; Tbinfo2.Text = info2; TbdriveType.Text = driveType;
                PB.Value = percentage; PB.IsIndeterminate = false;
            });
        }

        private async Task<(string title, string info1, string info2, string driveType, double percentage)> GetInfosAsync()
        {
            return await Task.Run(() => FetchInfos());
        }

        private (string title, string info1, string info2, string driveType, double percentage) FetchInfos()
        {
            var d = DriveInfo;
            var title = (d.VolumeLabel == "" ? (d.DriveType == DriveType.Fixed ? "Local Disk" : "Removable Disk") : d.VolumeLabel) + $" ({d.Name.Replace(@"\", "")})";
            var info1 = DataConverter.FormatBytes(d.TotalSize) + " " + d.DriveFormat;
            var info2 = DataConverter.FormatBytes(d.TotalSize - d.AvailableFreeSpace) + " Used - " + DataConverter.FormatBytes(d.AvailableFreeSpace) + " Free";
            var driveType = d.DriveType.ToString();
            var percentage = (1 - ((double)d.AvailableFreeSpace / (double)d.TotalSize)) * 100.0;
            return (title, info1, info2, driveType, percentage);
        }

        private void UserControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = DriveInfo.Name, UseShellExecute = true });
        }
    }
}
