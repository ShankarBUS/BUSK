using System.Collections.Generic;
using BUSK.Charting.Helpers;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// The SectionsCollection class holds a collection of Axis.Sections
    /// </summary>
    public class SectionsCollection : NoisyCollection<AxisSection>
    {
        /// <summary>
        /// Initializes a new instance of SectionsCollection instance
        /// </summary>
        public SectionsCollection()
        {
            NoisyCollectionChanged += OnNoisyCollectionChanged;
        }

        private static void OnNoisyCollectionChanged(IEnumerable<AxisSection> oldItems, IEnumerable<AxisSection> newItems)
        {
            if (oldItems == null) return;

            foreach (var oldSection in oldItems)
            {
                oldSection.Remove();
            }
        }
    }
}
