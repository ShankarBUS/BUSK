namespace BUSK.Charting.Definitions.Points
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Points.IChartPointView" />
    public interface IScatterPointView : IChartPointView
    {
        /// <summary>
        /// Gets or sets the diameter.
        /// </summary>
        /// <value>
        /// The diameter.
        /// </value>
        double Diameter { get; set; }
    }
}