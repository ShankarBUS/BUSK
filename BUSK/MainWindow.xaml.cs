using BUSK.Core;
using BUSK.Core.Diagnostics;
using BUSK.Core.Extensibility;
using BUSK.Core.Shortcutting;
using BUSK.Navigation;
using BUSK.Navigation.Pages;
using BUSK.Utilities;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace BUSK
{
    public partial class MainWindow : Window, IMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            DataContext = this;
            Initialization();
        }

        //public System.Windows.Forms.NotifyIcon NotifyIcon;

        public ContextMenu TrayContextMenu => new ContextMenu();

        private void Initialization()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) { return; }

            App.AddMessage("Initializing");

            if (!Directory.Exists(@".\Shortcuts")) { Directory.CreateDirectory(@".\Shortcuts"); }
            if (!Directory.Exists(@".\Extensions")) { Directory.CreateDirectory(@".\Extensions"); }

            BuskInterop.AddMessageRequested += (s, e) => App.AddMessage(e.Message);
            BuskInterop.Initialize(this);
            RAMInformation.Instance.SetTotalRAM(HardwareInfo.GetPhysicalMemoryBytes());
            ThemeHandler.Instance = new ThemeHandler();

            PageCollector.Instance = new PageCollector();
            PageCollector.Instance.Initialize();

            ExtensionsFileHandler.LoadExtensionBases();
            ShortcutsManager.Instance.LoadAllShortcutsAync();
            ExtensionsManager.Instance.Initialize();
            ExtensionsManager.Instance.EnableChangedRequested += (s, e) => ExtensionsFileHandler.SaveExtInfo(s, e.ConfigLocation);

            //try
            //{
            //    var exitMenu = new MenuItem() { Header = "Exit" };

            //    exitMenu.Click += (s, e) => App.Current.Shutdown();

            //    TrayContextMenu.Items.Add(exitMenu);

            //    NotifyIcon = new System.Windows.Forms.NotifyIcon
            //    {
            //        Icon = new System.Drawing.Icon(Properties.Resources.BUSK, new System.Drawing.Size(16, 16)),
            //        Visible = true,
            //        Text = "BUSK"
            //    };

            //    NotifyIcon.MouseClick += (s, e) =>
            //    { 
            //        if (e.Button == System.Windows.Forms.MouseButtons.Left) { Activate(); }
            //        else if (e.Button == System.Windows.Forms.MouseButtons.Right) { TrayContextMenu.IsOpen = true; }
            //    };

            //    NotifyIcon.MouseDoubleClick += (s, e) =>
            //    {
            //        if (e.Button == System.Windows.Forms.MouseButtons.Left) { Visible = true; }
            //    };

            //} catch (Exception exception) { Debug.WriteLine(exception.Message); }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStyle = WindowStyle.SingleBorderWindow;
            Top = 0;
            App.LoadComplete();
            CountersHandler.RunAll();
        }

        #region IMainWindow

        public readonly DependencyProperty VisibleProperty = DependencyProperty.Register("Visible", typeof(bool), typeof(Window), new PropertyMetadata(true));

        public bool Visible
        {
            get => (bool)GetValue(VisibleProperty);
            set => SetValue(VisibleProperty, value);
        }

        public readonly DependencyProperty PausedProperty = DependencyProperty.Register("Paused", typeof(bool), typeof(Window), new PropertyMetadata(false, OnPausedPropertyChanged));

        private static void OnPausedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var win = (IMainWindow)d;
            var value = (bool)e.NewValue;

            if (value) { win.PauseCounters(); }
            else { win.ResumeCounters(); }
        }

        public bool Paused
        {
            get => (bool)GetValue(PausedProperty);
            set => SetValue(PausedProperty, value);
        }

        public bool IsSettingsWindowVisible
        {
            get
            {
                if (SettingsWindow.Instance == null)
                {
                    return false;
                }

                return SettingsWindow.Instance?.Visibility == Visibility.Visible;
            }
        }

        public void PauseCounters()
        {
            CountersHandler.SuspendAll();
            Paused = true;
        }

        public void ResumeCounters()
        {
            CountersHandler.RunAll();
            Paused = false;
        }

        public void ToggleCounterState()
        {
            if (Paused) { ResumeCounters(); }
            else { PauseCounters(); }
        }

        public void ToggleVisible()
        {
            Visible = !Visible;
        }

        public void ToggleSettingsWindowVisibility()
        {
            if (IsSettingsWindowVisible) HideSettingsWindow();
            else ShowSettingsWindow();
        }

        public void ShowSettingsWindow()
        {
            SettingsWindow.EnsureInstanceAndShow();
        }

        public void HideSettingsWindow()
        {
            SettingsWindow.Instance?.Hide();
        }

        #endregion

        #region Basic Initialization



        #endregion

        #region Test

        private void ShowShortcuts(object sender, RoutedEventArgs e)
        {
            var o = "";
            foreach (Shortcut shortcut in ShortcutsManager.Instance.Shortcuts)
            {
                o += $"{shortcut.Title}, {shortcut.Description}, {shortcut.Hotkey} \n";
            }
            MessageBox.Show(o);
        }

        private void ShowExtensionBases(object sender, RoutedEventArgs e)
        {
            var o = "";
            foreach (ExtensionBase @base in ExtensionsManager.Instance.ExtensionBases)
            {
                o += $"{@base.ExtensionAssemblyLocation}, {@base.ExtensionAssemblyName}, {@base.ExtensionConfigLocation}, {@base.ExtensionDirectoryPath} \n";
            }
            MessageBox.Show(o);
        }

        private void Test(object sender, RoutedEventArgs e)
        {
            NavigationPageHelper.RequestNavigationPageRemoval(typeof(NetStatusPage));
        }

        #endregion
    }
}
