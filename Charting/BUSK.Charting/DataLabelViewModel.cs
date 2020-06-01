namespace BUSK.Charting
{
    /// <summary>
    /// Describes a data label view model
    /// </summary>
    public class DataLabelViewModel
    {
        /// <summary>
        /// Gets or sets the formatted text of the current point
        /// </summary>
        /// <value>
        /// The formatted text.
        /// </value>
        public string FormattedText { get; set; }
        /// <summary>
        /// Gets the instance of the current point.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public ChartPoint Point { get; internal set; }

    }
}
