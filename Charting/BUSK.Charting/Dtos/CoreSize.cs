namespace BUSK.Charting.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    public struct CoreSize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreSize"/> struct.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="heigth">The heigth.</param>
        public CoreSize(double width, double heigth) : this()
        {
            Width = width;
            Height = heigth;
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public double Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public double Height { get; set; }
    }
}