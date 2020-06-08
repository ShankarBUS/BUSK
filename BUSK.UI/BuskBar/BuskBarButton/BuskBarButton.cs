using ModernWpf.Controls.Primitives;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.UI.BuskBar
{
    public class BuskBarButton : Button, IBuskBarItem
    {
        static BuskBarButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BuskBarButton), new FrameworkPropertyMetadata(typeof(BuskBarButton)));
        }

        public BuskBarButton()
        {

        }

        #region Properties

        #region CornerRadius

        public static readonly DependencyProperty CornerRadiusProperty =
            ControlHelper.CornerRadiusProperty.AddOwner(typeof(BuskBarButton));

        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        #endregion

        #region UseSystemFocusVisuals

        public static readonly DependencyProperty UseSystemFocusVisualsProperty =
            FocusVisualHelper.UseSystemFocusVisualsProperty.AddOwner(typeof(BuskBarButton));

        public bool UseSystemFocusVisuals
        {
            get => (bool)GetValue(UseSystemFocusVisualsProperty);
            set => SetValue(UseSystemFocusVisualsProperty, value);
        }

        #endregion

        #region FocusVisualMargin

        public static readonly DependencyProperty FocusVisualMarginProperty =
            FocusVisualHelper.FocusVisualMarginProperty.AddOwner(typeof(BuskBarButton));

        public Thickness FocusVisualMargin
        {
            get => (Thickness)GetValue(FocusVisualMarginProperty);
            set => SetValue(FocusVisualMarginProperty, value);
        }

        #endregion

        #endregion
    }
}
