namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// Defines a portable limit
    /// </summary>
    public struct CoreLimit
    {
        /// <summary>
        /// Initializes a new instance of CoreLimit
        /// </summary>
        /// <param name="min">minimum value</param>
        /// <param name="max">maximum value</param>
        public CoreLimit(double min, double max) : this()
        {
            Max = max;
            Min = min;
        }

        /// <summary>
        /// Gets or sets the maximum value
        /// </summary>
        public double Max { get; set; }
        /// <summary>
        /// Gets or sets the minimum value
        /// </summary>
        public double Min { get; set; }
        /// <summary>
        /// Gets the range between max and min values
        /// </summary>
        public double Range { get { return Max - Min; } }
    }
}