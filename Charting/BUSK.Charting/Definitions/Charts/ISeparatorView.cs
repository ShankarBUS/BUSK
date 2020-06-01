namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISeparatorView
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }
        /// <summary>
        /// Gets or sets separator step, this means the value between each line, use double.NaN for auto.
        /// </summary>
        double Step { get; set; }
        /// <summary>
        /// Gets the axis orientation.
        /// </summary>
        /// <value>
        /// The axis orientation.
        /// </value>
        AxisOrientation AxisOrientation { get; }

        /// <summary>
        /// Ases the core element.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        SeparatorConfigurationCore AsCoreElement(AxisCore axis, AxisOrientation source);
    }
}