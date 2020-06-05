using BUSK.Core.Shortcutting;
using BUSK.Core.Shortcutting.Commands;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.Controls.Shortcutting
{
    public partial class ShortcutLinkerControl : UserControl
    {
        #region Properties

        #region LinkCommand

        public static readonly DependencyProperty LinkCommandProperty = DependencyProperty.Register(
            nameof(LinkCommand),
            typeof(ShortcutLinkCommand),
            typeof(ShortcutLinkerControl),
            new FrameworkPropertyMetadata(null));

        public ShortcutLinkCommand LinkCommand
        {
            get => (ShortcutLinkCommand)GetValue(LinkCommandProperty);
            set => SetValue(LinkCommandProperty, value);
        }

        #endregion

        #endregion

        public ShortcutLinkerControl()
        {
            InitializeComponent();
        }

        private void SelectShortcut(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.DataContext is Shortcut shortcut)
                {
                    if (shortcut.ShortcutType == ShortcutType.UserDefined && LinkCommand != null)
                    {
                        LinkCommand.Shortcut = shortcut;
                        ListFlyout.Hide();
                    }
                }
            }
        }

        private void CollectionViewSource_Filter(object sender, System.Windows.Data.FilterEventArgs e)
        {
            if (e.Item != null && e.Item is Shortcut shortcut)
            {
                e.Accepted = shortcut.ShortcutType == ShortcutType.UserDefined;
            }
        }
    }
}
