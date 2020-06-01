namespace BUSK.Charting.Definitions.Points
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Points.IChartPointView" />
    public interface IStepPointView : IChartPointView
    {
        /// <summary>
        /// Gets or sets the delta x.
        /// </summary>
        /// <value>
        /// The delta x.
        /// </value>
        double DeltaX { get; set; }
        /// <summary>
        /// Gets or sets the delta y.
        /// </summary>
        /// <value>
        /// The delta y.
        /// </value>
        double DeltaY { get; set; }
    }
}