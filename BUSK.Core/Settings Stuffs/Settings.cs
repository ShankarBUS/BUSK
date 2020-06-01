using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace BUSK.Core
{
    [Serializable]
    public class Setting
    {
        public Setting(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public object Value { get; set; }
    }

    [Serializable]
    public class SettingsCollector
    {
        public readonly List<Setting> Settings;

        private const string C_NOT_SPPRTD_FS_FILE_MSG = "The settings file is corrupt!";

        private const string C_SETTINGS_PATH = @".\Settings.cfg";

        private static SettingsCollector sc = null;

        public static SettingsCollector Instance()
        {
            if(sc == null) { Init(); }
            return sc;
        }

        public SettingsCollector()
        {
            Settings = new List<Setting>();
        }

        private static void Init()
        {
            if (File.Exists(C_SETTINGS_PATH)) 
            {
                try
                {
                    var fs = new FileStream(C_SETTINGS_PATH, FileMode.Open);
                    var bf = new BinaryFormatter();
                    sc = (SettingsCollector)bf.Deserialize(fs);
                    fs.Flush(); fs.Close(); fs.Dispose();
                }
                catch (Exception e) { throw new NotSupportedException(C_NOT_SPPRTD_FS_FILE_MSG, e); }
            }
            else 
            {
                sc = new SettingsCollector();
            }
        }

        public void Save()
        {
            try
            {
                var fs = File.Open(C_SETTINGS_PATH, FileMode.OpenOrCreate);
                var bf = new BinaryFormatter();
                bf.Serialize(fs, this);
                fs.Flush(); fs.Close(); fs.Dispose();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Clear()
        {
            Settings.Clear();
        }

        public int Count()
        {
            return Settings.Count;
        }

        public void Add(Setting val)
        {
            foreach( Setting sett in Settings)
            {
                if(sett.Name == val.Name)
                {
                    sett.Value = val.Value;
                    return;
                }
            }
            Settings.Add(val);
        }

        public void Remove(Setting val)
        {
            Settings.Remove(val);
        }

        public Setting GetSetting(string name)
        {
            foreach(Setting sett in Settings)
            {
                if(sett.Name == name)
                {
                    return sett;
                }
            }
            return null;
        }
    }
}
