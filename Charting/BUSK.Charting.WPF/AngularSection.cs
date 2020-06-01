using System.Windows;
using System.Windows.Media;

namespace BUSK.Charting.WPF
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.FrameworkElement" />
    public class AngularSection : FrameworkElement
    {
        internal AngularGauge Owner { get; set; }

        /// <summary>
        /// From value property
        /// </summary>
        public static readonly DependencyProperty FromValueProperty = DependencyProperty.Register(
            "FromValue", typeof(double), typeof(AngularSection), new PropertyMetadata(default(double), Redraw));

        /// <summary>
        /// Gets or sets from value.
        /// </summary>
        /// <value>
        /// From value.
        /// </value>
        public double FromValue
        {
            get { return (double) GetValue(FromValueProperty); }
            set { SetValue(FromValueProperty, value); }
        }

        /// <summary>
        /// To value property
        /// </summary>
        public static readonly DependencyProperty ToValueProperty = DependencyProperty.Register(
            "ToValue", typeof(double), typeof(AngularSection), new PropertyMetadata(default(double), Redraw));

        /// <summary>
        /// Gets or sets to value.
        /// </summary>
        /// <value>
        /// To value.
        /// </value>
        public double ToValue
        {
            get { return (double) GetValue(ToValueProperty); }
            set { SetValue(ToValueProperty, value); }
        }

        /// <summary>
        /// The fill property
        /// </summary>
        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(
            "Fill", typeof(Brush), typeof(AngularSection), new PropertyMetadata(default(Brush)));

        /// <summary>
        /// Gets or sets the fill.
        /// </summary>
        /// <value>
        /// The fill.
        /// </value>
        public Brush Fill
        {
            get { return (Brush) GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        private static void Redraw(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var angularSection = (AngularSection) dependencyObject;

            if (angularSection.Owner == null) return;

            angularSection.Owner.UpdateSections();
        }

    }
}