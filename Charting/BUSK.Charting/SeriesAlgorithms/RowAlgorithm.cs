using System;
using System.Linq;
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
    public class RowAlgorithm : SeriesAlgorithm, ICartesianSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RowAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public RowAlgorithm(ISeriesView view) : base(view)
        {
            SeriesOrientation = SeriesOrientation.Vertical;
            PreferredSelectionMode = TooltipSelectionMode.SharedYInSeries;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            var castedSeries = (IRowSeriesView) View;

            var padding = castedSeries.RowPadding;
            
            var totalSpace = ChartFunctions.GetUnitWidth(AxisOrientation.Y, Chart, View.ScalesYAt) - padding;
            var typeSeries = Chart.View.ActualSeries.OfType<IRowSeriesView>().ToList();

            var singleRowHeight = totalSpace/typeSeries.Count;

            double exceed = 0;

            var seriesPosition = typeSeries.IndexOf((IRowSeriesView) View);

            if (singleRowHeight > castedSeries.MaxRowHeigth)
            {
                exceed = (singleRowHeight - castedSeries.MaxRowHeigth)*typeSeries.Count/2;
                singleRowHeight = castedSeries.MaxRowHeigth;
            }

            var relativeTop = padding + exceed + singleRowHeight * (seriesPosition);

            var startAt = CurrentXAxis.FirstSeparator >= 0 && CurrentXAxis.LastSeparator > 0   //both positive
                ? CurrentXAxis.FirstSeparator                                                  //then use Min
                : (CurrentXAxis.FirstSeparator < 0 && CurrentXAxis.LastSeparator <= 0          //both negative
                    ? CurrentXAxis.LastSeparator                                               //then use Max
                    : 0);                                                                      //if mixed then use 0

            var zero = ChartFunctions.ToDrawMargin(startAt, AxisOrientation.X, Chart, View.ScalesXAt);

            var correction = ChartFunctions.GetUnitWidth(AxisOrientation.Y, Chart, View.ScalesYAt);

            foreach (var chartPoint in View.ActualValues.GetPoints(View))
            {
                var reference =
                    ChartFunctions.ToDrawMargin(chartPoint, View.ScalesXAt, View.ScalesYAt, Chart);

                chartPoint.View = View.GetPointView(chartPoint,
                    View.DataLabels ? View.GetLabelPointFormatter()(chartPoint) : null);

                chartPoint.SeriesView = View;

                var rectangleView = (IRectanglePointView) chartPoint.View;

                var w = Math.Abs(reference.X - zero);
                var l = reference.X < zero
                    ? reference.X
                    : zero;

            
                if (chartPoint.EvaluatesGantt)
                {
                    l = ChartFunctions.ToDrawMargin(chartPoint.XStart, AxisOrientation.X, Chart, View.ScalesXAt);
                    if (!(reference.X < zero && l < zero)) w -= l;
                }

                rectangleView.Data.Height = singleRowHeight - padding;
                rectangleView.Data.Top = reference.Y + relativeTop - correction;

                rectangleView.Data.Left = l;
                rectangleView.Data.Width = w;

                rectangleView.ZeroReference = zero;

                chartPoint.ChartLocation = new CorePoint(rectangleView.Data.Left + rectangleView.Data.Width,
                    rectangleView.Data.Top);

                chartPoint.View.DrawOrMove(null, chartPoint, 0, Chart);
            }
        }

        double ICartesianSeries.GetMinX(AxisCore axis)
        {
            var f = AxisLimits.SeparatorMin(axis);
            return CurrentXAxis.BotLimit >= 0 && CurrentXAxis.TopLimit > 0
                ? (f >= 0 ? f : 0)
                : f;
        }

        double ICartesianSeries.GetMaxX(AxisCore axis)
        {
            var f = AxisLimits.SeparatorMaxRounded(axis);
            return CurrentXAxis.BotLimit < 0 && CurrentXAxis.TopLimit <= 0
                ? (f >= 0 ? f : 0)
                : f;
        }

        double ICartesianSeries.GetMinY(AxisCore axis)
        {
            return AxisLimits.StretchMin(axis);
        }

        double ICartesianSeries.GetMaxY(AxisCore axis)
        {
            return AxisLimits.UnitRight(axis);
        }
    }
}
