namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.IGroupedStackedSeriesView" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.IStackModelableSeriesView" />
    public interface IStackedRowSeriesView : IGroupedStackedSeriesView, IStackModelableSeriesView
    {
        /// <summary>
        /// Gets or sets the maximum height of the row.
        /// </summary>
        /// <value>
        /// The maximum height of the row.
        /// </value>
        double MaxRowHeight { get; set; }
        /// <summary>
        /// Gets or sets the row padding.
        /// </summary>
        /// <value>
        /// The row padding.
        /// </value>
        double RowPadding { get; set; }
    }
}