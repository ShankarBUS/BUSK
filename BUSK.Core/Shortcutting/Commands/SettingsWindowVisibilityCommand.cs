using System;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("SettingsWindow Visibility Command", "A Command to control the visibility of BUSK's SettingsWindow")]
    public class SettingsWindowVisibilityCommand : Command
    {
        
        #region Properties

        private WindowVisibilityMode visibilityMode = WindowVisibilityMode.Toggle;

        [XmlAttribute]
        public WindowVisibilityMode VisibilityMode
        {
            get { return visibilityMode; }
            set { SetPropertyValue(ref visibilityMode, value); }
        }

        #endregion 

        public override void OnExecute()
        {
            switch (visibilityMode)
            {
                case WindowVisibilityMode.Show:
                    BuskInterop.ShowSettings();
                    return;
                case WindowVisibilityMode.Hide:
                    BuskInterop.HideSettings();
                    return;
                case WindowVisibilityMode.Toggle:
                    BuskInterop.ToggleSettingsWindowVisibility();
                    return;
            }
        }
    }
}
