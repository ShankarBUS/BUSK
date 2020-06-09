using System;

namespace BUSK.Core.Utilities
{
    public sealed class DataConverter
    {
        /// <summary>
        /// Converts the given bytes into a human readable format (i.e. KB, MB, GB, etc.) 
        /// </summary>
        /// <param name="bytecount">The total bytes to be formatted</param>
        /// <param name="addSuffix">Specifies whether to add <strong>Unit</strong> suffix (eg. 20 <strong>MB</strong>)</param>
        /// <returns>The formatted value of the given bytes</returns>
        public static string FormatBytes(long bytecount, bool addSuffix = true)
        {
            string[] suf = { " Bytes", " KB", " MB", " GB", " TB", " PB", " EB" };

            if (bytecount == 0L) { return "0" + (addSuffix ? suf[0] : ""); }

            long bytes = Math.Abs(bytecount);
            int place = (int)Math.Floor(Math.Log(bytes,1024));
            double num = Math.Round(bytes / Math.Pow(1024, place), 2);
            return (Math.Sign(bytecount) * num).ToString() + (addSuffix ? suf[place] : "");
        }
    }
}
