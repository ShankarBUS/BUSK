using System.Collections.Generic;

namespace BUSK.Core
{
    public static class VersionInfos
    {
        public static List<VersionItem> VersionItems { get; } = new List<VersionItem>();

        public static void Initialize()
        {
            BuskInterop.AddSplashScreenMessage("Initializing Version Infos");
            var versionItems = new VersionItem[]
            {
            new VersionItem("Windows 7", VersionInfos.Windows7),
            new VersionItem("Windows 7 SP1", VersionInfos.Windows7_SP1),
            new VersionItem("Windows 8", VersionInfos.Windows8),
            new VersionItem("Windows 8.1", VersionInfos.Windows8_1),
            new VersionItem("Windows 10", VersionInfos.Windows10),
            new VersionItem("Windows 10 1511", VersionInfos.Windows10_1511),
            new VersionItem("Windows 10 1607", VersionInfos.Windows10_1607),
            new VersionItem("Windows 10 1703", VersionInfos.Windows10_1703),
            new VersionItem("Windows 10 1709", VersionInfos.Windows10_1709),
            new VersionItem("Windows 10 1803", VersionInfos.Windows10_1803),
            new VersionItem("Windows 10 1809", VersionInfos.Windows10_1809),
            new VersionItem("Windows 10 1903", VersionInfos.Windows10_1903)
            };
            VersionItems.AddRange(versionItems);
        }

        public static VersionInfo Windows7 { get { return new VersionInfo(6, 1, 7600); } }
        public static VersionInfo Windows7_SP1 { get { return new VersionInfo(6, 1, 7601); } }

        public static VersionInfo Windows8 { get { return new VersionInfo(6, 2, 9200); } }
        public static VersionInfo Windows8_1 { get { return new VersionInfo(6, 3, 9600); } }

        public static VersionInfo Windows10 { get { return new VersionInfo(10, 0, 10240); } }
        public static VersionInfo Windows10_1511 { get { return new VersionInfo(10, 0, 10586); } }
        public static VersionInfo Windows10_1607 { get { return new VersionInfo(10, 0, 14393); } }
        public static VersionInfo Windows10_1703 { get { return new VersionInfo(10, 0, 15063); } }
        public static VersionInfo Windows10_1709 { get { return new VersionInfo(10, 0, 16299); } }
        public static VersionInfo Windows10_1803 { get { return new VersionInfo(10, 0, 17134); } }
        public static VersionInfo Windows10_1809 { get { return new VersionInfo(10, 0, 17763); } }
        public static VersionInfo Windows10_1903 { get { return new VersionInfo(10, 0, 18362); } }
    }

    public sealed class VersionItem
    {
        public VersionItem(string name, VersionInfo version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; set; }
        public VersionInfo Version { get; set; }
    }
}
