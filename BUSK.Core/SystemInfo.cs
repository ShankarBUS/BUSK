using System;

namespace BUSK.Core
{
    public class SystemInfo
    {
        public static Lazy<VersionInfo> Version = new Lazy<VersionInfo>(() => GetVersionInfo());

        internal static VersionInfo GetVersionInfo()
        {
            var v = Environment.OSVersion.Version;
            var vi = new VersionInfo(v.Major, v.Minor, v.Build);
            return vi;
        }
    }
}
