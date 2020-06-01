using System.Collections.Generic;
using System.ComponentModel;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public interface IChartLegend : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the series.
        /// </summary>
        /// <value>
        /// The series.
        /// </value>
        List<SeriesViewModel> Series { get; set; }
    }
}