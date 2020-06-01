using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BUSK.Core
{
    public class SettingsManager
    {
        private static SettingsCollector SettingsCollecter { get; set; }

        #region Properties
        
        private static bool cme = true;

        public static bool CPUModuleEnabled
        {
            get { return cme; }
            set
            {
                if (cme != value)
                {
                    cme = value;
                    SettingsCollecter.Add(new Setting(nameof(CPUModuleEnabled), value));
                    SettingsCollecter.Save();
                    OnStaticPropertyChanged();
                }
            }
        }

        private static bool dme = true;

        public static bool DiskModuleEnabled
        {
            get { return dme; }
            set
            {
                if (dme != value)
                {
                    dme = value;
                    SettingsCollecter.Add(new Setting(nameof(DiskModuleEnabled), value));
                    SettingsCollecter.Save();
                    OnStaticPropertyChanged();
                }
            }
        }

        private static bool nme = true;

        public static bool NetModuleEnabled
        {
            get { return nme; }
            set
            {
                if (nme != value)
                {
                    nme = value;
                    SettingsCollecter.Add(new Setting(nameof(NetModuleEnabled), value));
                    SettingsCollecter.Save();
                    OnStaticPropertyChanged();
                }
            }
        }

        private static bool rme = true;

        public static bool RAMModuleEnabled
        {
            get { return rme; }
            set
            {
                if (rme != value)
                {
                    rme = value;
                    SettingsCollecter.Add(new Setting(nameof(RAMModuleEnabled), value));
                    SettingsCollecter.Save();
                    OnStaticPropertyChanged();
                }
            }
        } 
        
        #endregion

        static SettingsManager()
        {
            SettingsCollecter = SettingsCollector.Instance();
        }

        public static void Initialize()
        {
            BuskInterop.AddSplashScreenMessage("Initializing Settings");
            SettingsCollector sc = SettingsCollecter;
            var cmes = sc.GetSetting(nameof(CPUModuleEnabled));
            var dmes = sc.GetSetting(nameof(DiskModuleEnabled));
            var nmes = sc.GetSetting(nameof(NetModuleEnabled));
            var rmes = sc.GetSetting(nameof(RAMModuleEnabled));

            if (cmes != null) { CPUModuleEnabled = (bool)cmes.Value; }
            else { CPUModuleEnabled = true; sc.Add(new Setting(nameof(CPUModuleEnabled), true)); }
            if (dmes != null) { DiskModuleEnabled = (bool)dmes.Value; }
            else { DiskModuleEnabled = true; sc.Add(new Setting(nameof(DiskModuleEnabled), true)); }
            if (nmes != null) { NetModuleEnabled = (bool)nmes.Value; }
            else { NetModuleEnabled = true; sc.Add(new Setting(nameof(NetModuleEnabled), true)); }
            if (rmes != null) { RAMModuleEnabled = (bool)rmes.Value; }
            else { RAMModuleEnabled = true; sc.Add(new Setting(nameof(RAMModuleEnabled), true)); }
            sc.Save();
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        protected static void OnStaticPropertyChanged([CallerMemberName] string propertyName = "")
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
