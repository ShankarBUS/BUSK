using BUSK.Charting.WPF;
using System;
using System.Windows;

namespace BUSK.Controls.Plotting
{
    public class PerformanceChart : CartesianChart
    {
        private Axis xAxis;

        private Axis yAxis;

        private double time = 0.0;

        public const string DefaultXAxisTitle = "60 Seconds";

        static PerformanceChart()
        {
            FontSizeProperty.OverrideMetadata(typeof(PerformanceChart), new FrameworkPropertyMetadata(OnFontSizeChanged));
        }

        public PerformanceChart()
        {
            xAxis = new Axis { Separator = new Separator { Step = 1 }, ShowLabels = false, Title = DefaultXAxisTitle, Unit = 1.0 };
            yAxis = new Axis() { MinValue = 0.0 };

            AxisX.Add(xAxis);
            AxisY.Add(yAxis);

            DataTooltip = null;
            Hoverable = false;
            TooltipTimeout = TimeSpan.Zero;
        }

        #region Properties

        #region XAxisTitle

        public static readonly DependencyProperty XAxisTitleProperty = DependencyProperty.Register(
            nameof(XAxisTitle),
            typeof(string),
            typeof(PerformanceChart),
            new PropertyMetadata(DefaultXAxisTitle, OnXAxisTitleChanged));

        public string XAxisTitle
        {
            get { return (string)GetValue(XAxisTitleProperty); }
            set { SetValue(XAxisTitleProperty, value); }
        }

        private static void OnXAxisTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pc = (PerformanceChart)d;
            pc.xAxis.Title = (string)e.NewValue;
        }

        #endregion

        #region MaxYValue

        public static readonly DependencyProperty MaxYValueProperty = DependencyProperty.Register(
            nameof(MaxYValue),
            typeof(double),
            typeof(PerformanceChart),
            new PropertyMetadata(double.NaN, OnMaxYValueChanged));

        public double MaxYValue
        {
            get { return (double)GetValue(MaxYValueProperty); }
            set { SetValue(MaxYValueProperty, value); }
        }

        private static void OnMaxYValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pc = (PerformanceChart)d;
            pc.yAxis.MaxValue = pc.MaxYValue;
        }

        #endregion

        #region YAxisLabelFormatter

        public static readonly DependencyProperty YAxisLabelFormatterProperty = DependencyProperty.Register(
            nameof(YAxisLabelFormatter),
            typeof(Func<double, string>),
            typeof(PerformanceChart),
            new PropertyMetadata(default(Func<double, string>), OnYAxisLabelFormatterChanged));

        public Func<double, string> YAxisLabelFormatter
        {
            get { return (Func<double, string>)GetValue(YAxisLabelFormatterProperty); }
            set { SetValue(YAxisLabelFormatterProperty, value); }
        }

        private static void OnYAxisLabelFormatterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pc = (PerformanceChart)d;
            pc.yAxis.LabelFormatter = (Func<double, string>)e.NewValue;
        }

        #endregion

        #endregion

        public void UpdateValuesAndScrollGraph(double bandWidth)
        {
            PlotGraphValues();
            ScrollGraph(bandWidth);
        }

        public void ScrollGraph(double bandWidth)
        {
            if (xAxis.Model == null)
            {
                xAxis.MinValue = -bandWidth;
                return;
            }

            xAxis.MinValue = xAxis.ActualMaxValue - bandWidth;

            foreach (var series in Series)
            {
                if (series.Values.Count > (bandWidth + 5))
                {
                    series.Values.RemoveAt(0);
                }
            }
        }

        private void PlotGraphValues()
        {
            time += 1.0;

            foreach (var series in Series)
            {
                if (series is PerformanceSeries performanceSeries)
                {
                    performanceSeries.PlotValue(time);
                }
            }
        }

        public void PrepareForMiniViewState()
        {
            yAxis.Separator.StrokeThickness = 0;
        }

        public void RestoreFromMinViewState()
        {
            yAxis.Separator.StrokeThickness = 1;
        }

        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pc = (PerformanceChart)d;
            var fontSize = (double)e.NewValue;
            pc.xAxis.FontSize = fontSize;
            pc.yAxis.FontSize = fontSize;
        }
    }
}
