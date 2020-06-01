using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using BUSK.Core;

namespace BUSKExtension
{

    public class ExtensionBase : BUSK.Core.ExtensionBase
    {

        public ExtensionBase()
        {
            // - Image should be 64x64, don't set ImageSource property directly (Until you use BitmapImage). Set the Image property instead (you can convert BitmapImage to System.Drawing.Image and vise versa using BUSK.Core.ImageConverter class).
            // - Don't set MinimumWindowsVersion/MaximumWindowsVersion if this extension doesn't have any OS specific functions.
            // - Don't access the Enabled and ImageSource property, it's handled by BUSK.
            this.Extension = new Extension() { Title = "My Extension", Description = "The description goes here", Image = Properties.Resources.Logo /* Replace this Image with yours in Assets\Images folder or add a new one to Resources.resx in Properties folder and change this */ };
            this.StartUp += ExtensionBase_StartUp;
        }

        SettingsNavPage navPage;

        private void ExtensionBase_StartUp(object sender, EventArgs e)
        {
            navPage = new MySettingsPage();
            BUSKInterop.AddSettingsNavPage(navPage); // You can also access the MainWindow's certain properties through the BUSKInterop class.
            AddShortcut();
        }

        private void AddShortcut()
        {
            BUSK.Core.Shortcut shortcut = new BUSK.Core.Shortcut()
            {
                Title = "My Custom Shortcut",
                ShortcutType = ShortcutType.ExtensionDefined, // Must mention the type so that the user can differentiate between shortcut types
                Hotkey = new Hotkey(Keys.Q, Keys.Control | Keys.Alt), // you can also mention modifiers through hotkey's properties, like Control = true, Alt = true, etc. (or) by setting Modifiers = Keys.Control | Keys.Alt
            };
            shortcut.SCommands.Add(new SCommand()
            {
                CommandType = CommandType.Custom,
                CustomMethod = () => { Debug.WriteLine("Shortcut Activated!"); }
            });
            ShortcutsManager.Instance.Shortcuts.Add(shortcut);
        }
    }
}
