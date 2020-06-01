using System;
using System.Linq;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.Definitions.Series;
using BUSK.Charting.Dtos;
using BUSK.Charting.Helpers;

namespace BUSK.Charting.Charts
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.Charts.ChartCore" />
    public class PieChartCore : ChartCore
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PieChartCore"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="updater">The updater.</param>
        public PieChartCore(IChartView view, ChartUpdater updater) : base(view, updater)
        {
            updater.Chart = this;
        }

        #endregion

        #region Publics

        /// <summary>
        /// Prepares the axes.
        /// </summary>
        /// <exception cref="BUSKChartingException">There is a invalid series in the series collection, " +
        ///                     "verify that all the series implement IPieSeries.</exception>
        public override void PrepareAxes()
        {
            View.Zoom = ZoomingOptions.None;

            if (View.ActualSeries.Any(x => !(x.Model is IPieSeries)))
                throw new BUSKChartingException(
                    "There is a invalid series in the series collection, " +
                    "verify that all the series implement IPieSeries.");

            foreach (var xi in AxisX)
            {
                xi.S = 1;
                xi.BotLimit = View.ActualSeries.Select(x => x.Values.GetTracker(x).XLimit.Min)
                    .DefaultIfEmpty(0).Min();
                xi.TopLimit = View.ActualSeries.Select(x => x.Values.GetTracker(x).XLimit.Max)
                    .DefaultIfEmpty(0).Max();

                if (Math.Abs(xi.BotLimit - xi.TopLimit) < xi.S * .01)
                {
                    xi.BotLimit -= xi.S;
                    xi.TopLimit += xi.S;
                }
            }

            foreach (var yi in AxisY)
            {
                //yi.CalculateSeparator(this, AxisTags.X);
                yi.BotLimit = View.ActualSeries.Select(x => x.Values.GetTracker(x).YLimit.Min)
                    .DefaultIfEmpty(0).Min();
                yi.TopLimit = View.ActualSeries.Select(x => x.Values.GetTracker(x).YLimit.Max)
                    .DefaultIfEmpty(0).Max();

                if (Math.Abs(yi.BotLimit - yi.TopLimit) < yi.S * .01)
                {
                    yi.BotLimit -= yi.S;
                    yi.TopLimit += yi.S;
                }
            }

            StackPoints(View.ActualSeries, AxisOrientation.Y, 0);

            var curSize = new CoreRectangle(0, 0, ControlSize.Width, ControlSize.Height);

            curSize = PlaceLegend(curSize);

            DrawMargin.Top = curSize.Top;
            DrawMargin.Left = curSize.Left;
            DrawMargin.Width = curSize.Width;
            DrawMargin.Height = curSize.Height;
        }

        #endregion

    }
}
