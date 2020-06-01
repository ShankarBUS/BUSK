using System;
using System.ComponentModel;

namespace BUSK.Core
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public enum Theme
    {
        WindowsDefault = 0,
        Light = 1,
        Dark = 2
    }

    internal class InternalThemeHelper
    {
        public static event EventHandler<InternalThemeChangesEventArgs> InternalThemeChanged;

        public static void SetLightTheme()
        {
            InternalThemeChanged?.Invoke(null, new InternalThemeChangesEventArgs(Theme.Light));
        }

        public static void SetDarkTheme()
        {
            InternalThemeChanged?.Invoke(null, new InternalThemeChangesEventArgs(Theme.Dark));
        }

        public static void SetWindowsDefaultTheme()
        {
            InternalThemeChanged?.Invoke(null, new InternalThemeChangesEventArgs(Theme.WindowsDefault));
        }
    }

    internal class InternalThemeChangesEventArgs : EventArgs
    {
        public InternalThemeChangesEventArgs(Theme theme)
        {
            Theme = theme;
        }

        public Theme Theme { get; private set; }
    }
}
