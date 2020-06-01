namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface IPieSeriesView : ISeriesView
    {
        /// <summary>
        /// Gets or sets the push out.
        /// </summary>
        /// <value>
        /// The push out.
        /// </value>
        double PushOut { get; set; }
    }
}