using BUSK.Charting.Defaults;
using BUSK.Charting.Definitions.Points;
using BUSK.Charting.Definitions.Series;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.SeriesAlgorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.SeriesAlgorithm" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.ICartesianSeries" />
    public class OhlcAlgorithm : SeriesAlgorithm, ICartesianSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OhlcAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public OhlcAlgorithm(ISeriesView view) : base(view)
        {
            SeriesOrientation = SeriesOrientation.Horizontal;
            PreferredSelectionMode = TooltipSelectionMode.SharedXValues;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            var castedSeries = (IFinancialSeriesView) View;
            
            const double padding = 1.2;

            var totalSpace = ChartFunctions.GetUnitWidth(AxisOrientation.X, Chart, View.ScalesXAt) - padding;

            double exceed = 0;
            double candleWidth = 0;

            if (totalSpace > castedSeries.MaxColumnWidth)
            {
                exceed = totalSpace - castedSeries.MaxColumnWidth;
                candleWidth = castedSeries.MaxColumnWidth;
            }
            else
            {
                candleWidth = totalSpace;
            }

            foreach (var chartPoint in View.ActualValues.GetPoints(View))
            {
                var x = ChartFunctions.ToDrawMargin(chartPoint.X, AxisOrientation.X, Chart, View.ScalesXAt);

                chartPoint.View = View.GetPointView(chartPoint,
                    View.DataLabels ? View.GetLabelPointFormatter()(chartPoint) : null);

                chartPoint.SeriesView = View;

                var ohclView = (IOhlcPointView) chartPoint.View;

                ohclView.Open = ChartFunctions.ToDrawMargin(chartPoint.Open, AxisOrientation.Y, Chart, View.ScalesYAt);
                ohclView.Close = ChartFunctions.ToDrawMargin(chartPoint.Close, AxisOrientation.Y, Chart, View.ScalesYAt);
                ohclView.High = ChartFunctions.ToDrawMargin(chartPoint.High, AxisOrientation.Y, Chart, View.ScalesYAt);
                ohclView.Low = ChartFunctions.ToDrawMargin(chartPoint.Low, AxisOrientation.Y, Chart, View.ScalesYAt);

                ohclView.Width = candleWidth - padding > 0 ? candleWidth - padding : 0;
                ohclView.Left = x + exceed/2 + padding;
                ohclView.StartReference = (ohclView.High + ohclView.Low)/2;

                chartPoint.ChartLocation = new CorePoint(x + exceed/2, (ohclView.High + ohclView.Low)/2);

                chartPoint.View.DrawOrMove(null, chartPoint, 0, Chart);
            }
        }

        double ICartesianSeries.GetMinX(AxisCore axis)
        {
            return AxisLimits.StretchMin(axis);
        }

        double ICartesianSeries.GetMaxX(AxisCore axis)
        {
            return AxisLimits.UnitRight(axis);
        }

        double ICartesianSeries.GetMinY(AxisCore axis)
        {
            return AxisLimits.SeparatorMin(axis);
        }

        double ICartesianSeries.GetMaxY(AxisCore axis)
        {
            return AxisLimits.SeparatorMaxRounded(axis);
        }
    }
}
