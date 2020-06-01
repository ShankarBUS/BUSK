using BUSK.Core.Utilities;
using System.Diagnostics;

namespace BUSK.Core.Diagnostics
{
    public class RAMPerfManager : PerfManagerBase
    {
        private PerformanceCounter memCounter = new PerformanceCounter("Memory", "Available Bytes");

        public static RAMPerfManager Instance { get; internal set; }

        private bool _isinit = false;

        #region Properties

        private string amem = "";

        public string AvailableRAM
        {
            get { return amem; }
            private set { SetPropertyValue(ref amem, value); }
        }

        private string amemns = "";

        public string AvailableRAMNoSuffix
        {
            get { return amemns; }
            private set { SetPropertyValue(ref amemns, value); }
        }

        private long amemb = 0;

        public long AvailableRAMBytes
        {
            get { return amemb; }
            private set { SetPropertyValue(ref amemb, value); }
        }

        private double ramu = 0.0;

        public double RAMUsage
        {
            get { return ramu; }
            private set { SetPropertyValue(ref ramu, value); }
        }

        private string rt = "";

        public string RAMUsageText
        {
            get { return rt; }
            private set { SetPropertyValue(ref rt, value); }
        }

        private string tmem = "";

        public string TotalRAM
        {
            get { return tmem; }
            private set { SetPropertyValue(ref tmem, value); }
        }

        private string tmemns = "";

        public string TotalRAMNoSuffix
        {
            get { return tmemns; }
            private set { SetPropertyValue(ref tmemns, value); }
        }

        private long tmemb = 0;

        public long TotalRAMBytes
        {
            get { return tmemb; }
            private set { SetPropertyValue(ref tmemb, value); }
        }

        #endregion

        public override void Update()
        {
            var _amemb = (long)memCounter.NextValue();
            AvailableRAMBytes = _amemb;
            AvailableRAM = DataConverter.FormatBytes(_amemb);
            AvailableRAMNoSuffix = DataConverter.FormatBytes(_amemb, false);
            
            if (!_isinit) return;

            double us = (1 - ((double)amemb / (double)tmemb)) * 100.0;

            RAMUsage = us;
            RAMUsageText = ((int)us).ToString() + "%";
        }

        internal void SetTotalRAM(long _tmemb)
        {
            if (_tmemb < 1) return;

            TotalRAMBytes = _tmemb;
            TotalRAM = DataConverter.FormatBytes(tmemb);
            TotalRAMNoSuffix = DataConverter.FormatBytes(tmemb, false);

            _isinit = true;
        }
    }
}
