using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Series;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SeriesAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeriesAlgorithm"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        protected SeriesAlgorithm(ISeriesView view)
        {
            View = view;
        }

        /// <summary>
        /// Gets or sets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public ISeriesView View { get; set; }
        /// <summary>
        /// Gets or sets the chart.
        /// </summary>
        /// <value>
        /// The chart.
        /// </value>
        public ChartCore Chart { get; set; }
        /// <summary>
        /// Gets or sets the series collection.
        /// </summary>
        /// <value>
        /// The series collection.
        /// </value>
        public SeriesCollection SeriesCollection { get; set; }
        /// <summary>
        /// Gets or sets the series orientation.
        /// </summary>
        /// <value>
        /// The series orientation.
        /// </value>
        public SeriesOrientation SeriesOrientation { get; set; }
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }
        /// <summary>
        /// Gets the preferred selection mode.
        /// </summary>
        /// <value>
        /// The preferred selection mode.
        /// </value>
        public TooltipSelectionMode PreferredSelectionMode { get; internal set; }

        /// <summary>
        /// Gets the current x axis.
        /// </summary>
        /// <value>
        /// The current x axis.
        /// </value>
        public AxisCore CurrentXAxis
        {
            get { return Chart.AxisX[View.ScalesXAt]; }
        }
        /// <summary>
        /// Gets the current y axis.
        /// </summary>
        /// <value>
        /// The current y axis.
        /// </value>
        public AxisCore CurrentYAxis
        {
            get { return Chart.AxisY[View.ScalesYAt]; }
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public abstract void Update();
    }
}
