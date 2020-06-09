using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace BUSK.Core
{
    [Serializable]
    [XmlRoot]
    public sealed class SettingsCollector
    {
        [XmlIgnore]
        internal List<SettingBase> Settings { get; private set; } = new List<SettingBase>();

        [XmlArray]
        public List<SettingWrapper> Wrappers { get; private set; } = new List<SettingWrapper>();

        private const string SettingsDirectory = @".\Settings\";

        private const string SettingsListPath = @".\Settings.xml";

        [XmlIgnore]
        public static SettingsCollector Instance { get; set; }

        public SettingsCollector()
        {

        }

        internal static void Initialize()
        {
            if (File.Exists(SettingsListPath))
            {
                try
                {
                    var x = new XmlSerializer(typeof(SettingsCollector));
                    var w = new StreamReader(SettingsListPath);
                    Instance = (SettingsCollector)x.Deserialize(w);
                    w.Close(); w.Dispose();
                }
                catch
                {
                    DeleteConfigs();
                    Instance = new SettingsCollector();
                }
            }
            else
            {
                DeleteConfigs();
                Instance = new SettingsCollector();
            }
            if (!Directory.Exists(SettingsDirectory))
            {
                Directory.CreateDirectory(SettingsDirectory);
            }

            Instance.LoadSettings();
        }

        private static void DeleteConfigs()
        {
            if (File.Exists(SettingsListPath))
            {
                File.Delete(SettingsListPath);
            }
            if (Directory.Exists(SettingsDirectory))
            {
                var dir = new DirectoryInfo(SettingsDirectory);
                var files = dir.GetFiles();
                foreach (var file in files)
                {
                    file.Delete();
                }
                dir.Delete();
            }
        }

        private void LoadSettings()
        {
            foreach (var wrapper in Wrappers)
            {
                string location = SettingsDirectory + wrapper.ID + ".xml";
                if (File.Exists(location))
                {
                    SettingBase setting;
                    var type = Type.GetType(wrapper.SettingTypeString);
                    var x = new XmlSerializer(type);
                    var w = new StreamReader(location);
                    setting = (SettingBase)x.Deserialize(w);
                    w.Close(); w.Dispose();

                    setting.ID = wrapper.ID;
                    Settings.Add(setting);
                }
            }
            Wrappers.Clear(); Wrappers = null;
        }

        public void Save()
        {
            Wrappers = new List<SettingWrapper>();
            foreach (var settingBase in Settings)
            {
                var x = new XmlSerializer(settingBase.GetType());
                var w = new StreamWriter(SettingsDirectory + settingBase.ID + ".xml");
                x.Serialize(w, settingBase);
                w.Flush(); w.Close(); w.Dispose();

                Wrappers.Add(new SettingWrapper(settingBase));
            }
            SaveInstance();
            Wrappers.Clear(); Wrappers = null;
        }

        private void SaveInstance()
        {
            var x = new XmlSerializer(typeof(SettingsCollector));
            var w = new StreamWriter(SettingsListPath);
            x.Serialize(w, this);
            w.Flush(); w.Close(); w.Dispose();
        }

        public void Add<T>(Setting<T> setting)
        {
            if (!SetExistingValue(setting.Name, setting.Value))
            {
                Settings.Add(setting);
            }
        }

        public void Add<T>(string name, T value)
        {
            if (!SetExistingValue(name, value))
            {
                Settings.Add(new Setting<T>(name, value));
            }
        }

        private bool SetExistingValue<T>(string name, T value)
        {
            var setting = GetSetting<T>(name);

            if (setting != null)
            {
                setting.Value = value;
                return true;
            }

            return false;
        }

        public void Remove<T>(Setting<T> setting)
        {
            if (Settings.Contains(setting))
            {
                Settings.Remove(setting);
            }
        }

        public Setting<T> GetSetting<T>(string name)
        {
            foreach (var settingBase in Settings)
            {
                if (settingBase is Setting<T> setting)
                {
                    if (setting.Name == name)
                    {
                        return setting;
                    }
                }
            }
            return null;
        }
    }
}
