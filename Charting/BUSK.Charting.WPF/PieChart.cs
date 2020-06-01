using System;
using System.Windows;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.WPF.Charts.Base;
using BUSK.Charting.WPF.Points;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// The pie chart compares mainly the distribution of the data according to different series.
    /// </summary>
    public class PieChart : Chart, IPieChart
    {
        /// <summary>
        /// Initializes a new instance of PieChart class
        /// </summary>
        public PieChart()
        {
            var freq = DisableAnimations ? TimeSpan.FromMilliseconds(10) : AnimationsSpeed;
            var updater = new Components.ChartUpdater(freq);
            ChartCoreModel = new PieChartCore(this, updater);

            SetCurrentValue(SeriesProperty, new SeriesCollection());
        }

        /// <summary>
        /// The inner radius property
        /// </summary>
        public static readonly DependencyProperty InnerRadiusProperty = DependencyProperty.Register(
            "InnerRadius", typeof (double), typeof (PieChart), new PropertyMetadata(0d, CallChartUpdater()));
        /// <summary>
        /// Gets or sets the pie inner radius, increasing this property will result in a doughnut chart.
        /// </summary>
        public double InnerRadius
        {
            get { return (double) GetValue(InnerRadiusProperty); }
            set { SetValue(InnerRadiusProperty, value); }
        }

        /// <summary>
        /// The starting rotation angle property
        /// </summary>
        public static readonly DependencyProperty StartingRotationAngleProperty = DependencyProperty.Register(
            "StartingRotationAngle", typeof (double), typeof (PieChart), new PropertyMetadata(45d, CallChartUpdater()));
        /// <summary>
        /// Gets or sets the starting rotation angle in degrees.
        /// </summary>
        public double StartingRotationAngle
        {
            get { return (double) GetValue(StartingRotationAngleProperty); }
            set { SetValue(StartingRotationAngleProperty, value); }
        }

        /// <summary>
        /// The hover push out property
        /// </summary>
        public static readonly DependencyProperty HoverPushOutProperty = DependencyProperty.Register(
            "HoverPushOut", typeof (double), typeof (PieChart), new PropertyMetadata(5d));
        /// <summary>
        /// Gets or sets the units that a slice is pushed out when a user moves the mouse over data point.
        /// </summary>
        public double HoverPushOut
        {
            get { return (double) GetValue(HoverPushOutProperty); }
            set { SetValue(HoverPushOutProperty, value); }
        }

        /// <summary>
        /// Gets the tooltip position.
        /// </summary>
        /// <param name="senderPoint">The sender point.</param>
        /// <returns></returns>
        protected internal override Point GetTooltipPosition(ChartPoint senderPoint)
        {
            var pieSlice = ((PiePointView) senderPoint.View).Slice;

            var alpha = pieSlice.RotationAngle + pieSlice.WedgeAngle * .5 + 180;
            var alphaRad = alpha * Math.PI / 180;
            var sliceMidAngle = alpha - 180;

            DataTooltip.UpdateLayout();

            var y = DrawMargin.ActualHeight*.5 +
                    (sliceMidAngle > 90 && sliceMidAngle < 270 ? -1 : 0)*DataTooltip.ActualHeight -
                    Math.Cos(alphaRad)*15;
            var x = DrawMargin.ActualWidth*.5 +
                    (sliceMidAngle > 0 && sliceMidAngle < 180 ? -1 : 0)*DataTooltip.ActualWidth +
                    Math.Sin(alphaRad)*15;

            return new Point(x, y);
        }
    }
}
