namespace BUSK.Charting
{
    /// <summary>
    /// Stacked mode, for stacked series
    /// </summary>
    public enum StackMode
    {
        /// <summary>
        /// Stacks the values, eg: if values are 1,2,3 the stacked total is 6
        /// </summary>
        Values,
        /// <summary>
        /// Stacks percentage, eg: if values are 1,2,3, they are actually being stacked as (1/6), (2/6), (3/6) [value/totalSum]
        /// </summary>
        Percentage
    }
}