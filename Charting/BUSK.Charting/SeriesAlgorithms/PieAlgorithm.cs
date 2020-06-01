using System.Linq;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.Definitions.Points;
using BUSK.Charting.Definitions.Series;

namespace BUSK.Charting.SeriesAlgorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.SeriesAlgorithm" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.IPieSeries" />
    public class PieAlgorithm : SeriesAlgorithm, IPieSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PieAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public PieAlgorithm(ISeriesView view) : base(view)
        {
            PreferredSelectionMode= TooltipSelectionMode.SharedXValues;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            var pieChart = (IPieChart) View.Model.Chart.View;

            var maxPushOut = View.Model.Chart.View.ActualSeries
                .OfType<IPieSeriesView>()
                .Select(x => x.PushOut)
                .DefaultIfEmpty(0).Max();

            var minDimension = Chart.DrawMargin.Width < Chart.DrawMargin.Height
                ? Chart.DrawMargin.Width
                : Chart.DrawMargin.Height;
            minDimension -= 10 + maxPushOut;
            minDimension = minDimension < 10 ? 10 : minDimension;
            
            var inner = pieChart.InnerRadius;

            var startAt = pieChart.StartingRotationAngle > 360
                ? 360
                : (pieChart.StartingRotationAngle < 0
                    ? 0
                    : pieChart.StartingRotationAngle);

            foreach (var chartPoint in View.ActualValues.GetPoints(View))
            {
                chartPoint.View = View.GetPointView(chartPoint,
                    View.DataLabels
                        ? View.GetLabelPointFormatter()(chartPoint)
                        : null);

                var pieSlice = (IPieSlicePointView) chartPoint.View;

                var space = pieChart.InnerRadius +
                            ((minDimension/2) - pieChart.InnerRadius)*((chartPoint.X + 1)/(View.Values.GetTracker(View).XLimit.Max + 1));

                chartPoint.SeriesView = View;

                pieSlice.Radius = space;
                pieSlice.InnerRadius = inner;
                pieSlice.Rotation = startAt + (chartPoint.StackedParticipation - chartPoint.Participation)*360;
                pieSlice.Wedge = chartPoint.Participation*360 > 0 ? chartPoint.Participation*360 : 0;

                chartPoint.View.DrawOrMove(null, chartPoint, 0, Chart);

                inner = pieSlice.Radius;
            }
        }
    }
}
