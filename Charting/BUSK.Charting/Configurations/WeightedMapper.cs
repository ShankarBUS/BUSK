using System;

namespace BUSK.Charting.Configurations
{
    /// <summary>
    /// Mapper to configure Bubble points
    /// </summary>
    /// <typeparam name="T">type to configure</typeparam>
    public class WeightedMapper<T> : IPointEvaluator<T>
    {
        private Func<T, int, double> _x = (v, i) => i;
        private Func<T, int, double> _y = (v, i) => i;
        private Func<T, int, double> _weight = (v, i) => 0;
        private Func<T, int, object> _stroke;
        private Func<T, int, object> _fill;

        /// <summary>
        /// Sets values for a specific point
        /// </summary>
        /// <param name="point">Point to set</param>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public void Evaluate(int key, T value, ChartPoint point)
        {
            point.X = _x(value, key);
            point.Y = _y(value, key);
            point.Weight = _weight(value, key);
            if (_stroke != null) point.Stroke = _stroke(value, key);
            if (_fill != null) point.Fill = _fill(value, key);
        }

        /// <summary>
        /// Sets the X mapper
        /// </summary>
        /// <param name="predicate">function that pulls the X coordinate</param>
        /// <returns>current mapper instance</returns>
        public WeightedMapper<T> X(Func<T, double> predicate)
        {
            return X((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets the X mapper
        /// </summary>
        /// <param name="predicate">function that pulls the X coordinate, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public WeightedMapper<T> X(Func<T, int, double> predicate)
        {
            _x = predicate;
            return this;
        }

        /// <summary>
        /// Sets the Y mapper
        /// </summary>
        /// <param name="predicate">function that pulls the Y coordinate</param>
        /// <returns>current mapper instance</returns>
        public WeightedMapper<T> Y(Func<T, double> predicate)
        {
            return Y((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets the Y mapper
        /// </summary>
        /// <param name="predicate">function that pulls the Y coordinate, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public WeightedMapper<T> Y(Func<T, int, double> predicate)
        {
            _y = predicate;
            return this;
        }

        /// <summary>
        /// Sets Weight mapper
        /// </summary>
        /// <param name="predicate">function that pulls the point's weight</param>
        /// <returns>current mapper instance</returns>
        public WeightedMapper<T> Weight(Func<T, double> predicate)
        {
            return Weight((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets Weight mapper
        /// </summary>
        /// <param name="predicate">function that pulls the point's weight, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public WeightedMapper<T> Weight(Func<T, int, double> predicate)
        {
            _weight = predicate;
            return this;
        }

        /// <summary>
        /// Sets the Stroke of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public WeightedMapper<T> Stroke(Func<T, object> predicate)
        {
            return Stroke((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets the Stroke of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public WeightedMapper<T> Stroke(Func<T, int, object> predicate)
        {
            _stroke = predicate;
            return this;
        }

        /// <summary>
        /// Sets the Fill of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public WeightedMapper<T> Fill(Func<T, object> predicate)
        {
            return Fill((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets the Fill of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public WeightedMapper<T> Fill(Func<T, int, object> predicate)
        {
            _fill = predicate;
            return this;
        }
    }
}