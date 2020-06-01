using System;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.Dtos;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.AxisCore" />
    public class LogarithmicAxisCore : AxisCore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogarithmicAxisCore"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public LogarithmicAxisCore(IAxisView view) : base(view)
        {
            CleanFactor = 1.5;
        }

        internal override CoreMargin PrepareChart(AxisOrientation source, ChartCore chart)
        {
            if (!(Math.Abs(TopLimit - BotLimit) > S * .01) || !ShowLabels) return new CoreMargin();

            CalculateSeparator(chart, source);

            var f = GetFormatter();

            var currentMargin = new CoreMargin();
            if (S < 1) S = 1;
            var tolerance = S / 10;

            InitializeGarbageCollector();

            var bl = Math.Ceiling(BotLimit / Magnitude) * Magnitude;
            var @base = ((ILogarithmicAxisView) View).Base;

            for (var i = bl; i <= TopLimit - (EvaluatesUnitWidth ? 1 : 0); i += S)
            {
                var minTolerance = tolerance/10;
                if (Math.Abs(i - bl) > tolerance)
                {
                    var step = Math.Pow(@base, i - 1);
                    for (var j = Math.Pow(@base, i - 1) + step; 
                        j < Math.Pow(@base, i); 
                        j += step)
                    {
                        var scaledJ = Math.Log(j, @base);

                        var minorKey = Math.Round(scaledJ / minTolerance) * minTolerance;
                        if (!Cache.TryGetValue(minorKey, out SeparatorElementCore minorAsc))
                        {
                            minorAsc = new SeparatorElementCore { IsNew = true };
                            Cache[minorKey] = minorAsc;
                        }
                        else
                        {
                            minorAsc.IsNew = false;
                        }

                        View.RenderSeparator(minorAsc, Chart);

                        minorAsc.Key = minorKey;
                        minorAsc.Value = scaledJ;
                        minorAsc.GarbageCollectorIndex = GarbageCollectorIndex;

                        minorAsc.View.UpdateLabel(string.Empty, this, source);

                        if (LastAxisMax == null)
                        {
                            minorAsc.State = SeparationState.InitialAdd;
                            continue;
                        }

                        minorAsc.State = SeparationState.Keep;
                    }
                }


                var key = Math.Round(i / tolerance) * tolerance;
                if (!Cache.TryGetValue(key, out SeparatorElementCore asc))
                {
                    asc = new SeparatorElementCore { IsNew = true };
                    Cache[key] = asc;
                }
                else
                {
                    asc.IsNew = false;
                }

                View.RenderSeparator(asc, Chart);

                asc.Key = key;
                asc.Value = i;
                asc.GarbageCollectorIndex = GarbageCollectorIndex;

                var labelsMargin = asc.View.UpdateLabel(f(i), this, source);

                currentMargin.Width = labelsMargin.TakenWidth > currentMargin.Width
                    ? labelsMargin.TakenWidth
                    : currentMargin.Width;
                currentMargin.Height = labelsMargin.TakenHeight > currentMargin.Height
                    ? labelsMargin.TakenHeight
                    : currentMargin.Height;

                currentMargin.Left = labelsMargin.Left > currentMargin.Left
                    ? labelsMargin.Left
                    : currentMargin.Left;
                currentMargin.Right = labelsMargin.Right > currentMargin.Right
                    ? labelsMargin.Right
                    : currentMargin.Right;

                currentMargin.Top = labelsMargin.Top > currentMargin.Top
                    ? labelsMargin.Top
                    : currentMargin.Top;
                currentMargin.Bottom = labelsMargin.Bottom > currentMargin.Bottom
                    ? labelsMargin.Bottom
                    : currentMargin.Bottom;

                if (LastAxisMax == null)
                {
                    asc.State = SeparationState.InitialAdd;
                    continue;
                }

                asc.State = SeparationState.Keep;
            }
            return currentMargin;
        }
    }
}