using System;

namespace BUSK.Charting
{
    /// <summary>
    /// Describes where a label should be placed
    /// </summary>
    public enum BarLabelPosition
    {
        /// <summary>
        /// Places a label at the top of a bar
        /// </summary>
        Top,
        /// <summary>
        /// Places a labels inside the bar
        /// </summary>
        [Obsolete("Instead use BarLabelPosition.Parallel")]
        Merged,
        /// <summary>
        /// Places a labels in a parallel orientation to the bar height.
        /// </summary>
        Parallel,
        /// <summary>
        /// Places a labels in a perpendicular orientation to the bar height.
        /// </summary>
        Perpendicular
    }
}