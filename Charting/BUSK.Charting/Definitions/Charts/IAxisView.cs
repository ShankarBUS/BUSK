using System;
using BUSK.Charting.Charts;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAxisView
    {
        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        AxisCore Model { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [disable animations].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [disable animations]; otherwise, <c>false</c>.
        /// </value>
        bool DisableAnimations { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show labels].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show labels]; otherwise, <c>false</c>.
        /// </value>
        bool ShowLabels { get; set; }
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        double MaxValue { get; set; }
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        double MinValue { get; set; }
        /// <summary>
        /// Gets or sets the minimum range.
        /// </summary>
        /// <value>
        /// The minimum range.
        /// </value>
        double MinRange { get; set; }
        /// <summary>
        /// Gets or sets the maximum range.
        /// </summary>
        /// <value>
        /// The maximum range.
        /// </value>
        double MaxRange { get; set; }
        /// <summary>
        /// Gets or sets the labels rotation.
        /// </summary>
        /// <value>
        /// The labels rotation.
        /// </value>
        double LabelsRotation { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is merged.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is merged; otherwise, <c>false</c>.
        /// </value>
        bool IsMerged { get; set; }
        /// <summary>
        /// Gets or sets the bar unit.
        /// </summary>
        /// <value>
        /// The bar unit.
        /// </value>
        double Unit { get; set; }
        /// <summary>
        /// Gets or sets the bar unit.
        /// </summary>
        /// <value>
        /// The bar unit.
        /// </value>
        [Obsolete]
        double BarUnit { get; set; }
        /// <summary>
        /// Gets the previous maximum value.
        /// </summary>
        /// <value>
        /// The previous maximum value.
        /// </value>
        double PreviousMaxValue { get; }
        /// <summary>
        /// Gets the previous minimum value.
        /// </summary>
        /// <value>
        /// The previous minimum value.
        /// </value>
        double PreviousMinValue { get; }
        /// <summary>
        /// Gets the axis orientation.
        /// </summary>
        /// <value>
        /// The axis orientation.
        /// </value>
        AxisOrientation AxisOrientation { get; }

        /// <summary>
        /// Updates the title.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="rotationAngle">The rotation angle.</param>
        /// <returns></returns>
        CoreSize UpdateTitle(ChartCore chart, double rotationAngle = 0);
        /// <summary>
        /// Sets the title top.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetTitleTop(double value);
        /// <summary>
        /// Sets the title left.
        /// </summary>
        /// <param name="value">The value.</param>
        void SetTitleLeft(double value);
        /// <summary>
        /// Gets the title left.
        /// </summary>
        /// <returns></returns>
        double GetTitleLeft();
        /// <summary>
        /// Gets the tile top.
        /// </summary>
        /// <returns></returns>
        double GetTileTop();
        /// <summary>
        /// Gets the size of the label.
        /// </summary>
        /// <returns></returns>
        CoreSize GetLabelSize();
        /// <summary>
        /// Ases the core element.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        AxisCore AsCoreElement(ChartCore chart, AxisOrientation source);
        /// <summary>
        /// Renders the separator.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="chart">The chart.</param>
        void RenderSeparator(SeparatorElementCore model, ChartCore chart);
        /// <summary>
        /// Cleans this instance.
        /// </summary>
        void Clean();
        /// <summary>
        /// Sets the range.
        /// </summary>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        void SetRange(double min, double max);
    }
}