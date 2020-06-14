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

        private PerformanceSeries line;

        private TextBlock extraInfo;

        private bool _isinit = false;

        #region Properties

        public string ProcessorInfo { get; private set; } = "";

        private GraphView graphView;

        public GraphView GraphView
        {
            get { return graphView; }
            set { SetPropertyValue(ref graphView, value); }
        }

        #endregion

        public CPUPageViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_isinit)
            {
                return;
            }

            GraphView = new GraphView()
            {
                MaxYValue = 100.0
            };

            line = new PerformanceSeries();
            line.SetBinding(PerformanceSeries.GraphValueProperty, new Binding(nameof(CPUInformation.CPUUsage)) { Source = CPUInformation.Instance });
            line.SetResourceReference(PerformanceSeries.FillProperty, "CPULineSeriesFill");
            line.SetResourceReference(PerformanceSeries.StrokeProperty, "CPULineSeriesStroke");

            GraphView.Series.Add(line);

            extraInfo = new TextBlock() { Margin = new Thickness(10.0), HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Top, FontSize = 30.0 };
            extraInfo.SetBinding(TextBlock.TextProperty, new Binding(nameof(CPUInformation.CPUUsageText)) { Source = CPUInformation.Instance });

            GraphView.AdditionalMiniViewContent = extraInfo;
            GraphView.TitleBlock.Text = "% Utilization";

            ProcessorInfo = HardwareInfo.GetProcessorInformation();

            _isinit = true;
        }

        public void Suspend()
        {
            if (!_isinit)
            {
                return;
            }

            BindingOperations.ClearBinding(line, PerformanceSeries.GraphValueProperty);
            BindingOperations.ClearBinding(extraInfo, TextBlock.TextProperty);
            line.Values.Clear();
            line = null;
            GraphView.Series.Clear();
            GraphView.AdditionalMiniViewContent = null;
            GraphView.Dispose();
            GraphView = null;
            _isinit = false;
        }
    }
}
