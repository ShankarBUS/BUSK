using System.Windows.Input;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting
{
    public class Hotkey : BindableBase
    {
        public Hotkey()
        {

        }

        public Hotkey(Key inputKey, ModifierKeys mods = 0)
        {
            Key = inputKey;
            Modifiers = mods;
            control = mods.HasFlag(ModifierKeys.Control);
            shift = mods.HasFlag(ModifierKeys.Shift);
            alt = mods.HasFlag(ModifierKeys.Alt);
            windows = mods.HasFlag(ModifierKeys.Windows);
        }

        #region Properties

        private Key key = Key.A;

        [XmlAttribute]
        public Key Key
        { 
            get { return key; }
            set { SetPropertyValue(ref key, value); }
        }

        [XmlIgnore]
        public ModifierKeys Modifiers { get; set; }

        private bool alt = false;

        [XmlAttribute]
        public bool Alt
        {
            get { return alt; }
            set { if (SetPropertyValue(ref alt, value)) { SetMods(); } }
        }

        private bool control = false;

        [XmlAttribute]
        public bool Control
        {
            get { return control; }
            set { if (SetPropertyValue(ref control, value)) { SetMods(); } }
        }

        private bool shift = false;

        [XmlAttribute]
        public bool Shift
        {
            get { return shift; }
            set { if (SetPropertyValue(ref shift, value)) { SetMods(); } }
        }

        private bool windows = false;

        [XmlAttribute]
        public bool Windows
        {
            get { return windows; }
            set { if (SetPropertyValue(ref windows, value)) { SetMods(); } }
        }

        #endregion 

        public Hotkey Clone()
        {
            return (Hotkey)MemberwiseClone();
        }

        public void SetMods()
        {
            Modifiers = (Control ? ModifierKeys.Control : 0) | (Shift ? ModifierKeys.Shift : 0) | (Alt ? ModifierKeys.Alt : 0) | (Windows ? ModifierKeys.Windows : 0);
        }

        public override string ToString()
        {
            return ((Control ? "Control + " : "") + (Shift ? "Shift + " : "") + (Alt ? "Alt + " : "") + (Windows ? "Windows + " : "") + Key.ToString());
        }
    }
}
