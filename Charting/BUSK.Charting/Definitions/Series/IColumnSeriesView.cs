namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface IColumnSeriesView : ISeriesView
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
        /// <summary>
        /// Gets or sets a value indicating whether [shares position].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [shares position]; otherwise, <c>false</c>.
        /// </value>
        bool SharesPosition { get; set; }
    }
}