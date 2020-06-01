using System;
using System.ComponentModel;
using System.Windows;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.WPF.Charts.Base;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// The Cartesian chart can plot any series with x and y coordinates
    /// </summary>
    public class CartesianChart : Chart, ICartesianChart
    {
        /// <summary>
        /// Initializes a new instance of CartesianChart class
        /// </summary>
        public CartesianChart()
        {
            var freq = DisableAnimations ? TimeSpan.FromMilliseconds(10) : AnimationsSpeed;
            var updater = new Components.ChartUpdater(freq);
            ChartCoreModel = new CartesianChartCore(this, updater);

            SetCurrentValue(SeriesProperty,
                DesignerProperties.GetIsInDesignMode(this)
                    ? GetDesignerModeCollection()
                    : new SeriesCollection());

            SetCurrentValue(VisualElementsProperty, new VisualElementsCollection());
        }

        /// <summary>
        /// The visual elements property
        /// </summary>
        public static readonly DependencyProperty VisualElementsProperty = DependencyProperty.Register(
            "VisualElements", typeof (VisualElementsCollection), typeof (CartesianChart),
            new PropertyMetadata(default(VisualElementsCollection), OnVisualCollectionChanged));

        /// <summary>
        /// Gets or sets the collection of visual elements in the chart, a visual element display another UiElement in the chart.
        /// </summary>
        public VisualElementsCollection VisualElements
        {
            get { return (VisualElementsCollection) GetValue(VisualElementsProperty); }
            set { SetValue(VisualElementsProperty, value); }
        }

        private static void OnVisualCollectionChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var chart = (CartesianChart)dependencyObject;

            if (chart.VisualElements != null) chart.VisualElements.Chart = chart.Model;
        }
    }
}
