using BUSK.Charting.Charts;
using BUSK.Charting.Dtos;

namespace BUSK.Charting.Definitions.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISeparatorElementView
    {
        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        SeparatorElementCore Model { get; }
        /// <summary>
        /// Gets the label model.
        /// </summary>
        /// <value>
        /// The label model.
        /// </value>
        LabelEvaluation LabelModel { get; }

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="axis">The axis.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        LabelEvaluation UpdateLabel(string text, AxisCore axis, AxisOrientation source);

        /// <summary>
        /// Clears the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void Clear(IChartView chart);

        //No animated methods
        /// <summary>
        /// Places the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="axis">The axis.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="axisIndex">Index of the axis.</param>
        /// <param name="toLabel">To label.</param>
        /// <param name="toLine">To line.</param>
        /// <param name="tab">The tab.</param>
        void Place(ChartCore chart, AxisCore axis, AxisOrientation direction, int axisIndex, double toLabel, double toLine, double tab);
        /// <summary>
        /// Removes the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void Remove(ChartCore chart);

        //Animated methods
        /// <summary>
        /// Moves the specified chart.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="axis">The axis.</param>
        /// <param name="direction">The direction.</param>
        /// <param name="axisIndex">Index of the axis.</param>
        /// <param name="toLabel">To label.</param>
        /// <param name="toLine">To line.</param>
        /// <param name="tab">The tab.</param>
        void Move(ChartCore chart, AxisCore axis, AxisOrientation direction, int axisIndex, double toLabel, double toLine, double tab);
        /// <summary>
        /// Fades the in.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <param name="chart">The chart.</param>
        void FadeIn(AxisCore axis, ChartCore chart);
        /// <summary>
        /// Fades the out and remove.
        /// </summary>
        /// <param name="chart">The chart.</param>
        void FadeOutAndRemove(ChartCore chart);
    }
}