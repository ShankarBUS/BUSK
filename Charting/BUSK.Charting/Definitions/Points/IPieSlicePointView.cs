namespace BUSK.Charting.Definitions.Points
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Points.IChartPointView" />
    public interface IPieSlicePointView : IChartPointView
    {
        /// <summary>
        /// Gets or sets the rotation.
        /// </summary>
        /// <value>
        /// The rotation.
        /// </value>
        double Rotation { get; set; }
        /// <summary>
        /// Gets or sets the wedge.
        /// </summary>
        /// <value>
        /// The wedge.
        /// </value>
        double Wedge { get; set; }
        /// <summary>
        /// Gets or sets the inner radius.
        /// </summary>
        /// <value>
        /// The inner radius.
        /// </value>
        double InnerRadius { get; set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        double Radius { get; set; }
    }
}