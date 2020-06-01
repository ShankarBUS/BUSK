using System;
namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("Use INotifyPropertyChangedInstead")]
    public interface IObservableChartPoint
    {
        /// <summary>
        /// Occurs when [point changed].
        /// </summary>
        event Action PointChanged;
    }
}