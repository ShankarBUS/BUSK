namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// Defines a portable margin
    /// </summary>
    public class CoreMargin
    {
        /// <summary>
        /// Distance to top
        /// </summary>
        public double Top { get; set; }
        /// <summary>
        /// Distance to bottom
        /// </summary>
        public double Bottom { get; set; }
        /// <summary>
        /// Distance to left
        /// </summary>
        public double Left { get; set; }
        /// <summary>
        /// Distance to right
        /// </summary>
        public double Right { get; set; }
        /// <summary>
        /// Size width
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Size height
        /// </summary>
        public double Height { get; set; }
    }
}