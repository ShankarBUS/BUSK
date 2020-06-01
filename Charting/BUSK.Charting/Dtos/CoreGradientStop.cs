namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// Portable color gradient stop
    /// </summary>
    public struct CoreGradientStop
    {
        /// <summary>
        /// Offset, goes from 0 to 1
        /// </summary>
        public double Offset { get; set; }
        /// <summary>
        /// Color at Offset
        /// </summary>
        public CoreColor Color { get; set; }
    }
}