using BUSK.Charting.Charts;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Points
{

    /// <summary>
    /// 
    /// </summary>
    public interface IChartPointView
    {
        /// <summary>
        /// Gets a value indicating whether this instance is new.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is new; otherwise, <c>false</c>.
        /// </value>
        bool IsNew { get; }
        /// <summary>
        /// Gets the valid area.
        /// </summary>
        /// <value>
        /// The valid area.
        /// </value>
        CoreRectangle ValidArea { get; }
        /// <summary>
        /// Draws the or move.
        /// </summary>
        /// <param name="previousDrawn">The previous drawn.</param>
        /// <param name="current">The current.</param>
        /// <param name="index">The index.</param>
        /// <param name="chart">The chart.</param>
        void DrawOrMove(ChartPoint previousDrawn, ChartPoint current, int index, ChartCore chart);
        /// <summary>
        /// Removes from view.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void RemoveFromView(ChartCore chart);
        /// <summary>
        /// Called when [hover].
        /// </summary>
        /// <param name="point">The point.</param>
        void OnHover(ChartPoint point);
        /// <summary>
        /// Called when [hover leave].
        /// </summary>
        /// <param name="point">The point.</param>
        void OnHoverLeave(ChartPoint point);
    }

}
