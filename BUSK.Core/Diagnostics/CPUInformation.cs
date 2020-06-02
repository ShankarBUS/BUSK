using System.Diagnostics;

namespace BUSK.Core.Diagnostics
{
    public class CPUInformation : CounterBase
    {
        private PerformanceCounter cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");

        public static CPUInformation Instance { get; internal set; }

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
            CPUUsage = cpu.NextValue();
            CPUUsageText = ((int)cpuu).ToString()  + "%";
        }
    }
}
