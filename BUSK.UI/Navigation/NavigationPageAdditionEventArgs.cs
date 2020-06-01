using ModernWpf.Controls;
using System;

namespace BUSK.Navigation
{
    internal class NavigationPageAdditionEventArgs : EventArgs
    {
        public NavigationPageAdditionEventArgs(Type pageType, string title, IconElement icon, string tooltip)
        {
            PageType = pageType;
            Title = title;
            Icon = icon;
            Tooltip = tooltip;
        }

        public Type PageType { get; set; }

        public string Title { get; set; }

        public IconElement Icon { get; set; }

        public string Tooltip { get; set; }

        public bool PageAdded { get; set; } = false;
    }
}
