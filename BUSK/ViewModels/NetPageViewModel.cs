using BUSK.Controls.Plotting;
using BUSK.Core;
using BUSK.Core.Diagnostics;
using BUSK.Core.Utilities;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BUSK.ViewModels
{
    public class NetPageViewModel : BindableBase
    {
        public static NetPageViewModel Instance { get; set; }

        #region Properties

        private GraphView graphView;

        public GraphView GraphView
        {
            get { return graphView; }
            set { SetPropertyValue(ref graphView, value); }
        }

        private string networkInfo = "";

        public string NetworkInfo
        {
            get { return networkInfo; }
            set { SetPropertyValue(ref networkInfo, value); }
        }

        private string macAddress = "";

        public string MACAddress
        {
            get { return macAddress; }
            set { SetPropertyValue(ref macAddress, value); }
        }

        private string interfaceType = "";

        public string NetworkInterfaceType
        {
            get { return interfaceType; }
            set { SetPropertyValue(ref interfaceType, value); }
        }

        private string ipv6 = "";

        public string IPv6
        {
            get { return ipv6; }
            set { SetPropertyValue(ref ipv6, value); }
        }

        private string ipv4 = "";

        public string IPv4
        {
            get { return ipv4; }
            set { SetPropertyValue(ref ipv4, value); }
        }

        #endregion

        public NetPageViewModel()
        {
            GraphView = new GraphView()
            {
                YAxisLabelFormatter = (o) => DataConverter.FormatBytes((long)o) + "/s"
            };

            var lineDown = new PerformanceSeries();
            GraphView.Series.Add(lineDown);
            lineDown.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(NetInformation.DownBytes)) { Source = NetInformation.Instance });
            lineDown.SetResourceReference(PerformanceSeries.FillProperty, "NetLineSeriesDownFill");
            lineDown.SetResourceReference(PerformanceSeries.StrokeProperty, "NetLineSeriesDownStroke");

            var lineUp = new PerformanceSeries() { StrokeDashArray = new DoubleCollection(new[] { 2.0, 4.0 }) };
            GraphView.Series.Add(lineUp);
            lineUp.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(NetInformation.UpBytes)) { Source = NetInformation.Instance });
            lineUp.SetResourceReference(PerformanceSeries.FillProperty, "NetLineSeriesUpFill");
            lineUp.SetResourceReference(PerformanceSeries.StrokeProperty, "NetLineSeriesUpStroke");

            var extraInfo = new StackPanel() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, Orientation = Orientation.Horizontal };
            var tbDown = new TextBlock() { FontSize = 12.0 };
            var tbUp = new TextBlock() { FontSize = 12.0, Margin = new Thickness(5.0, 0.0, 0.0, 0.0) };

            tbDown.SetBinding(TextBlock.TextProperty, new Binding(nameof(NetInformation.Down)) { Source = NetInformation.Instance, StringFormat = "D : {0}" });
            tbUp.SetBinding(TextBlock.TextProperty, new Binding(nameof(NetInformation.Up)) { Source = NetInformation.Instance, StringFormat = "U : {0}" });
            extraInfo.Children.Add(tbDown); extraInfo.Children.Add(tbUp);

            GraphView.AdditionalMiniViewContent = extraInfo;
            GraphView.TitleBlock.Text = "Network transfer rate";

            NetInformation.Instance.NetworkInterfaceAssigned += (s, e) =>
            {
                lineDown.Values.Clear();
                lineUp.Values.Clear();
                LoadInfos();
            };

            LoadInfos();
        }

        private void LoadInfos()
        {
            var NIS = NetworkInterface.GetAllNetworkInterfaces();
            int index = NetInformation.GetIndexOfNetInterface(NIS, NetInformation.Instance.CurrentNetInterface);
            var ni = NIS[index];
            NetworkInfo = $"{ni.Name} ({ni.Description})";
            MACAddress = NetInformation.GetFormattedMACAddress(ni.GetPhysicalAddress().ToString());
            NetworkInterfaceType = ni.NetworkInterfaceType.ToString();
            IPv6 = NetInformation.GetIPv6(ni);
            IPv4 = NetInformation.GetIPv4(ni);
        }
    }
}
