using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Points
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Points.IChartPointView" />
    public interface IRectanglePointView : IChartPointView
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        CoreRectangle Data { get; set; }
        /// <summary>
        /// Gets or sets the zero reference.
        /// </summary>
        /// <value>
        /// The zero reference.
        /// </value>
        double ZeroReference { get; set; }
    }
}