using System;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("MainWindow Visibility Command", "A Command to control the visibility of BUSK's MainWindow")]
    public class MainWindowVisibilityCommand : Command
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
                    BuskInterop.ShowMainWindow();
                    return;
                case WindowVisibilityMode.Hide:
                    BuskInterop.HideMainWindow();
                    return;
                case WindowVisibilityMode.Toggle:
                    BuskInterop.ToggleMainWindowVisibility();
                    return;
            }
        }
    }
}
