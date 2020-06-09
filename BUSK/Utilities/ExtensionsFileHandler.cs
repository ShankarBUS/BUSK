using BUSK.Core.Extensibility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace BUSK.Utilities
{
    public class ExtensionsFileHandler
    {
        public static string GetOtherFileWithExtension(string path, string ext)
        {
            string[] pathSplit = path.Split('.');
            string o = "";
            for (int i = 0; i < pathSplit.Length - 1; i++)
            {
                o += pathSplit[i] + ".";
            }
            o += ext;

            return o;
        }

        public static ExtensionBase LoadExtensionBaseFromFile(string location)
        {
            try
            {
                byte[] raw = File.ReadAllBytes(location);
                Assembly ass = Assembly.Load(raw);
                string tn = "TypeName";
                Type[] types = ass.GetExportedTypes();
                foreach (Type type in types)
                {
                    if (type.BaseType == typeof(ExtensionBase))
                    {
                        tn = type.FullName;
                    }
                }

                if (tn != "TypeName")
                {
                    ExtensionBase extensionBase = (ExtensionBase)ass.CreateInstance(tn);
                    return extensionBase;
                }
                else { return null; }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(nameof(ExtensionsFileHandler) + " - " + ex.Message);
                return null;
            }
        }

        public const string cfgfileext = "extconfig";

        public static void LoadExtensionBases()
        {
            App.AddMessage("Initializing Extensions");

            var dir = new DirectoryInfo(@".\Extensions");
            foreach (FileInfo file in dir.GetFiles($"*.{cfgfileext}", SearchOption.AllDirectories))
            {
                try
                {
                    var x = new XmlSerializer(typeof(ExtensionInfo));
                    var r = new StreamReader(file.FullName);
                    var ex = (ExtensionInfo)x.Deserialize(r);

                    var min = ex.MinimumWindowsVersion;
                    var max = ex.MaximumWindowsVersion;
                    var current = Core.SystemInfo.Version.Value;

                    if (IsCompatible(min, max, current)) { LoadIt(); }

                    void LoadIt()
                    {
                        string dllloc = GetOtherFileWithExtension(file.FullName, "dll");
                        string dllname = GetOtherFileWithExtension(file.Name, "dll");
                        paths.Add(dllname, file.DirectoryName);

                        if (File.Exists(dllloc))
                        {
                            var exbase = LoadExtensionBaseFromFile(dllloc);

                            var ext = exbase.ExtensionInfo;
                            ext.IsEnabled = ex.IsEnabled;

                            ExtensionsManager.Instance.Add(exbase, file.FullName, dllloc, file.Directory.FullName, dllname);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(nameof(ExtensionsFileHandler) + " - " + ex.Message);
                }
            }
        }

        public static bool IsCompatible(Core.VersionInfo minver, Core.VersionInfo maxver, Core.VersionInfo current)
        {
            return (minver <= current & (maxver == default) ? true : current <= maxver );
        }

        public static void SaveExtConfig(object s, string configLocation)
        {
            var ex = s as ExtensionInfo;
            var x = new XmlSerializer(typeof(ExtensionInfo));
            var w = new StreamWriter(configLocation);
            x.Serialize(w, ex);
            w.Flush(); w.Close(); w.Dispose();
        }

        public static Dictionary<string, string> paths = new Dictionary<string, string>();
    }
}
