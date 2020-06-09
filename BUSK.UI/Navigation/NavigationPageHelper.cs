using ModernWpf.Controls;
using System;

namespace BUSK.Navigation
{
    public static class NavigationPageHelper
    {
        internal const string DefaultPageTitle = "Navigation Page";

        internal const string DefaultPageTooltip = "Navigation Page";

        internal static event NavigationPageAdditionEventHandler NavigationPageAdditionRequested;

        internal static event NavigationPageRemovalEventHandler NavigationPageRemovalRequested;

        public static bool RequestNavigationPageAddition(Type pageType, string title, IconElement icon, string tooltip)
        {
            if (pageType == null) return false;
            if (string.IsNullOrEmpty(title) || string.IsNullOrWhiteSpace(title)) title = DefaultPageTitle;
            if (string.IsNullOrEmpty(tooltip) || string.IsNullOrWhiteSpace(tooltip)) tooltip = DefaultPageTooltip;

            var args = new NavigationPageAdditionEventArgs(pageType, title, icon, tooltip);
            NavigationPageAdditionRequested?.Invoke(args);

            return args.PageAdded;
        }

        public static bool RequestNavigationPageRemoval(Type pageType)
        {
            if (pageType == null) return false;

            var args = new NavigationPageRemovalEventArgs(pageType);
            NavigationPageRemovalRequested?.Invoke(args);

            return args.PageRemoved;
        }
    }
}
