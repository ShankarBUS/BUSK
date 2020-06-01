using BUSK.Controls.Plotting;
using BUSK.Core;
using BUSK.Core.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BUSK.ViewModels
{
    public class DiskPageViewModel : BindableBase
    {
        public static DiskPageViewModel Instance { get; set; }

        #region Properties

        private GraphView graphView;

        public GraphView GraphView
        {
            get { return graphView; }
            set { SetPropertyValue(ref graphView, value); }
        }

        private DriveType driveType = DriveType.Fixed;

        public DriveType DriveType
        {
            get { return driveType; }
            set { SetPropertyValue(ref driveType, value); }
        }

        private string diskInfo = "";

        public string DiskInfo
        {
            get { return diskInfo; }
            set { SetPropertyValue(ref diskInfo, value); }
        }

        #endregion

        public DiskPageViewModel()
        {
            GraphView = new GraphView()
            {
                MaxYValue = 100.0
            };

            var line = new PerformanceSeries();
            GraphView.Series.Add(line);
            line.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(DiskPerfManager.DiskUsage)) { Source = DiskPerfManager.Instance });
            line.SetResourceReference(PerformanceSeries.FillProperty, "DiskLineSeriesFill");
            line.SetResourceReference(PerformanceSeries.StrokeProperty, "DiskLineSeriesStroke");

            var extraInfo = new TextBlock() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, FontSize = 30.0 };
            extraInfo.SetBinding(TextBlock.TextProperty, new Binding(nameof(DiskPerfManager.DiskUsageText)) { Source = DiskPerfManager.Instance });

            GraphView.AdditionalMiniViewContent = extraInfo;

            DiskPerfManager.Instance.DiskCounterAssigned += (s, e) =>
            {
                line.Values.Clear();
                var driveInfos = DriveInfo.GetDrives();
                foreach(var driveInfo in driveInfos)
                {
                    if (driveInfo.IsReady)
                    {
                        var driveletter = driveInfo.Name.Remove(1);
                        if (DiskPerfManager.Instance.CurrentDiskName.Contains(driveletter))
                        {
                            DriveType = driveInfo.DriveType;
                            DiskInfo = $"{DiskPerfManager.Instance.CurrentDiskName} ({DriveType})";
                        }
                    }
                }
            };
        }
    }
}
