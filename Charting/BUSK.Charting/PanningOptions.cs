namespace BUSK.Charting
{
    /// <summary>
    /// Chart Panning Options
    /// </summary>
    public enum PanningOptions
    {
        /// <summary>
        /// By default chart Panning is Unset, this means it will be based the Chart Zooming selection
        /// </summary>
        Unset,
        /// <summary>
        /// Not panning allowed
        /// </summary>
        None,
        /// <summary>
        /// Panning only in the X axis
        /// </summary>
        X,
        /// <summary>
        /// Panning only in the Y axis
        /// </summary>
        Y,
        /// <summary>
        /// Panning in both X and Y axes
        /// </summary>
        Xy
    }
}