using System;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("Shortcut Link Command", "A Command to link another Shortcut with this Shortcut")]
    public class ShortcutLinkCommand : Command
    {

        #region Properties

        private string id = "";

        [XmlAttribute]
        public string ID
        {
            get { return id; }
            set { if (IsIDValid(value) && SetPropertyValue(ref id, value)) { LinkToShortcut(value); } }
        }

        private Shortcut shortcut;

        [XmlIgnore]
        public Shortcut Shortcut
        {
            get { return shortcut; }
            set
            {
                if (Owner == value && value != null)
                {
                    return;
                }

                if (SetPropertyValue(ref shortcut, value)) 
                { 
                    if (value != null)
                    {
                        ID = value.ID;
                    }
                }
            }
        }

        #endregion 

        public ShortcutLinkCommand() : this(string.Empty)
        {

        }

        public ShortcutLinkCommand(string shortcutID)
        {
            if (IsIDValid(shortcutID))
            {
                id = shortcutID;
                LinkToShortcut(shortcutID);
            }
        }

        public ShortcutLinkCommand(Shortcut shortcutToLink)
        {
            shortcut = shortcutToLink;
        }

        private async void LinkToShortcut(string shortcutID)
        {
            if (Shortcut == null)
            {
                Shortcut = await ShortcutLinker.GetShortcutAsync(shortcutID);
            }
        }

        public override void OnExecute()
        {
            Shortcut?.Start();
        }

        private bool IsIDValid(string id) => !string.IsNullOrEmpty(id) && !string.IsNullOrWhiteSpace(id);
    }
}
