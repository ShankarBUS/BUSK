using BUSK.Charting.Charts;

namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICartesianVisualElement
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        double X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        double Y { get; set; }
        /// <summary>
        /// Gets or sets the axis x.
        /// </summary>
        /// <value>
        /// The axis x.
        /// </value>
        int AxisX { get; set; }
        /// <summary>
        /// Gets or sets the axis y.
        /// </summary>
        /// <value>
        /// The axis y.
        /// </value>
        int AxisY { get; set; }
        /// <summary>
        /// Adds the or move.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void AddOrMove(ChartCore chart);
        /// <summary>
        /// Removes the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void Remove(ChartCore chart);
    }
}