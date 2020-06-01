using System;

namespace BUSK.Charting.Configurations
{
    /// <summary>
    /// Mapper to configure financial points
    /// </summary>
    /// <typeparam name="T">type to configure</typeparam>
    public class FinancialMapper<T> : IPointEvaluator<T>
    {
        private Func<T, int, double> _x = (v, i) => i;
        private Func<T, int, double> _y = (v, i) => 0;
        private Func<T, int, double> _open;
        private Func<T, int, double> _high;
        private Func<T, int, double> _low;
        private Func<T, int, double> _close;

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
            point.Open = _open(value, key);
            point.High = _high(value, key);
            point.Close = _close(value, key);
            point.Low = _low(value, key);
        }

        /// <summary>
        /// Maps X value
        /// </summary>
        /// <param name="predicate">function that pulls X coordinate</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> X(Func<T, double> predicate)
        {
            return X((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps X value
        /// </summary>
        /// <param name="predicate">function that pulls X coordinate, with value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> X(Func<T, int, double> predicate)
        {
            _x = predicate;
            return this;
        }

        /// <summary>
        /// Maps Y value
        /// </summary>
        /// <param name="predicate">function that pulls Y coordinate</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Y(Func<T, double> predicate)
        {
            return Y((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps Y value
        /// </summary>
        /// <param name="predicate">function that pulls Y coordinate, with value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Y(Func<T, int, double> predicate)
        {
            _y = predicate;
            return this;
        }

        /// <summary>
        /// Maps Open value
        /// </summary>
        /// <param name="predicate">function that pulls open value</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Open(Func<T, double> predicate)
        {
            return Open((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps Open value
        /// </summary>
        /// <param name="predicate">function that pulls open value, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Open(Func<T, int, double> predicate)
        {
            _open = predicate;
            return this;
        }

        /// <summary>
        /// Maps High value
        /// </summary>
        /// <param name="predicate">function that pulls High value</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> High(Func<T, double> predicate)
        {
            return High((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps High value
        /// </summary>
        /// <param name="predicate">function that pulls High value</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> High(Func<T, int, double> predicate)
        {
            _high = predicate;
            return this;
        }

        /// <summary>
        /// Maps Close value
        /// </summary>
        /// <param name="predicate">function that pulls close value</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Close(Func<T, double> predicate)
        {
            return Close((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps Close value
        /// </summary>
        /// <param name="predicate">function that pulls close value, value and index as parameters</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Close(Func<T, int, double> predicate)
        {
            _close = predicate;
            return this;
        }

        /// <summary>
        /// Maps Low value
        /// </summary>
        /// <param name="predicate">function that pulls low value</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Low(Func<T, double> predicate)
        {
            return Low((t, i) => predicate(t));
        }
        /// <summary>
        /// Maps Low value
        /// </summary>
        /// <param name="predicate">function that pulls low value, index and value as parameters</param>
        /// <returns>current mapper instance</returns>
        public FinancialMapper<T> Low(Func<T, int, double> predicate)
        {
            _low = predicate;
            return this;
        }
    }
}
