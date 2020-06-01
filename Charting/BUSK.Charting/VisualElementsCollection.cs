using System.Collections.Generic;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.Helpers;

namespace BUSK.Charting
{
    /// <summary>
    /// Defines a collection of items to be added in a cartesian chart
    /// </summary>
    public class VisualElementsCollection : NoisyCollection<ICartesianVisualElement>
    {
        /// <summary>
        /// Initializes a new instance of VisualElementsCollection
        /// </summary>
        public VisualElementsCollection()
        {
            NoisyCollectionChanged += OnNoisyCollectionChanged;
        }

        /// <summary>
        /// Gets or sets the chart.
        /// </summary>
        /// <value>
        /// The chart.
        /// </value>
        public ChartCore Chart { get; set; }

        private void OnNoisyCollectionChanged(IEnumerable<ICartesianVisualElement> oldItems, IEnumerable<ICartesianVisualElement> newItems)
        {
            if (oldItems != null) foreach (var oltItem in oldItems) oltItem.Remove(Chart);
            if (newItems != null) foreach (var newItem in newItems) newItem.AddOrMove(Chart);
        }
    }
}
