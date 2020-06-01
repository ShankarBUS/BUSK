using BUSK.Charting.Defaults;
using BUSK.Charting.Definitions.Series;

namespace BUSK.Charting.SeriesAlgorithms
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.SeriesAlgorithms.LineAlgorithm" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.ICartesianSeries" />
    public class VerticalLineAlgorithm : LineAlgorithm, ICartesianSeries
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalLineAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public VerticalLineAlgorithm(ISeriesView view) : base(view)
        {
            SeriesOrientation = SeriesOrientation.Vertical;
            PreferredSelectionMode = TooltipSelectionMode.SharedYValues;
        }

        double ICartesianSeries.GetMinX(AxisCore axis)
        {
            return AxisLimits.SeparatorMin(axis);
        }

        double ICartesianSeries.GetMaxX(AxisCore axis)
        {
            return AxisLimits.SeparatorMaxRounded(axis);
        }

        double ICartesianSeries.GetMinY(AxisCore axis)
        {
            return AxisLimits.StretchMin(axis);
        }

        double ICartesianSeries.GetMaxY(AxisCore axis)
        {
            return AxisLimits.StretchMax(axis);
        }
    }
}
