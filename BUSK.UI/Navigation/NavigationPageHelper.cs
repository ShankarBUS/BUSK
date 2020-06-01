using ModernWpf.Controls;
using System;

namespace BUSK.Navigation
{
    public class NavigationPageHelper
    {
        internal const string DefaultPageTitle = "Navigation Page";

        internal const string DefaultPageTooltip = "Navigation Page";

        internal static event NavigationPageAdditionEventHandler NavigationPageAdditionRequested;

        public static bool RequestNavigationPageAddition(Type pageType, string title, IconElement icon, string tooltip)
        {
            if (pageType == null) return false;
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title)) title = DefaultPageTitle;
            if (string.IsNullOrEmpty(tooltip) || string.IsNullOrWhiteSpace(tooltip)) tooltip = DefaultPageTooltip;

            var args = new NavigationPageAdditionEventArgs(pageType, title, icon, tooltip);
            NavigationPageAdditionRequested?.Invoke(args);

            return args.PageAdded;
        }
    }
}
