using System.Collections.Generic;
using BUSK.Charting.Helpers;
using BUSK.Charting.WPF.Charts.Base;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// Stores a collection of axis.
    /// </summary>
    public class AxesCollection : NoisyCollection<Axis>
    {
        /// <summary>
        /// Initializes a new instance of AxisCollection class
        /// </summary>
        public AxesCollection()
        {
            NoisyCollectionChanged += OnNoisyCollectionChanged;
        }

        /// <summary>
        /// Gets the chart that owns the series.
        /// </summary>
        /// <value>
        /// The chart.
        /// </value>
        public Chart Chart { get; internal set; }

        private void OnNoisyCollectionChanged(IEnumerable<Axis> oldItems, IEnumerable<Axis> newItems)
        {
            if(Chart != null && Chart.Model != null)
                Chart.Model.Updater.Run();

            if (oldItems == null) return;

            foreach (var oldAxis in oldItems)
            {
                oldAxis.Clean();
                if (oldAxis.Model == null) continue;
                var chart = oldAxis.Model.Chart.View;
                if (chart == null) continue;
                chart.RemoveFromView(oldAxis);
                chart.RemoveFromView(oldAxis.Separator);
            }
        }
    }
}
