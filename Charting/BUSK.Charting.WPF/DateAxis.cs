using System;
using System.Linq;
using System.Windows;
using BUSK.Charting.Charts;
using BUSK.Charting.Definitions.Charts;
using BUSK.Charting.Helpers;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="BUSK.Charting.WPF.WindowAxis" />
    /// <seealso cref="BUSK.Charting.Definitions.Charts.IDateAxisView" />
    public class DateAxis : WindowAxis, IDateAxisView
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DateAxis"/> class.
        /// </summary>
        public DateAxis()
        {
            // Initialize the axis with date windows
            var collection = new AxisWindowCollection();
            collection.AddRange(DateAxisWindows.GetDateAxisWindows());
            SetCurrentValue(WindowsProperty, collection);
        }

        #endregion

        #region Properties

        /// <summary>
        /// The initial date time property
        /// </summary>
        public static readonly DependencyProperty InitialDateTimeProperty = DependencyProperty.Register(
            "InitialDateTime", typeof(DateTime), typeof(DateAxis), new PropertyMetadata(DateTime.UtcNow, UpdateChart()));
        /// <summary>
        /// Gets or sets the Initial Date Time.
        /// </summary>
        public DateTime InitialDateTime
        {
            get { return (DateTime)GetValue(InitialDateTimeProperty); }
            set { SetValue(InitialDateTimeProperty, value); }
        }

        /// <summary>
        /// The period property
        /// </summary>
        public static readonly DependencyProperty PeriodProperty = DependencyProperty.Register(
            "Period", typeof(PeriodUnits), typeof(DateAxis), new PropertyMetadata(PeriodUnits.Milliseconds, UpdateChart()));
        /// <summary>
        /// Gets or sets the period that represents every unit in the axis.
        /// </summary>
        public PeriodUnits Period
        {
            get { return (PeriodUnits)GetValue(PeriodProperty); }
            set { SetValue(PeriodProperty, value); }
        }

        #endregion

        /// <summary>
        /// Maps as core element.
        /// </summary>
        /// <param name="chart">The chart.</param>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public override AxisCore AsCoreElement(ChartCore chart, AxisOrientation source)
        {
            if (Model == null) Model = new DateAxisCore(this);
            Model.ShowLabels = ShowLabels;
            Model.Chart = chart;
            Model.IsMerged = IsMerged;
            Model.Labels = Labels;
            Model.LabelFormatter = LabelFormatter;
            Model.MaxValue = MaxValue;
            Model.MinValue = MinValue;
            Model.Title = Title;
            Model.Position = Position;
            Model.Separator = Separator.AsCoreElement(Model, source);
            Model.DisableAnimations = DisableAnimations;
            Model.Sections = Sections.Select(x => x.AsCoreElement(Model, source)).ToList();            

            ((DateAxisCore)Model).Windows = Windows.ToList();
            ((DateAxisCore)Model).Windows.ForEach(w => ((DateAxisWindow)w).DateAxisCore = (DateAxisCore)Model);
            return Model;
        }
    }
}