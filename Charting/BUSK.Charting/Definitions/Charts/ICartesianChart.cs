namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Charts.IChartView" />
    public interface ICartesianChart : IChartView
    {
        /// <summary>
        /// Gets or sets the visual elements.
        /// </summary>
        /// <value>
        /// The visual elements.
        /// </value>
        VisualElementsCollection VisualElements { get; set; }
    }
}