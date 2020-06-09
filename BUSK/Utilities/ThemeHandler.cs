using BUSK.Core;
using ModernWpf;

namespace BUSK.Utilities
{
    public class ThemeHandler
    {
        public static ThemeHandler Instance { get; set; }

        public ThemeHandler()
        {
            SettingsManager.Instance.InternalThemeChanged += InternalThemeChanged;

            ChangeTheme(SettingsManager.Instance.Theme);
        }

        private void InternalThemeChanged(object sender, InternalThemeChangesEventArgs e)
        {
            ChangeTheme(e.Theme);
        }

        private static void ChangeTheme(Theme theme)
        {
            switch (theme)
            {
                case Theme.WindowsDefault:
                    ThemeManager.Current.ApplicationTheme = null;
                    break;
                case Theme.Dark:
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
                    break;
                case Theme.Light:
                    ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
                    break;
            }
        }
    }
}
