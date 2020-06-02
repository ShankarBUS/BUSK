using BUSK.Controls.Plotting;
using BUSK.Core;
using BUSK.Core.Diagnostics;
using BUSK.Core.Utilities;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

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

        private GraphView graphView2;

        public GraphView GraphView2
        {
            get { return graphView2; }
            set { SetPropertyValue(ref graphView2, value); }
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
            line.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(DiskInformation.DiskUsage)) { Source = DiskInformation.Instance });
            line.SetResourceReference(PerformanceSeries.FillProperty, "DiskLineSeriesFill");
            line.SetResourceReference(PerformanceSeries.StrokeProperty, "DiskLineSeriesStroke");

            var extraInfo = new TextBlock() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, FontSize = 30.0 };
            extraInfo.SetBinding(TextBlock.TextProperty, new Binding(nameof(DiskInformation.DiskUsageText)) { Source = DiskInformation.Instance });

            GraphView.AdditionalMiniViewContent = extraInfo;
            GraphView.TitleBlock.Text = "Active time";

            #region Read & Write speed graph

            GraphView2 = new GraphView()
            {
                YAxisLabelFormatter = (o) => DataConverter.FormatBytes((long)o) + "/s"
            };

            var lineRead = new PerformanceSeries();
            GraphView2.Series.Add(lineRead);
            lineRead.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(DiskInformation.ReadBytes)) { Source = DiskInformation.Instance });
            lineRead.SetResourceReference(PerformanceSeries.FillProperty, "DiskLineSeriesReadFill");
            lineRead.SetResourceReference(PerformanceSeries.StrokeProperty, "DiskLineSeriesReadStroke");

            var lineWrite = new PerformanceSeries() { StrokeDashArray = new DoubleCollection(new[] { 2.0, 4.0 }) };
            GraphView2.Series.Add(lineWrite);
            lineWrite.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(DiskInformation.WriteBytes)) { Source = DiskInformation.Instance });
            lineWrite.SetResourceReference(PerformanceSeries.FillProperty, "DiskLineSeriesWriteFill");
            lineWrite.SetResourceReference(PerformanceSeries.StrokeProperty, "DiskLineSeriesWriteStroke");

            var extraInfo2 = new StackPanel() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, Orientation = Orientation.Horizontal };
            var tbRead = new TextBlock() { FontSize = 12.0 };
            var tbWrite = new TextBlock() { FontSize = 12.0, Margin = new Thickness(5.0, 0.0, 0.0, 0.0) };

            tbRead.SetBinding(TextBlock.TextProperty, new Binding(nameof(DiskInformation.Read)) { Source = DiskInformation.Instance, StringFormat = "R : {0}" });
            tbWrite.SetBinding(TextBlock.TextProperty, new Binding(nameof(DiskInformation.Write)) { Source = DiskInformation.Instance, StringFormat = "W : {0}" });
            extraInfo2.Children.Add(tbRead); extraInfo2.Children.Add(tbWrite);

            GraphView2.AdditionalMiniViewContent = extraInfo2;
            GraphView2.TitleBlock.Text = "Disk transfer rate";

            #endregion

            DiskInformation.Instance.DiskCounterAssigned += (s, e) =>
            {
                line.Values.Clear();
                var driveInfos = DriveInfo.GetDrives();
                foreach(var driveInfo in driveInfos)
                {
                    if (driveInfo.IsReady)
                    {
                        var driveletter = driveInfo.Name.Remove(1);
                        if (DiskInformation.Instance.CurrentDiskName.Contains(driveletter))
                        {
                            DriveType = driveInfo.DriveType;
                            DiskInfo = $"{DiskInformation.Instance.CurrentDiskName} ({DriveType})";
                        }
                    }
                }
            };
        }
    }
}
