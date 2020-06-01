using System.Collections.Generic;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Series;
using BUSK.Charting.Helpers;

namespace BUSK.Charting
{
    /// <summary>
    /// Stores a collection of series to plot, this collection notifies the changes every time you add/remove any series.
    /// </summary>
    public class SeriesCollection : NoisyCollection<ISeriesView>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SeriesCollection class
        /// </summary>
        public SeriesCollection()
        {
            NoisyCollectionChanged += OnNoisyCollectionChanged;
            CollectionReset += OnCollectionReset;
        }

        /// <summary>
        /// Initializes a new instance of the SeriesCollection class, with a given mapper
        /// </summary>
        public SeriesCollection(object configuration)
        {
            Configuration = configuration;

            NoisyCollectionChanged += OnNoisyCollectionChanged;
            CollectionReset += OnCollectionReset;
        }

        #endregion

        /// <summary>
        /// Gets or sets the current series index, this index is used to pull out the automatic color of any series
        ///  </summary>
        public int CurrentSeriesIndex { get; set; }

        /// <summary>
        /// Gets the chart that owns the collection
        /// </summary>
        public ChartCore Chart { get; internal set; }
        /// <summary>
        /// Gets or sets then mapper in the collection, this mapper will be used in any series inside the collection, if null then BUSK.Charting will try to get the value from the global configuration.
        /// </summary>
        public object Configuration { get; set; }

        private void OnNoisyCollectionChanged(IEnumerable<ISeriesView> oldItems, IEnumerable<ISeriesView> newItems)
        {
            if (newItems != null)
            {
                foreach (var view in newItems)
                {
                    view.Model.SeriesCollection = this;
                    view.Model.Chart = Chart;
                }
            }

            if (oldItems != null)
            {
                foreach (var view in oldItems)
                {
                    view.Erase(true);
                }
            }
           
            if (Chart != null) Chart.Updater.Run();
        }

        private void OnCollectionReset()
        {
            CurrentSeriesIndex = 0;
        }
    }
}