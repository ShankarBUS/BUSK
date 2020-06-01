using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    public class AxisSectionCore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AxisSectionCore"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="chart">The chart.</param>
        public AxisSectionCore(IAxisSectionView view, ChartCore chart)
        {
            View = view;
            Chart = chart;
        }
        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public IAxisSectionView View { get; set; }
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public AxisOrientation Source { get; set; }
        /// <summary>
        /// Gets or sets the index of the axis.
        /// </summary>
        /// <value>
        /// The index of the axis.
        /// </value>
        public int AxisIndex { get; set; }
        /// <summary>
        /// Gets or sets the chart.
        /// </summary>
        /// <value>
        /// The chart.
        /// </value>
        public ChartCore Chart { get; set; }
    }
}
