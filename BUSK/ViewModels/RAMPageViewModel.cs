using BUSK.Controls.Plotting;
using BUSK.Core;
using BUSK.Core.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BUSK.ViewModels
{
    public class RAMPageViewModel : BindableBase
    {
        public static RAMPageViewModel Instance { get; set; }

        #region Properties

        private GraphView graphView;

        public GraphView GraphView
        {
            get { return graphView; }
            set { SetPropertyValue(ref graphView, value); }
        }

        #endregion

        public RAMPageViewModel()
        {
            GraphView = new GraphView()
            {
                MaxYValue = 100.0
            };

            var line = new PerformanceSeries();
            GraphView.Series.Add(line);
            line.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(RAMPerfManager.RAMUsage)) { Source = RAMPerfManager.Instance });
            line.SetResourceReference(PerformanceSeries.FillProperty, "RAMLineSeriesFill");
            line.SetResourceReference(PerformanceSeries.StrokeProperty, "RAMLineSeriesStroke");

            var extraInfo = new TextBlock() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, FontSize = 30.0 };
            extraInfo.SetBinding(TextBlock.TextProperty, new Binding(nameof(RAMPerfManager.RAMUsageText)) { Source = RAMPerfManager.Instance });

            GraphView.AdditionalMiniViewContent = extraInfo;
        }
    }
}
