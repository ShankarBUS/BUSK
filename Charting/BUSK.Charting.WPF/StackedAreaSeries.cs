using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BUSK.Charting.Definitions.Series;
using BUSK.Charting.SeriesAlgorithms;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// The stacked area compares trends and percentage, add this series to a cartesian chart
    /// </summary>
    /// <seealso cref="BUSK.Charting.WPF.LineSeries" />
    /// <seealso cref="BUSK.Charting.Definitions.Series.IStackedAreaSeriesView" />
    public class StackedAreaSeries : LineSeries, IStackedAreaSeriesView
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of StackedAreaSeries class
        /// </summary>
        public StackedAreaSeries()
        {
            Model = new StackedAreaAlgorithm(this);
            InitializeDefuaults();
        }

        /// <summary>
        /// Initializes a new instance of StackedAreaSeries class, with a given mapper
        /// </summary>
        public StackedAreaSeries(object configuration)
        {
            Model = new StackedAreaAlgorithm(this);
            Configuration = configuration;
            InitializeDefuaults();
        }

        #endregion

        #region Private Properties
        #endregion

        #region Properties

        /// <summary>
        /// The stack mode property
        /// </summary>
        public static readonly DependencyProperty StackModeProperty = DependencyProperty.Register(
            "StackMode", typeof(StackMode), typeof(StackedAreaSeries),
            new PropertyMetadata(default(StackMode), CallChartUpdater()));
        /// <summary>
        /// Gets or sets the series stacked mode, values or percentage
        /// </summary>
        public StackMode StackMode
        {
            get { return (StackMode)GetValue(StackModeProperty); }
            set { SetValue(StackModeProperty, value); }
        }
        #endregion

        #region Overridden Methods

        /// <summary>
        /// This method runs when the update starts
        /// </summary>
        public override void OnSeriesUpdateStart()
        {
            ActiveSplitters = 0;

            if (SplittersCollector == int.MaxValue - 1)
            {
                //just in case!
                Splitters.ForEach(s => s.SplitterCollectorIndex = 0);
                SplittersCollector = 0;
            }

            SplittersCollector++;

            if (Figure != null && Values != null)
            {
                var xIni = ChartFunctions.ToDrawMargin(Values.GetTracker(this).XLimit.Min, AxisOrientation.X, Model.Chart, ScalesXAt);

                if (Model.Chart.View.DisableAnimations)
                    Figure.StartPoint = new Point(xIni, Model.Chart.DrawMargin.Height);
                else
                    Figure.BeginAnimation(PathFigure.StartPointProperty,
                        new PointAnimation(new Point(xIni, Model.Chart.DrawMargin.Height),
                            Model.Chart.View.AnimationsSpeed));
            }

            if (IsPathInitialized)
            {
                Model.Chart.View.EnsureElementBelongsToCurrentDrawMargin(Path);
                Path.Stroke = Stroke;
                Path.StrokeThickness = StrokeThickness;
                Path.Fill = Fill;
                Path.Visibility = Visibility;
                Path.StrokeDashArray = StrokeDashArray;
                return;
            }

            IsPathInitialized = true;

            Path = new Path
            {
                Stroke = Stroke,
                StrokeThickness = StrokeThickness,
                Fill = Fill,
                Visibility = Visibility,
                StrokeDashArray = StrokeDashArray
            };

            var geometry = new PathGeometry();
            Figure = new PathFigure();
            geometry.Figures.Add(Figure);
            Path.Data = geometry;
            Model.Chart.View.AddToDrawMargin(Path);

            var x = ChartFunctions.ToDrawMargin(ActualValues.GetTracker(this).XLimit.Min, AxisOrientation.X, Model.Chart, ScalesXAt);
            Figure.StartPoint = new Point(x, Model.Chart.DrawMargin.Height);

            var i = Model.Chart.View.Series.IndexOf(this);
            Panel.SetZIndex(Path, Model.Chart.View.Series.Count - i);
        }

        #endregion

        #region Public Methods 


        #endregion

        #region Private Methods

        private void InitializeDefuaults()
        {
            SetCurrentValue(LineSmoothnessProperty, .7d);
            SetCurrentValue(PointGeometrySizeProperty, 0d);
            SetCurrentValue(PointForegroundProperty, Brushes.White);
            SetCurrentValue(ForegroundProperty, new SolidColorBrush(Color.FromRgb(229, 229, 229)));
            SetCurrentValue(StrokeThicknessProperty, 0d);
            DefaultFillOpacity = 1;

            Func<ChartPoint, string> defaultLabel = x => Model.CurrentYAxis.GetFormatter()(x.Y);
            SetCurrentValue(LabelPointProperty, defaultLabel);

            Splitters = new List<LineSegmentSplitter>();
        }

        #endregion
    }
}
