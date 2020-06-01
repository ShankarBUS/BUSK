using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface ILineSeriesView : ISeriesView
    {
        /// <summary>
        /// Gets or sets the line smoothness.
        /// </summary>
        /// <value>
        /// The line smoothness.
        /// </value>
        double LineSmoothness { get; set; }
        /// <summary>
        /// Gets or sets the area limit.
        /// </summary>
        /// <value>
        /// The area limit.
        /// </value>
        double AreaLimit { get; set; }
        /// <summary>
        /// Starts the segment.
        /// </summary>
        /// <param name="atIndex">At index.</param>
        /// <param name="location">The location.</param>
        void StartSegment(int atIndex, CorePoint location);
        /// <summary>
        /// Ends the segment.
        /// </summary>
        /// <param name="atIndex">At index.</param>
        /// <param name="location">The location.</param>
        void EndSegment(int atIndex, CorePoint location);
    }
}