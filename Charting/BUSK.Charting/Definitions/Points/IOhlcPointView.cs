namespace BUSK.Charting.Definitions.Points
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Points.IChartPointView" />
    public interface IOhlcPointView : IChartPointView
    {
        /// <summary>
        /// Gets or sets the open.
        /// </summary>
        /// <value>
        /// The open.
        /// </value>
        double Open { get; set; }
        /// <summary>
        /// Gets or sets the high.
        /// </summary>
        /// <value>
        /// The high.
        /// </value>
        double High { get; set; }
        /// <summary>
        /// Gets or sets the close.
        /// </summary>
        /// <value>
        /// The close.
        /// </value>
        double Close { get; set; }
        /// <summary>
        /// Gets or sets the low.
        /// </summary>
        /// <value>
        /// The low.
        /// </value>
        double Low { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        double Width { get; set; }
        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        /// <value>
        /// The left.
        /// </value>
        double Left { get; set; }
        /// <summary>
        /// Gets or sets the start reference.
        /// </summary>
        /// <value>
        /// The start reference.
        /// </value>
        double StartReference { get; set; }
    }
}