using System.Windows.Media;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// Contains an already defined collection of geometries, useful to set the Series.PointGeomety property
    /// </summary>
    public static class DefaultGeometries
    {
        /// <summary>
        /// Returns a null geometry
        /// </summary>
        public static Geometry None
        {
            get { return null; }
        }

        /// <summary>
        /// Returns a circle geometry
        /// </summary>
        public static Geometry Circle
        {
            get
            {
                var g = Geometry.Parse("M 0,0 A 180,180 180 1 1 1,1 Z");
                g.Freeze();
                return g;
            }
        }

        /// <summary>
        /// Returns a square geometry
        /// </summary>
        public static Geometry Square
        {
            get
            {
                var g = Geometry.Parse("M 1,1 h -2 v -2 h 2 z");
                g.Freeze();
                return g;
            }
        }

        /// <summary>
        /// Returns a diamond geometry
        /// </summary>
        public static Geometry Diamond
        {
            get
            {
                var g = Geometry.Parse("M 1,0 L 2,1  1,2  0,1 z");
                g.Freeze();
                return g;
            }
        }

        /// <summary>
        /// Returns a triangle geometry
        /// </summary>
        public static Geometry Triangle
        {
            get
            {
                var g = Geometry.Parse("M 0,1 l 1,1 h -2 Z");
                g.Freeze();
                return g;
            }
        }

        /// <summary>
        /// Returns a cross geometry
        /// </summary>
        public static Geometry Cross
        {
            get
            {
                var g = Geometry.Parse("M0,0 L1,1 M0,1 l1,-1");
                g.Freeze();
                return g;
            }
        }
    }
}
