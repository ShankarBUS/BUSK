using System;

namespace BUSK.Core
{
    [Serializable]
    public enum Theme
    {
        WindowsDefault = 0,
        Light = 1,
        Dark = 2
    }

    internal class InternalThemeHelper
    {
        public static void SetLightTheme()
        {
            SettingsManager.Instance.Theme = Theme.Light;
        }

        public static void SetDarkTheme()
        {
            SettingsManager.Instance.Theme = Theme.Dark;
        }

        public static void SetWindowsDefaultTheme()
        {
            SettingsManager.Instance.Theme = Theme.WindowsDefault;
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
