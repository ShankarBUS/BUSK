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
    public class StepLineAlgorithm : SeriesAlgorithm, ICartesianSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StepLineAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public StepLineAlgorithm(ISeriesView view) : base(view)
        {
            SeriesOrientation = SeriesOrientation.Horizontal;
            PreferredSelectionMode = TooltipSelectionMode.SharedXValues;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public override void Update()
        {
            ChartPoint previous = null;

            var i = 0;
            foreach (var current in View.ActualValues.GetPoints(View))
            {
                current.View = View.GetPointView(current,
                    View.DataLabels ? View.GetLabelPointFormatter()(current) : null);

                current.SeriesView = View;

                var stepView = (IStepPointView) current.View;

                current.ChartLocation = new CorePoint(
                    ChartFunctions.ToDrawMargin(current.X, AxisOrientation.X, Chart, View.ScalesXAt),
                    ChartFunctions.ToDrawMargin(current.Y, AxisOrientation.Y, Chart, View.ScalesYAt));

                if (previous != null)
                {
                    stepView.DeltaX = current.ChartLocation.X - previous.ChartLocation.X;
                    stepView.DeltaY = current.ChartLocation.Y - previous.ChartLocation.Y;
                }

                current.View.DrawOrMove(previous, current, i, Chart);

                i++;
                previous = current;
            }
        }

        double ICartesianSeries.GetMinX(AxisCore axis)
        {
            return AxisLimits.StretchMin(axis);
        }

        double ICartesianSeries.GetMaxX(AxisCore axis)
        {
            return AxisLimits.StretchMax(axis);
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
