namespace BUSK.Charting.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Events.RangeChangedEventArgs" />
    public class PreviewRangeChangedEventArgs : RangeChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewRangeChangedEventArgs"/> class.
        /// </summary>
        public PreviewRangeChangedEventArgs()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewRangeChangedEventArgs"/> class.
        /// </summary>
        /// <param name="args">The <see cref="RangeChangedEventArgs"/> instance containing the event data.</param>
        public PreviewRangeChangedEventArgs(RangeChangedEventArgs args)
        {
            LeftLimitChange = args.LeftLimitChange;
            RightLimitChange = args.RightLimitChange;
            Range = args.Range;
            Axis = args.Axis;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the axis change was canceled by the user.
        /// </summary>
        /// <value>
        ///   <c>true</c> if cancel; otherwise, <c>false</c>.
        /// </value>
        public bool Cancel { get; set; }
        /// <summary>
        /// Gets the preview minimum value.
        /// </summary>
        /// <value>
        /// The preview minimum value.
        /// </value>
        public double PreviewMinValue { get; internal set; }
        /// <summary>
        /// Gets the preview maximum value.
        /// </summary>
        /// <value>
        /// The preview maximum value.
        /// </value>
        public double PreviewMaxValue { get; internal set; }
    }
}