namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface IScatterSeriesView : ISeriesView
    {
        /// <summary>
        /// Gets or sets the maximum point shape diameter.
        /// </summary>
        /// <value>
        /// The maximum point shape diameter.
        /// </value>
        double MaxPointShapeDiameter { get; set; }
        /// <summary>
        /// Gets or sets the minimum point shape diameter.
        /// </summary>
        /// <value>
        /// The minimum point shape diameter.
        /// </value>
        double MinPointShapeDiameter { get; set; }
    }
}