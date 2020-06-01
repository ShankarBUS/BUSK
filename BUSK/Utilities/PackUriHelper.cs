using System;

namespace BUSK.Utilities
{
    internal static class PackUriHelper
    {
        public static Uri GetAbsoluteUri(string path)
        {
            return new Uri($"pack://application:,,,/BUSK;component/{path}");
        }
    }
}
