namespace BUSK.Charting.Dtos
{

    /// <summary>
    /// 
    /// </summary>
    public class BezierData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BezierData"/> class.
        /// </summary>
        public BezierData()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BezierData"/> class.
        /// </summary>
        /// <param name="point">The point.</param>
        public BezierData(CorePoint point)
        {
            Point1 = point;
            Point2 = point;
            Point3 = point;
        }

        /// <summary>
        /// Gets or sets the point1.
        /// </summary>
        /// <value>
        /// The point1.
        /// </value>
        public CorePoint Point1 { get; set; }

        /// <summary>
        /// Gets or sets the point2.
        /// </summary>
        /// <value>
        /// The point2.
        /// </value>
        public CorePoint Point2 { get; set; }
        /// <summary>
        /// Gets or sets the point3.
        /// </summary>
        /// <value>
        /// The point3.
        /// </value>
        public CorePoint Point3 { get; set; }
        /// <summary>
        /// Gets or sets the start point.
        /// </summary>
        /// <value>
        /// The start point.
        /// </value>
        public CorePoint StartPoint { get; set; }
    }
}
