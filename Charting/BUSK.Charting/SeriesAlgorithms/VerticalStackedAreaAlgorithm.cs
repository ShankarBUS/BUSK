using BUSK.Charting.Definitions.Series;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.SeriesAlgorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.SeriesAlgorithms.StackedAreaAlgorithm" />
    public class VerticalStackedAreaAlgorithm : StackedAreaAlgorithm
    {
        private readonly IStackModelableSeriesView _stackModelable;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalStackedAreaAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public VerticalStackedAreaAlgorithm(ISeriesView view) : base(view)
        {
            SeriesOrientation = SeriesOrientation.Vertical;
            _stackModelable = (IStackModelableSeriesView) view;
            PreferredSelectionMode = TooltipSelectionMode.SharedYValues;
        }

        /// <summary>
        /// Gets the stacked point.
        /// </summary>
        /// <param name="chartPoint">The chart point.</param>
        /// <returns></returns>
        protected override CorePoint GetStackedPoint(ChartPoint chartPoint)
        {
            if (_stackModelable.StackMode == StackMode.Values)
                return new CorePoint(
                    ChartFunctions.ToDrawMargin(chartPoint.To, AxisOrientation.X, Chart, View.ScalesXAt),
                    ChartFunctions.ToDrawMargin(chartPoint.Y, AxisOrientation.Y, Chart, View.ScalesYAt));

            return new CorePoint(
                ChartFunctions.ToDrawMargin(chartPoint.StackedParticipation, AxisOrientation.X, Chart, View.ScalesXAt),
                ChartFunctions.ToDrawMargin(chartPoint.Y, AxisOrientation.Y, Chart, View.ScalesYAt));
        }
    }
}
