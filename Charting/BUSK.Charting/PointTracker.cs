using System.Collections.Generic;
using BUSK.Charting.Dtos;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    public class PointTracker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointTracker"/> class.
        /// </summary>
        public PointTracker()
        {
            Indexed = new Dictionary<int, ChartPoint>();
            Referenced = new Dictionary<object, ChartPoint>();
        }

        /// <summary>
        /// Gets the x limit.
        /// </summary>
        /// <value>
        /// The x limit.
        /// </value>
        public CoreLimit XLimit { get; internal set; }
        /// <summary>
        /// Gets the y limit.
        /// </summary>
        /// <value>
        /// The y limit.
        /// </value>
        public CoreLimit YLimit { get; internal set; }
        /// <summary>
        /// Gets the w limit.
        /// </summary>
        /// <value>
        /// The w limit.
        /// </value>
        public CoreLimit WLimit { get; internal set; }
        /// <summary>
        /// Gets the gci.
        /// </summary>
        /// <value>
        /// The gci.
        /// </value>
        public int Gci { get; internal set; }
        /// <summary>
        /// Gets or sets the indexed.
        /// </summary>
        /// <value>
        /// The indexed.
        /// </value>
        public Dictionary<int, ChartPoint> Indexed { get; set; }
        /// <summary>
        /// Gets or sets the referenced.
        /// </summary>
        /// <value>
        /// The referenced.
        /// </value>
        public Dictionary<object, ChartPoint> Referenced { get; set; }
    }
}