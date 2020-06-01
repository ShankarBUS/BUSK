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
    public class CandleAlgorithm : SeriesAlgorithm, ICartesianSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandleAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public CandleAlgorithm(ISeriesView view) : base(view)
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
            double candleWidth;

            if (totalSpace > castedSeries.MaxColumnWidth)
            {
                exceed = totalSpace - castedSeries.MaxColumnWidth;
                candleWidth = castedSeries.MaxColumnWidth;
            }
            else
            {
                candleWidth = totalSpace;
            }

            ChartPoint previousDrawn = null;

            foreach (var chartPoint in View.ActualValues.GetPoints(View))
            {
                var x = ChartFunctions.ToDrawMargin(chartPoint.X, AxisOrientation.X, Chart, View.ScalesXAt);

                chartPoint.View = View.GetPointView(chartPoint,
                    View.DataLabels ? View.GetLabelPointFormatter()(chartPoint) : null);

                chartPoint.SeriesView = View;

                var candeView = (IOhlcPointView) chartPoint.View;

                candeView.Open = ChartFunctions.ToDrawMargin(chartPoint.Open, AxisOrientation.Y, Chart, View.ScalesYAt);
                candeView.Close = ChartFunctions.ToDrawMargin(chartPoint.Close, AxisOrientation.Y, Chart, View.ScalesYAt);
                candeView.High = ChartFunctions.ToDrawMargin(chartPoint.High, AxisOrientation.Y, Chart, View.ScalesYAt);
                candeView.Low = ChartFunctions.ToDrawMargin(chartPoint.Low, AxisOrientation.Y, Chart, View.ScalesYAt);

                candeView.Width = candleWidth - padding > 0 ? candleWidth - padding : 0;
                candeView.Left = x + exceed/2 + padding;
                candeView.StartReference = (candeView.High + candeView.Low)/2;

                chartPoint.ChartLocation = new CorePoint(x + exceed/2, (candeView.High + candeView.Low)/2);

                chartPoint.View.DrawOrMove(previousDrawn, chartPoint, 0, Chart);

                previousDrawn = chartPoint;
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
