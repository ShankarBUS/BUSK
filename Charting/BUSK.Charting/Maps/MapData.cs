using System.Collections.Generic;

namespace BUSK.Charting.Maps
{
    /// <summary>
    /// 
    /// </summary>
    public class LvcMap
    {
        /// <summary>
        /// Gets or sets the width of the desired.
        /// </summary>
        /// <value>
        /// The width of the desired.
        /// </value>
        public double DesiredWidth { get; set; }
        /// <summary>
        /// Gets or sets the height of the desired.
        /// </summary>
        /// <value>
        /// The height of the desired.
        /// </value>
        public double DesiredHeight { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<MapData> Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MapData
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public string Data { get; set; }
        /// <summary>
        /// Gets or sets the shape.
        /// </summary>
        /// <value>
        /// The shape.
        /// </value>
        public object Shape { get; set; }
        /// <summary>
        /// Gets or sets the LVC map.
        /// </summary>
        /// <value>
        /// The LVC map.
        /// </value>
        public LvcMap LvcMap { get; set; }
    }
}