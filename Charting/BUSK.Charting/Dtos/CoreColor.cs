namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// Defines a portable color
    /// </summary>
    public struct CoreColor
    {
        /// <summary>
        /// Initializes a new instance of CoreColor
        /// </summary>
        /// <param name="a">alpha component</param>
        /// <param name="r">red component</param>
        /// <param name="g">green component</param>
        /// <param name="b">blue component</param>
        public CoreColor(byte a, byte r, byte g, byte b) : this()
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        /// <summary>
        /// Alpha component
        /// </summary>
        public byte A { get; set; }
        /// <summary>
        /// Red component
        /// </summary>
        public byte R { get; set; }
        /// <summary>
        /// Green component
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// Red component
        /// </summary>
        public byte B { get; set; }
    }
}