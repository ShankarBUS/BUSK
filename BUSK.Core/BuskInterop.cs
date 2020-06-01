using BUSK.Core.Utilities;
using System;
using System.Windows;
using System.Windows.Interop;
using BUSK.Core.Extensibility;
using BUSK.Core.Shortcutting;
using BUSK.Core.Diagnostics;

namespace BUSK.Core
{
    internal class AddMessageEventArgs : EventArgs
    {
        public AddMessageEventArgs(string msg)
        {
            Message = msg;
        }

        public string Message { get; private set; }
    }

    public class BuskInterop
    {
        internal static event EventHandler<AddMessageEventArgs> AddMessageRequested;

        public static event EventHandler HookRequested;

        public static Window MainWindow { get; private set; }

        public static IMainWindow IMainWindow { get; private set; }

        internal static void AddSplashScreenMessage(string msg)
        {
            AddMessageRequested?.Invoke(null,new AddMessageEventArgs(msg));
        }

        public static IntPtr GetMainWindowHandle()
        {
            var w = new WindowInteropHelper(MainWindow);
            return w.Handle;
        }

        public static System.Windows.Threading.Dispatcher GetUIDispatcher()
        {
            return MainWindow.Dispatcher;
        }

        public static void Initialize(Window mainWindow)
        {
            MainWindow = mainWindow;

            IMainWindow = (IMainWindow)mainWindow;

            SettingsManager.Initialize();
            DiskHandler.Initialize();
            DisplayHandler.Initialize();
            VersionInfos.Initialize();
            ExtensionsManager.Instance = new ExtensionsManager();
            ShortcutsManager.Instance = new ShortcutsManager();

            HookRequested?.Invoke(null, null);
        }

        internal static void IntializePerfCounters()
        {
            CPUPerfManager.Instance = new CPUPerfManager();
            DiskPerfManager.Instance = new DiskPerfManager();
            NetPerfManager.Instance = new NetPerfManager();
            RAMPerfManager.Instance = new RAMPerfManager();
        }

        public static void ToggleCounterState()
        {
            IMainWindow.ToggleCounterState();
        }

        public static void PauseCounters()
        {
            IMainWindow.PauseCounters();
        }

        public static void ResumeCounters()
        {
            IMainWindow.ResumeCounters();
        }

        public static void ToggleMainWindowVisibility()
        {
            IMainWindow.ToggleVisible();
        }

        public static void ShowMainWindow()
        {
            IMainWindow.Visible = true;
        }

        public static void HideMainWindow()
        {
            IMainWindow.Visible = false;
        }

        public static void ToggleSettingsWindowVisibility()
        {
            IMainWindow.ToggleSettingsWindowVisibility();
        }

        public static void ShowSettings()
        {
            IMainWindow.ShowSettingsWindow();
        }

        public static void HideSettings()
        {
            IMainWindow.HideSettingsWindow();
        }
    }
}