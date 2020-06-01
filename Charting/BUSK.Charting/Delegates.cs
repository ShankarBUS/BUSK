namespace BUSK.Charting
{
    /// <summary>
    /// Defines a financial coloring rule delegate
    /// </summary>
    /// <param name="current">The current point</param>
    /// <param name="prior">The previous point</param>
    public delegate bool FinancialDelegate(ChartPoint current, ChartPoint prior);
}
