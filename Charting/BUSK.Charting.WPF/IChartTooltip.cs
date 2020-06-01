using System.ComponentModel;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IChartTooltip : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        TooltipData Data { get; set; }
        /// <summary>
        /// Gets or sets the selection mode.
        /// </summary>
        /// <value>
        /// The selection mode.
        /// </value>
        TooltipSelectionMode? SelectionMode { get; set; }
    }
}