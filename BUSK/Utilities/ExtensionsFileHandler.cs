using BUSK.Core.Extensibility;
using McMaster.NETCore.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

        public static ExtensionBase LoadExtensionBase(PluginLoader loader)
        {
            Type extensionBaseType = loader
                    .LoadDefaultAssembly()
                    .GetTypes()
                    .FirstOrDefault(t => t.IsSubclassOf(typeof(ExtensionBase)) && !t.IsAbstract);

            if (extensionBaseType == null)
            {
                return null;
            }

            ExtensionBase extensionBase = (ExtensionBase)Activator.CreateInstance(extensionBaseType);
            return extensionBase;
        }

        public const string cfgfileext = "extinfo";

        public static void LoadExtensionBases()
        {
            App.AddMessage("Initializing Extensions");

            var loaders = new List<PluginLoader>();

            var dir = new DirectoryInfo(@".\Extensions");
            foreach (FileInfo file in dir.GetFiles($"*.{cfgfileext}", SearchOption.AllDirectories))
            {
                try
                {
                    var x = new XmlSerializer(typeof(ExtensionInfo));
                    var r = new StreamReader(file.FullName);
                    var exi = (ExtensionInfo)x.Deserialize(r);

                    var min = exi.MinimumWindowsVersion;
                    var max = exi.MaximumWindowsVersion;
                    var current = Core.SystemInfo.Version.Value;

                    if (IsCompatible(min, max, current)) { LoadIt(); }

                    void LoadIt()
                    {
                        string dllloc = GetOtherFileWithExtension(file.FullName, "dll");
                        string dllname = GetOtherFileWithExtension(file.Name, "dll");

                        if (File.Exists(dllloc))
                        {
                            var loader = GetDefaultLoader(dllloc);
                            loaders.Add(loader);

                            var extensionBase = LoadExtensionBase(loader);

                            if (extensionBase != null)
                            {
                                var extensionInfo = extensionBase.ExtensionInfo;
                                extensionInfo.IsEnabled = exi.IsEnabled;

                                ExtensionsManager.Instance.Add(extensionBase, file.FullName, dllloc, file.Directory.FullName, dllname);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(nameof(ExtensionsFileHandler) + " - " + ex.Message);
                }
            }
        }

        public static bool TryGenerateAndSaveInfoFromExtensionAssembly(string dllloc)
        {
            if (File.Exists(dllloc))
            {
                var loader = GetDefaultLoader(dllloc);

                var extensionBase = LoadExtensionBase(loader);

                if (extensionBase != null)
                {
                    var infopath = GetOtherFileWithExtension(dllloc, cfgfileext);
                    SaveExtInfo(extensionBase.ExtensionInfo, infopath);
                    extensionBase = null;
                    loader.Dispose();

                    return true;
                }
            }

            return false;
        }

        private static PluginLoader GetDefaultLoader(string dllloc)
        {
            return PluginLoader.CreateFromAssemblyFile(
                                dllloc,
                                isUnloadable: true,
                                sharedTypes: new Type[]
                                {
                                    typeof(ExtensionBase),
                                    typeof(UI.BuskBar.IBuskBarItem),
                                    typeof(ModernWpf.ApplicationTheme),
                                    typeof(ModernWpf.Controls.AppBarButton),
                                    typeof(System.Drawing.Image),
                                });
        }

        public static bool IsCompatible(Core.VersionInfo minver, Core.VersionInfo maxver, Core.VersionInfo current)
        {
            return (minver <= current & (maxver == default) ? true : current <= maxver );
        }

        public static void SaveExtInfo(object s, string configLocation)
        {
            var ex = s as ExtensionInfo;
            var x = new XmlSerializer(typeof(ExtensionInfo));
            var w = new StreamWriter(configLocation);
            x.Serialize(w, ex);
            w.Flush(); w.Close(); w.Dispose();
        }
    }
}
