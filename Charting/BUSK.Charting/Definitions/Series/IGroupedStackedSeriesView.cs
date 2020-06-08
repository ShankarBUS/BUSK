namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface IGroupedStackedSeriesView : ISeriesView
    {
        /// <summary>
        /// Gets or sets the column grouping.
        /// </summary>
        /// <value>
        /// The column grouping.
        /// </value>
        object Grouping { get; set; }
    }
}
