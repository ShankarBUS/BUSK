using System;

namespace BUSK.Navigation
{
    internal class NavigationPageRemovalEventArgs : EventArgs
    {
        public NavigationPageRemovalEventArgs(Type pageType)
        {
            PageType = pageType;
        }

        public Type PageType { get; set; }

        public bool PageRemoved { get; set; } = false;
    }
}
