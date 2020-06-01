namespace BUSK.Charting.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class RangeChangedEventArgs
    {
        /// <summary>
        /// Gets the min limit difference compared with previous state
        /// </summary>
        public double LeftLimitChange { get; internal set; }
        /// <summary>
        /// Gets the max limit difference compared with previous state
        /// </summary>
        public double RightLimitChange { get; internal set; }
        /// <summary>
        /// Gets the current axis range
        /// </summary>
        public double Range { get; internal set; }
        /// <summary>
        /// Gets the axis that fired the change
        /// </summary>
        public object Axis { get; internal set; }
    }
}
