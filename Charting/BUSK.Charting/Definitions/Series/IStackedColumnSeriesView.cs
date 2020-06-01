namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.IGroupedStackedSeriesView" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.IStackModelableSeriesView" />
    public interface IStackedColumnSeriesView : IGroupedStackedSeriesView, IStackModelableSeriesView
    {
        /// <summary>
        /// Gets or sets the maximum width of the column.
        /// </summary>
        /// <value>
        /// The maximum width of the column.
        /// </value>
        double MaxColumnWidth { get; set; }
        /// <summary>
        /// Gets or sets the column padding.
        /// </summary>
        /// <value>
        /// The column padding.
        /// </value>
        double ColumnPadding { get; set; }
    }
}