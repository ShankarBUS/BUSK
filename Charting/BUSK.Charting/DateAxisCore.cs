using System;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.Dtos;
using BUSK.Charting.Helpers;

namespace BUSK.Charting
{
    /// <summary>
    /// 
    /// </summary>
    public class DateAxisCore : WindowAxisCore
    {
        private DateTime _initialDateTime = DateTime.MinValue;
        private PeriodUnits _period = PeriodUnits.Milliseconds;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateAxisCore"/> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public DateAxisCore(IWindowAxisView view) : base(view)
        {
            CleanFactor = 3;
        }

        /// <inheritdoc />
        public override Func<double, string> GetFormatter()
        {
            return FormatLabel;
        }

        internal override CoreMargin PrepareChart(AxisOrientation source, ChartCore chart)
        {
            // Get the current configued values from the view
            _initialDateTime = ((IDateAxisView) View).InitialDateTime;
            _period = ((IDateAxisView)View).Period;

            return base.PrepareChart(source, chart);
        }

        private string FormatLabel(double x)
        {
            // For the points, we use the actual value based upon the period
            var dateTime = GetdateTime(x);

            return ((IDateAxisView)View).Period switch
            {
                PeriodUnits.Seconds => dateTime.ToString("G"),
                PeriodUnits.Minutes => dateTime.ToString("g"),
                PeriodUnits.Hours => dateTime.ToString("g"),
                PeriodUnits.Days => dateTime.ToString("d"),
                PeriodUnits.Milliseconds => dateTime.ToString("G") + dateTime.ToString(".fff"),
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        internal DateTime GetdateTime(double x)
        {
            var dateTime = _period switch
            {
                PeriodUnits.Milliseconds => _initialDateTime.AddMilliseconds(Math.Floor(x)),
                PeriodUnits.Seconds => _initialDateTime.AddSeconds(Math.Floor(x)),
                PeriodUnits.Minutes => _initialDateTime.AddMinutes(Math.Floor(x)),
                PeriodUnits.Hours => _initialDateTime.AddHours(Math.Floor(x)),
                PeriodUnits.Days => _initialDateTime.AddDays(Math.Floor(x)),
                _ => throw new ArgumentException(),
            };
            return dateTime;
        }
    }
}