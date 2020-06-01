namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    public class SeparatorConfigurationCore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatorConfigurationCore"/> class.
        /// </summary>
        /// <param name="axis">The axis.</param>
        public SeparatorConfigurationCore(AxisCore axis)
        {
            Axis = axis;
        }

        /// <summary>
        /// Gets or sets the axis.
        /// </summary>
        /// <value>
        /// The axis.
        /// </value>
        public AxisCore Axis { get; set; }

        /// <summary>
        /// Gets or sets if separators are enabled (will be drawn)
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets sepator step, this means the value between each line, use null for auto.
        /// </summary>
        public double Step { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public AxisOrientation Source { get; set; }
    }
}