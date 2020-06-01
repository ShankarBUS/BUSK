namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface IRowSeriesView : ISeriesView
    {
        /// <summary>
        /// Gets or sets the maximum row heigth.
        /// </summary>
        /// <value>
        /// The maximum row heigth.
        /// </value>
        double MaxRowHeigth { get; set; }
        /// <summary>
        /// Gets or sets the row padding.
        /// </summary>
        /// <value>
        /// The row padding.
        /// </value>
        double RowPadding { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [shares position].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [shares position]; otherwise, <c>false</c>.
        /// </value>
        bool SharesPosition { get; set; }
    }
}