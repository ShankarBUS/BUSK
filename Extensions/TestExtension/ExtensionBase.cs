using BUSK.Navigation;
using BUSK.Core.Extensibility;
using System;
using System.Diagnostics;
using ModernWpf.Controls;
using System.Windows.Controls;
using BUSK.Core.Shortcutting;
using System.Windows.Input;
using BUSK.Core.Shortcutting.Commands;
using System.Windows.Media.Imaging;

namespace TestExtension
{

    public class ExtensionBase : BUSK.Core.Extensibility.ExtensionBase
    {

        public ExtensionBase()
        {
            ExtensionInfo = new ExtensionInfo()
            {
                Title = "My Extension",
                Description = "The description goes here",
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/TestExtension;component/Assets/Images/Logo.png"))
            };

            StartUp += ExtensionBase_StartUp;
        }

        private void ExtensionBase_StartUp(object sender, EventArgs e)
        {
            NavigationPageHelper.RequestNavigationPageAddition(typeof(Button), "Test Extension", new SymbolIcon() { Symbol = Symbol.Admin }, "Test Extension");
            AddShortcut();
        }

        private void AddShortcut()
        {
            Shortcut shortcut = new Shortcut()
            {
                Title = "My Custom Shortcut",
                ShortcutType = ShortcutType.ExtensionDefined,
                Hotkey = new Hotkey(Key.Q, ModifierKeys.Control | ModifierKeys.Alt),
            };
            shortcut.Commands.Add(new CustomMethodCommand()
            {
                CustomMethod = () => { Debug.WriteLine("Shortcut Activated!"); }
            });
            ShortcutsManager.Instance.Shortcuts.Add(shortcut);
        }
    }
}
