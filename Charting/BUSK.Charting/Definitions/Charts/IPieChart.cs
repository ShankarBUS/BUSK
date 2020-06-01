namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Charts.IChartView" />
    public interface IPieChart : IChartView
    {
        /// <summary>
        /// Gets or sets the inner radius.
        /// </summary>
        /// <value>
        /// The inner radius.
        /// </value>
        double InnerRadius { get; set; }
        /// <summary>
        /// Gets or sets the starting rotation angle.
        /// </summary>
        /// <value>
        /// The starting rotation angle.
        /// </value>
        double StartingRotationAngle { get; set; }
        /// <summary>
        /// Gets or sets the hover push out.
        /// </summary>
        /// <value>
        /// The hover push out.
        /// </value>
        double HoverPushOut { get; set; }
    }
}