using System;

namespace BUSK.Charting.Configurations
{
    /// <summary>
    /// Mapper to configure polar series
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PolarMapper<T> : IPointEvaluator<T>
    {
        private Func<T, int, double> _r;
        private Func<T, int, double> _angle;
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
            point.Radius = _r(value, key);
            point.Angle = _angle(value, key);
            if (_stroke != null) point.Stroke = _stroke(value, key);
            if (_fill != null) point.Fill = _fill(value, key);
        }

        /// <summary>
        /// Maps X value
        /// </summary>
        /// <param name="predicate">function that pulls the radius value</param>
        /// <returns>current mapper instance</returns>
        public PolarMapper<T> Radius(Func<T, double> predicate)
        {
            return Radius((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps X value
        /// </summary>
        /// <param name="predicate">function that pulls the radius value, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public PolarMapper<T> Radius(Func<T, int, double> predicate)
        {
            _r = predicate;
            return this;
        }

        /// <summary>
        /// Maps Y value
        /// </summary>
        /// <param name="predicate">function that pulls the angle value</param>
        /// <returns>current mapper instance</returns>
        public PolarMapper<T> Angle(Func<T, double> predicate)
        {
            return Angle((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps Y value
        /// </summary>
        /// <param name="predicate">function that pulls the angle value, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public PolarMapper<T> Angle(Func<T, int, double> predicate)
        {
            _angle = predicate;
            return this;
        }

        /// <summary>
        /// Sets the Stroke of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public PolarMapper<T> Stroke(Func<T, object> predicate)
        {
            return Stroke((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets the Stroke of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public PolarMapper<T> Stroke(Func<T, int, object> predicate)
        {
            _stroke = predicate;
            return this;
        }

        /// <summary>
        /// Sets the Fill of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public PolarMapper<T> Fill(Func<T, object> predicate)
        {
            return Fill((t, i) => predicate(t));
        }

        /// <summary>
        /// Sets the Fill of the point
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public PolarMapper<T> Fill(Func<T, int, object> predicate)
        {
            _fill = predicate;
            return this;
        }
    }
}