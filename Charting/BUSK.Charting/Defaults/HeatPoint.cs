namespace BUSK.Charting.Defaults
{
    /// <summary>
    /// An already configured weighted chart point, this class notifies the chart to update every time a property changes
    /// </summary>
    public class HeatPoint : ScatterPoint
    {
        /// <summary>
        /// Initializes a new instance of HeatPoint class
        /// </summary>
        public HeatPoint()
        {

        }
        
        /// <summary>
        /// _initializes a new instance of HeatPoint class, giving x, y and weight
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="weight"></param>
        public HeatPoint(double x, double y, double weight)
        {
            X = x;
            Y = y;
            Weight = weight;
        }
    }
}