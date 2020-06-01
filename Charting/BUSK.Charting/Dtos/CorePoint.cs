namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public struct CorePoint
    {
        /// <summary>
        /// Initializes a new instance of CorePoint
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        public CorePoint(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance of CorePoint
        /// </summary>
        /// <param name="point">source pont</param>
        public CorePoint(CorePoint point) : this()
        {
            X = point.X;
            Y = point.Y;
        }

        /// <summary>
        /// X coordinate
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y coordinate
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Sums every property between 2 given points
        /// </summary>
        /// <param name="p1">point 1</param>
        /// <param name="p2">point 2</param>
        /// <returns></returns>
        public static CorePoint operator +(CorePoint p1, CorePoint p2)
        {
            return new CorePoint(p1.X + p2.X, p1.Y + p2.Y);
        }

        /// <summary>
        /// Subtracts every property between 2 given points
        /// </summary>
        /// <param name="p1">point 1</param>
        /// <param name="p2">point 2</param>
        /// <returns></returns>
        public static CorePoint operator -(CorePoint p1, CorePoint p2)
        {
            return new CorePoint(p1.X - p2.X, p1.Y - p2.Y);
        }
    }
}