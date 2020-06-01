using System;
using BUSK.Charting.Defaults;
using BUSK.Charting.Definitions.Points;
using BUSK.Charting.Definitions.Series;
using BUSK.Charting.Dtos;
using System.Linq;

namespace BUSK.Charting.SeriesAlgorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.SeriesAlgorithm" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.ICartesianSeries" />
    public class StackedRowAlgorithm : SeriesAlgorithm, ICartesianSeries
    {
        private readonly IStackModelableSeriesView _stackModelable;
        /// <summary>
        /// Initializes a new instance of the <see cref="StackedRowAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public StackedRowAlgorithm(ISeriesView view) : base(view)
        {
            SeriesOrientation = SeriesOrientation.Vertical;
            _stackModelable = (IStackModelableSeriesView) view;
            PreferredSelectionMode = TooltipSelectionMode.SharedYValues;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            var castedSeries = (IStackedRowSeriesView) View;

            var padding = castedSeries.RowPadding;

            var totalSpace = ChartFunctions.GetUnitWidth(AxisOrientation.Y, Chart, View.ScalesYAt) - padding;
            var groups = Chart.View.ActualSeries.Select(s => (s as IGroupedStackedSeriesView)?.Grouping).Distinct().ToList();
            var singleColHeigth = totalSpace / groups.Count();

            double exceed = 0;
            var seriesPosition = groups.IndexOf(castedSeries.Grouping);

            if (singleColHeigth > castedSeries.MaxRowHeight)
            {
                exceed = (singleColHeigth - castedSeries.MaxRowHeight) * groups.Count() / 2;
                singleColHeigth = castedSeries.MaxRowHeight;
            }

            var relativeTop = padding + exceed + singleColHeigth * (seriesPosition);

            var startAt = CurrentXAxis.FirstSeparator >= 0 && CurrentXAxis.LastSeparator > 0
                ? CurrentXAxis.FirstSeparator
                : (CurrentXAxis.FirstSeparator < 0 && CurrentXAxis.LastSeparator <= 0
                    ? CurrentXAxis.LastSeparator
                    : 0);

            var zero = ChartFunctions.ToDrawMargin(startAt, AxisOrientation.X, Chart, View.ScalesXAt);

            foreach (var chartPoint in View.ActualValues.GetPoints(View))
            {
                var y = ChartFunctions.ToDrawMargin(chartPoint.Y, AxisOrientation.Y, Chart, View.ScalesYAt) - ChartFunctions.GetUnitWidth(AxisOrientation.Y, Chart, View.ScalesYAt);
                var from = _stackModelable.StackMode == StackMode.Values
                    ? ChartFunctions.ToDrawMargin(chartPoint.From, AxisOrientation.X, Chart, View.ScalesXAt)
                    : ChartFunctions.ToDrawMargin(chartPoint.From/chartPoint.Sum, AxisOrientation.X, Chart, View.ScalesXAt);
                var to = _stackModelable.StackMode == StackMode.Values
                    ? ChartFunctions.ToDrawMargin(chartPoint.To, AxisOrientation.X, Chart, View.ScalesXAt)
                    : ChartFunctions.ToDrawMargin(chartPoint.To/chartPoint.Sum, AxisOrientation.X, Chart, View.ScalesXAt);

                chartPoint.View = View.GetPointView(chartPoint,
                    View.DataLabels
                        ? (chartPoint.Participation > 0.05
                            ? View.GetLabelPointFormatter()(chartPoint)
                            : string.Empty)
                        : null);

                chartPoint.SeriesView = View;

                var rectangleView = (IRectanglePointView) chartPoint.View;

                var w = Math.Abs(to - zero) - Math.Abs(from - zero);
                var l = to < zero
                    ? to
                    : from;

                rectangleView.Data.Height = singleColHeigth - padding;
                rectangleView.Data.Top = y + relativeTop;

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
            return double.MaxValue;
        }

        double ICartesianSeries.GetMaxX(AxisCore axis)
        {
            return double.MinValue;
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
