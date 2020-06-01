using System.Collections.Generic;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Definitions.Series.ISeriesView" />
    public interface IHeatSeriesView : ISeriesView
    {
        /// <summary>
        /// Gets the stops.
        /// </summary>
        /// <value>
        /// The stops.
        /// </value>
        IList<CoreGradientStop> Stops { get; }
        /// <summary>
        /// Gets a value indicating whether [draws heat range].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [draws heat range]; otherwise, <c>false</c>.
        /// </value>
        bool DrawsHeatRange { get; }
    }
}