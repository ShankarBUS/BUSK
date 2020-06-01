using BUSK.Charting;
using BUSK.Core;
using BUSK.Utilities;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace BUSK.Controls.Plotting
{
    [ContentProperty(nameof(Series))]
    public partial class GraphView : UserControl
    {
        private PerformancePlotter performancePlotter;

        public event GraphViewDockChangedHandler GraphViewDockChanged;

        public GraphView()
        {
            InitializeComponent();
            Series = new SeriesCollection();

            Chart.ScrollGraph(BandWidth);

            UpdateInterval();

            performancePlotter = new PerformancePlotter(() => UpdateGraph());

            SetBindings();

            MouseDoubleClick += (s, e) => UnDockFromView();

            Loaded += GraphView_Loaded;
            Unloaded += GraphView_Unloaded;
        }

        #region Properties

        #region AdditionalMiniViewContent

        public static readonly DependencyProperty AdditionalMiniViewContentProperty = DependencyProperty.Register(
            nameof(AdditionalMiniViewContent),
            typeof(FrameworkElement),
            typeof(GraphView),
            new FrameworkPropertyMetadata(null, OnAdditionalMiniViewContentChanged));

        public FrameworkElement AdditionalMiniViewContent
        {
            get => (FrameworkElement)GetValue(AdditionalMiniViewContentProperty);
            set => SetValue(AdditionalMiniViewContentProperty, value);
        }

        private static void OnAdditionalMiniViewContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gv = (GraphView)d;
            gv.AdditionalContentHost.Child = (FrameworkElement)e.NewValue;
        }

        #endregion

        #region BandWidth

        public static readonly DependencyProperty BandWidthProperty = DependencyProperty.Register(
            nameof(BandWidth),
            typeof(double),
            typeof(GraphView),
            new PropertyMetadata(60.0, OnBandWidthChanged));

        public double BandWidth
        {
            get { return (double)GetValue(BandWidthProperty); }
            set { SetValue(BandWidthProperty, value); }
        }

        private static void OnBandWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gv = (GraphView)d;
            double bandWidth = (double)e.NewValue;
            gv.Chart.ScrollGraph(bandWidth);
            gv.UpdateXAxisTitle();
        }

        #endregion

        #region Interval

        public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register(
            nameof(Interval),
            typeof(double),
            typeof(GraphView),
            new FrameworkPropertyMetadata(Core.Diagnostics.CountersHandler.DefaultInterval, OnIntervalChanged));

        public double Interval
        {
            get => (double)GetValue(IntervalProperty);
            set => SetValue(IntervalProperty, value);
        }

        private static void OnIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gv = (GraphView)d;
            gv.performancePlotter.Interval = (double)e.NewValue;
            gv.UpdateXAxisTitle();
        }

        #endregion

        #region IsGlobalIntervalRespected

        public static readonly DependencyProperty IsGlobalIntervalRespectedProperty = DependencyProperty.Register(
            nameof(IsGlobalIntervalRespected),
            typeof(bool),
            typeof(GraphView),
            new FrameworkPropertyMetadata(true));

        public bool IsGlobalIntervalRespected
        {
            get => (bool)GetValue(IsGlobalIntervalRespectedProperty);
            set => SetValue(IsGlobalIntervalRespectedProperty, value);
        }

        #endregion

        #region MiniViewCommand

        private static readonly DependencyPropertyKey MiniViewCommandPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(MiniViewCommand),
            typeof(ICommand),
            typeof(GraphView),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty MiniViewCommandProperty = MiniViewCommandPropertyKey.DependencyProperty;

        public ICommand MiniViewCommand
        {
            get { return (ICommand)GetValue(MiniViewCommandProperty); }
            private set { SetValue(MiniViewCommandPropertyKey, value); }
        }

        #endregion

        #region MiniViewGlyph

        private static readonly DependencyPropertyKey MiniViewGlyphPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(MiniViewGlyph),
            typeof(string),
            typeof(GraphView),
            new FrameworkPropertyMetadata(BUSKGlyphs.OpenInNewWindow));

        public static readonly DependencyProperty MiniViewGlyphProperty = MiniViewGlyphPropertyKey.DependencyProperty;

        public string MiniViewGlyph
        {
            get { return (string)GetValue(MiniViewGlyphProperty); }
            private set { SetValue(MiniViewGlyphPropertyKey, value); }
        }

        #endregion

        #region MaxYValue

        public static readonly DependencyProperty MaxYValueProperty = PerformanceChart.MaxYValueProperty.AddOwner(typeof(GraphView));

        public double MaxYValue
        {
            get { return (double)GetValue(MaxYValueProperty); }
            set { SetValue(MaxYValueProperty, value); }
        }

        #endregion

        #region Series

        public SeriesCollection Series
        {
            get => Chart.Series;
            set => Chart.Series = value;
        }

        #endregion

        #region YAxisLabelFormatter

        public static readonly DependencyProperty YAxisLabelFormatterProperty = PerformanceChart.YAxisLabelFormatterProperty.AddOwner(typeof(GraphView));

        public Func<double, string> YAxisLabelFormatter
        {
            get { return (Func<double, string>)GetValue(YAxisLabelFormatterProperty); }
            set { SetValue(YAxisLabelFormatterProperty, value); }
        }

        #endregion

        #endregion

        private void SetBindings()
        {
            Chart.SetBinding(PerformanceChart.MaxYValueProperty, new Binding() { Source = this, Path = new PropertyPath(MaxYValueProperty) });
            Chart.SetBinding(PerformanceChart.YAxisLabelFormatterProperty, new Binding() { Source = this, Path = new PropertyPath(YAxisLabelFormatterProperty) });

            MiniViewCommand = new RelayCommand(o =>
            {
                if (IsDocked)
                {
                    UnDockFromView();
                }
                else
                {
                    DockIntoView();
                }
            });
        }

        private void UpdateGraph()
        {
            Dispatcher.Invoke(() =>
            {
                Chart.UpdateValuesAndScrollGraph(BandWidth);
            });
        }

        private MiniView miniView;

        public bool IsDocked 
        { 
            get { return GetIsDocked(); }
        }

        private bool GetIsDocked()
        {
            return GraphContentHost.Child == GraphContent;
        }

        public void DockIntoView(bool closeMiniView = true)
        {
            if (!IsDocked)
            {
                miniView.ContentArea.Child = null;
                Chart.RestoreFromMinViewState();
                AdditionalContentHost.Visibility = Visibility.Collapsed;
                FallbackContent.Visibility = Visibility.Collapsed;
                GraphContentHost.Child = GraphContent;

                if (closeMiniView)
                { miniView?.Close(); }

                miniView = null;

                MiniViewGlyph = BUSKGlyphs.OpenInNewWindow;
                GraphViewDockChanged?.Invoke(true);
            }
        }

        public void UnDockFromView()
        {
            if (IsDocked)
            {
                var pos = PointToScreen(new Point(0, 0));
                miniView = new MiniView()
                {
                    Width = ActualWidth,
                    Height = ActualHeight,
                    Left = pos.X,
                    Top = pos.Y
                };

                GraphContentHost.Child = null;
                AdditionalContentHost.Visibility = Visibility.Visible;
                FallbackContent.Visibility = Visibility.Visible;
                miniView.ContentArea.Child = GraphContent;
                Chart.PrepareForMiniViewState();

                void handler(object sender, CancelEventArgs e)
                {
                    miniView.Closing -= handler;
                    DockIntoView(false);
                }
                miniView.Closing += handler;
                miniView.Show();
                MiniViewGlyph = BUSKGlyphs.ReturnToWindow;
                GraphViewDockChanged?.Invoke(false);
            }
        }

        private void GraphView_Unloaded(object sender, RoutedEventArgs e)
        {
            Core.Diagnostics.CountersHandler.CommonCounterIntervalChanged -= CountersHandler_CommonCounterIntervalChanged;
        }

        private void GraphView_Loaded(object sender, RoutedEventArgs e)
        {
            Core.Diagnostics.CountersHandler.CommonCounterIntervalChanged += CountersHandler_CommonCounterIntervalChanged;
        }

        private void CountersHandler_CommonCounterIntervalChanged(object sender, EventArgs e)
        {
            UpdateInterval();
        }

        private void UpdateInterval()
        {
            if (IsGlobalIntervalRespected)
            {
                Interval = Core.Diagnostics.CountersHandler.CommonInterval;
            }
        }

        private void UpdateXAxisTitle()
        {
            Chart.XAxisTitle = $"{ BandWidth * Interval / 1000.0 } Seconds";
        }
    }

    public delegate void GraphViewDockChangedHandler(bool isDocked);
}
