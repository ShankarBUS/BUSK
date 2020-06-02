using System.Windows;
using BUSK.Charting;
using BUSK.Charting.Configurations;
using BUSK.Charting.WPF;

namespace BUSK.Controls.Plotting
{
    public class PerformanceSeries : LineSeries
    {
        private ChartValues<MeasureModel> values = new ChartValues<MeasureModel>(new [] { new MeasureModel() { Time = 0, Value = 0.0 } });

        static PerformanceSeries()
        {
            var mapper = Mappers.Xy<MeasureModel>().X(o => o.Time).Y(o => o.Value);
            Charting.Charting.For<MeasureModel>(mapper);
        }

        public PerformanceSeries()
        {
            LineSmoothness = 0.0;
            PointGeometry = Geometry.Empty;
            StrokeThickness = 2.0;
            Values = values;
        }

        #region Properties

        #region GraphValue

        public static readonly DependencyProperty GraphValueProperty = DependencyProperty.Register(
            nameof(GraphValue),
            typeof(double),
            typeof(PerformanceSeries),
            new FrameworkPropertyMetadata(0.0));

        public double GraphValue
        {
            get => (double)GetValue(GraphValueProperty);
            set => SetValue(GraphValueProperty, value);
        }

        #endregion

        #endregion

        public void PlotValue(double time)
        {
            values.Add(new MeasureModel() { Time = time, Value = GraphValue });
        }

        private class MeasureModel
        {
            public double Time { get; set; }

            public double Value { get; set; }
        }
    }
}
