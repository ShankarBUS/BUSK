namespace BUSK.Charting.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPointEvaluator<in T>
    {
        /// <summary>
        /// Evaluates the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="point">The point.</param>
        void Evaluate(int key, T value, ChartPoint point);
    }
}
