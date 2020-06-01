using System;
using System.Collections.Generic;

namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public struct TooltipDataViewModel
    {
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public IEnumerable<ChartPoint> Points { get; set; }
        /// <summary>
        /// Gets or sets the shares.
        /// </summary>
        /// <value>
        /// The shares.
        /// </value>
        public double? Shares { get; set; }
        /// <summary>
        /// Gets or sets the x formatter.
        /// </summary>
        /// <value>
        /// The x formatter.
        /// </value>
        public Func<double, string> XFormatter { get; set; }
        /// <summary>
        /// Gets or sets the y formatter.
        /// </summary>
        /// <value>
        /// The y formatter.
        /// </value>
        public Func<double, string> YFormatter { get; set; }
    }
}