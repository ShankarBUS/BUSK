using BUSK.Core.Diagnostics;
using System;

namespace BUSK.Controls.Plotting
{
    public class PerformancePlotter : PerfManagerBase
    {
        public PerformancePlotter(Action action)
        {
            _action = action;
            IsGlobalIntervalRespected = false;
        }

        public static PerformancePlotter Instance { get; set; }

        private Action _action;

        public override void Update()
        {
            _action.Invoke();
        }
    }
}
