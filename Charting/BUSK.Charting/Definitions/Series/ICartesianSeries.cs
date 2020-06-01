namespace BUSK.Charting.Definitions.Series
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICartesianSeries
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        ISeriesView View { get; }

        /// <summary>
        /// Gets the minimum x.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        double GetMinX(AxisCore axis);
        /// <summary>
        /// Gets the maximum x.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        double GetMaxX(AxisCore axis);
        /// <summary>
        /// Gets the minimum y.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        double GetMinY(AxisCore axis);
        /// <summary>
        /// Gets the maximum y.
        /// </summary>
        /// <param name="axis">The axis.</param>
        /// <returns></returns>
        double GetMaxY(AxisCore axis);
    }
}