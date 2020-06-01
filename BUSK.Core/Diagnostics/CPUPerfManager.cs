using System.Diagnostics;

namespace BUSK.Core.Diagnostics
{
    public class CPUPerfManager : PerfManagerBase
    {
        private PerformanceCounter cpu = new PerformanceCounter("Processor", "% Idle Time", "_Total");

        public static CPUPerfManager Instance { get; internal set; }

        #region Properties

        private double cpuu = 0.0;

        public double CPUUsage
        {
            get { return cpuu; }
            private set { SetPropertyValue(ref cpuu, value); }
        }

        private string ct = "";

        public string CPUUsageText
        {
            get { return ct; }
            private set { SetPropertyValue(ref ct, value); }
        }

        #endregion

        public override void Update()
        {
            CPUUsage = 100.0 - cpu.NextValue();
            CPUUsageText = ((int)cpuu).ToString()  + "%";
        }
    }
}
