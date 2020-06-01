using BUSK.Core;
using ModernWpf;

namespace BUSK.Utilities
{
    public class ThemeHandler
    {
        public static ThemeHandler Instance { get; set; }

        public ThemeHandler()
        {
            InternalThemeHelper.InternalThemeChanged += InternalThemeHelper_InternalThemeChanged;
        }

        private void InternalThemeHelper_InternalThemeChanged(object sender, InternalThemeChangesEventArgs e)
        {
            switch (e.Theme)
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
