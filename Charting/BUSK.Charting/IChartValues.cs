using System.Collections.Generic;
using BUSK.Charting.Definitions.Series;
using BUSK.Charting.Helpers;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Helpers.INoisyCollection" />
    public interface IChartValues : INoisyCollection
    {
        /// <summary>
        /// Forces values to calculate max, min and index data.
        /// </summary>
        void Initialize(ISeriesView seriesView);

        /// <summary>
        /// Gets the current chart points in the view, the view is required as an argument, because an instance of IChartValues could hold many ISeriesView instances.
        /// </summary>
        /// <param name="seriesView">The series view</param>
        /// <returns></returns>
        IEnumerable<ChartPoint> GetPoints(ISeriesView seriesView);
        /// <summary>
        /// Initializes the garbage collector
        /// </summary>
        void InitializeStep(ISeriesView seriesView);
        /// <summary>
        /// Removes all unnecessary points from the view
        /// </summary>
        void CollectGarbage(ISeriesView seriesView);

        /// <summary>
        /// Gets series that owns the values
        /// </summary>
        PointTracker GetTracker(ISeriesView view);
    }
}