using System.Windows;
using System.Windows.Media;

namespace BUSK.Charting.WPF
{
    public partial class HeatColorRange
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeatColorRange"/> class.
        /// </summary>
        public HeatColorRange()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Updates the fill.
        /// </summary>
        /// <param name="stops">The stops.</param>
        public void UpdateFill(GradientStopCollection stops)
        {
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = stops
            };
        }

        /// <summary>
        /// Sets the maximum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public double SetMax(string value)
        {
            MaxVal.Text = value;
            MaxVal.UpdateLayout();
            return MaxVal.ActualWidth;
        }

        /// <summary>
        /// Sets the minimum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public double SetMin(string value)
        {
            MinVal.Text = value;
            MinVal.UpdateLayout();
            return MinVal.ActualWidth;
        }
    }
}
