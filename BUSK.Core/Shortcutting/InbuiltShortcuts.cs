using BUSK.Core.Shortcutting.Commands;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BUSK.Core.Shortcutting
{
    internal class InbuiltShortcuts
    {
        internal static Shortcut ToggleMainWindowVisibility
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Toggle Main Window's Visibility", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.H, ModifierKeys.Control | ModifierKeys.Alt) };
                shortcut.Commands.Add(new MainWindowVisibilityCommand() { VisibilityMode = WindowVisibilityMode.Toggle });
                return shortcut;
            }
        }

        internal static Shortcut PauseOrResumeCounters
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Pause (Or) Resume Counters", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.MediaPlayPause) };
                shortcut.Commands.Add(new CounterStateCommand() { CounterState = CounterState.Toggle });
                return shortcut;
            }
        }

        internal static Shortcut ShowSavedSettings
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Show Saved Settings (Options)", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.O, ModifierKeys.Control | ModifierKeys.Alt) };
                shortcut.Commands.Add(new CustomMethodCommand()
                {
                    CustomMethod = () =>
                    {
                        string o = "";
                        foreach (Setting setting in SettingsCollector.Instance().Settings)
                        {
                            o += $"{setting.Name} : {setting.Value.ToString()} \n";
                        }
                        MessageBox.Show(o);
                    }
                });
                return shortcut;
            }
        }

        internal static Shortcut ShowSettingsWindow
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Show Settings Window", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.S, ModifierKeys.Control | ModifierKeys.Alt) };
                shortcut.Commands.Add(new SettingsWindowVisibilityCommand() { VisibilityMode = WindowVisibilityMode.Show });
                return shortcut;
            }
        }

        internal static Shortcut SetWindowsDefaultTheme
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Set Windows Default Theme", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.W, ModifierKeys.Control | ModifierKeys.Alt) };
                shortcut.Commands.Add(new ThemeControlCommand() { Theme = Theme.WindowsDefault });
                return shortcut;
            }
        }

        internal static Shortcut SetDarkTheme
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Set Dark Theme", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.D, ModifierKeys.Control | ModifierKeys.Alt) };
                shortcut.Commands.Add(new ThemeControlCommand() { Theme = Theme.Dark });
                return shortcut;
            }
        }

        internal static Shortcut SetLightTheme
        {
            get
            {
                Shortcut shortcut = new Shortcut() { Title = "Set Light Theme", ShortcutType = ShortcutType.Inbuilt, Hotkey = new Hotkey(Key.L, ModifierKeys.Control | ModifierKeys.Alt) };
                shortcut.Commands.Add(new ThemeControlCommand() { Theme = Theme.Light });
                return shortcut;
            }
        }

        internal static List<Shortcut> GetShortcuts()
        {
            List<Shortcut> shortcutsList = new List<Shortcut>();

            shortcutsList.AddRange(new Shortcut[] { PauseOrResumeCounters, ToggleMainWindowVisibility, ShowSettingsWindow, SetWindowsDefaultTheme, SetDarkTheme, SetLightTheme });
#if DEBUG
            shortcutsList.Add(ShowSavedSettings);
#endif
            return shortcutsList;
        }
    }
}
