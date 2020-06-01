using System;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("Theme Control Command", "A Command to control the theme of BUSK")]
    public class ThemeControlCommand : Command
    {

        #region Properties

        private Theme theme = Theme.WindowsDefault;

        [XmlAttribute]
        public Theme Theme
        {
            get { return theme; }
            set { SetPropertyValue(ref theme, value); }
        }

        #endregion 

        public override void OnExecute()
        {
            switch (theme)
            {
                case Theme.WindowsDefault:
                    InternalThemeHelper.SetWindowsDefaultTheme();
                    return;
                case Theme.Light:
                    InternalThemeHelper.SetLightTheme();
                    return;
                case Theme.Dark:
                    InternalThemeHelper.SetDarkTheme();
                    return;
            }
        }
    }
}
