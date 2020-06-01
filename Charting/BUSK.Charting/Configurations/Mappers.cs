namespace BUSK.Charting.Configurations
{
    /// <summary>
    /// Gets the already built point mappers
    /// </summary>
    public static class Mappers
    {
        /// <summary>
        /// Gets a mapper to configure X, Y points
        /// </summary>
        /// <typeparam name="T">Type to map</typeparam>
        /// <returns>A new cartesian mapper instance</returns>
        public static CartesianMapper<T> Xy<T>()
        {
            return new CartesianMapper<T>();
        }

        /// <summary>
        /// Gets a mapper to configure financial points
        /// </summary>
        /// <typeparam name="T">type to map</typeparam>
        /// <returns>a new financial mapper instance</returns>
        public static FinancialMapper<T> Financial<T>()
        {
            return new FinancialMapper<T>();
        }

        /// <summary>
        /// Gets a mapper to configure X, Y and Weight points
        /// </summary>
        /// <typeparam name="T">type to map</typeparam>
        /// <returns>a new weighted mapper instance</returns>
        public static WeightedMapper<T> Weighted<T>()
        {
            return new WeightedMapper<T>();
        }

        /// <summary>
        /// Gets a Gantt Mapper
        /// </summary>
        /// <typeparam name="T">type to amp</typeparam>
        /// <returns>a new polar mapper insance</returns>
        public static GanttMapper<T> Gantt<T>()
        {
            return new GanttMapper<T>();
        }

        /// <summary>
        /// Gets a mapper to configure Radius and Angle
        /// </summary>
        /// <typeparam name="T">type to amp</typeparam>
        /// <returns>a new polar mapper insance</returns>
        public static PolarMapper<T> Polar<T>()
        {
            return new PolarMapper<T>();
        }

        /// <summary>
        /// PGets a mapper to configure a pie chart
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static PieMapper<T> Pie<T>()
        {
            return new PieMapper<T>();
        }
    }
}