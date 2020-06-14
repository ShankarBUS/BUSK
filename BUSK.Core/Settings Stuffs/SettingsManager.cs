using BUSK.Core.Diagnostics;
using System;
using System.Runtime.CompilerServices;

namespace BUSK.Core
{
    public sealed class SettingsManager : BindableBase
    {
        private static SettingsCollector SettingsCollector { get; set; }

        internal event EventHandler<InternalThemeChangesEventArgs> InternalThemeChanged;

        internal event EventHandler<ModuleEnabledChangedEventArgs> ModuleEnabledChanged;

        public static SettingsManager Instance { get; internal set; }

        #region Properties

        private bool cme = true;

        public bool CPUModuleEnabled
        {
            get { return cme; }
            set
            {
                if (SetValueAndSave(ref cme, value))
                {
                    ModuleEnabledChanged?.Invoke(this, new ModuleEnabledChangedEventArgs(CounterType.CPU, value));
                }
            }
        }

        private bool dme = true;

        public bool DiskModuleEnabled
        {
            get { return dme; }
            set
            {
                if (SetValueAndSave(ref dme, value))
                {
                    ModuleEnabledChanged?.Invoke(this, new ModuleEnabledChangedEventArgs(CounterType.Disk, value));
                }
            }
        }

        private bool nme = true;

        public bool NetModuleEnabled
        {
            get { return nme; }
            set
            {
                if (SetValueAndSave(ref nme, value))
                {
                    ModuleEnabledChanged?.Invoke(this, new ModuleEnabledChangedEventArgs(CounterType.Network, value));
                }
            }
        }

        private bool rme = true;

        public bool RAMModuleEnabled
        {
            get { return rme; }
            set
            {
                if (SetValueAndSave(ref rme, value))
                {
                    ModuleEnabledChanged?.Invoke(this, new ModuleEnabledChangedEventArgs(CounterType.RAM, value));
                }
            }
        }

        private Theme theme = Theme.WindowsDefault;

        public Theme Theme
        {
            get { return theme; }
            set
            {
                if (SetValueAndSave(ref theme, value))
                {
                    InternalThemeChanged?.Invoke(null, new InternalThemeChangesEventArgs(value));
                }
            }
        }

        #endregion

        internal SettingsManager()
        {
            BuskInterop.AddSplashScreenMessage("Initializing Settings");

            SettingsCollector.Initialize();
            SettingsCollector = SettingsCollector.Instance;

            SettingsCollector sc = SettingsCollector;
            var cmes = sc.GetSetting<bool>(nameof(CPUModuleEnabled));
            var dmes = sc.GetSetting<bool>(nameof(DiskModuleEnabled));
            var nmes = sc.GetSetting<bool>(nameof(NetModuleEnabled));
            var rmes = sc.GetSetting<bool>(nameof(RAMModuleEnabled));
            var ts = sc.GetSetting<Theme>(nameof(Theme));

            if (cmes != null) { CPUModuleEnabled = cmes.Value; }
            else { sc.Add(nameof(CPUModuleEnabled), cme); }

            if (dmes != null) { DiskModuleEnabled = dmes.Value; }
            else { sc.Add(nameof(DiskModuleEnabled), dme); }

            if (nmes != null) { NetModuleEnabled = nmes.Value; }
            else { sc.Add(nameof(NetModuleEnabled), nme); }

            if (rmes != null) { RAMModuleEnabled = rmes.Value; }
            else { sc.Add(nameof(RAMModuleEnabled), rme); }

            if (ts != null) { Theme = ts.Value; }
            else { sc.Add(nameof(Theme), theme); }

            sc.Save();
        }

        private bool SetValueAndSave<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (SetPropertyValue(ref field, value, propertyName))
            {
                SettingsCollector.Add(propertyName, value);
                SettingsCollector.Save();
                return true;
            }
            return false;
        }
    }

    internal class ModuleEnabledChangedEventArgs : EventArgs
    {
        public ModuleEnabledChangedEventArgs(CounterType counterType, bool isEnabled)
        {
            CounterType = counterType;
            IsEnabled = isEnabled;
        }

        public CounterType CounterType { get; private set; }

        public bool IsEnabled { get; private set; }
    }
}
