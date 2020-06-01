using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Points
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Points.IChartPointView" />
    public interface IHeatPointView : IChartPointView
    {
        /// <summary>
        /// Gets or sets the color components.
        /// </summary>
        /// <value>
        /// The color components.
        /// </value>
        CoreColor ColorComponents { get; set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        double Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        double Height { get; set; }
    }
}