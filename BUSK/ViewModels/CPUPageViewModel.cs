using BUSK.Controls.Plotting;
using BUSK.Core;
using BUSK.Core.Diagnostics;
using BUSK.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BUSK.ViewModels
{
    public class CPUPageViewModel : BindableBase
    {
        public static CPUPageViewModel Instance { get; set; }

        #region Properties

        public string ProcessorInfo { get; } = "";

        private GraphView graphView;

        public GraphView GraphView
        {
            get { return graphView; }
            set { SetPropertyValue(ref graphView, value); }
        }

        #endregion

        public CPUPageViewModel()
        {
            GraphView = new GraphView()
            {
                MaxYValue = 100.0
            };

            var line = new PerformanceSeries();
            GraphView.Series.Add(line);
            line.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(CPUInformation.CPUUsage)) { Source = CPUInformation.Instance });
            line.SetResourceReference(PerformanceSeries.FillProperty, "CPULineSeriesFill");
            line.SetResourceReference(PerformanceSeries.StrokeProperty, "CPULineSeriesStroke");

            var extraInfo = new TextBlock() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, FontSize = 30.0 };
            extraInfo.SetBinding(TextBlock.TextProperty, new Binding(nameof(CPUInformation.CPUUsageText)) { Source = CPUInformation.Instance });

            GraphView.AdditionalMiniViewContent = extraInfo;

            ProcessorInfo = HardwareInfo.GetProcessorInformation();
        }
    }
}
