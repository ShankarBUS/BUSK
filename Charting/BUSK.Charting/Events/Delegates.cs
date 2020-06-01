namespace BUSK.Charting.Events
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="chartPoint">The chart point.</param>
    public delegate void DataClickHandler(object sender, ChartPoint chartPoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="chartPoint"></param>
    public delegate void DataHoverHandler(object sender, ChartPoint chartPoint);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender">The sender.</param>
    public delegate void UpdaterTickHandler(object sender);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventArgs">The <see cref="RangeChangedEventArgs"/> instance containing the event data.</param>
    public delegate void RangeChangedHandler(RangeChangedEventArgs eventArgs);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventArgs">The <see cref="PreviewRangeChangedEventArgs"/> instance containing the event data.</param>
    public delegate void PreviewRangeChangedHandler(PreviewRangeChangedEventArgs eventArgs);
}
