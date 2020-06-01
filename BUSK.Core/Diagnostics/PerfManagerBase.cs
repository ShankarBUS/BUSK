using System;
using System.Threading.Tasks;

namespace BUSK.Core.Diagnostics
{
    public abstract class PerfManagerBase : BindableBase
    {
        private System.Timers.Timer Timer { get; } = new System.Timers.Timer() { Interval = CountersHandler.DefaultInterval, Enabled = false };

        #region Properties

        private double interval = CountersHandler.DefaultInterval;

        public double Interval
        {
            get { return interval; }
            internal set { if (CountersHandler.IsIntervalValid(value) && SetPropertyValue(ref interval, value)) { SetInterval(value); } }
        }

        private bool isGlobalIntervalRespected = true;

        protected bool IsGlobalIntervalRespected
        {
            get { return isGlobalIntervalRespected; }
            set { SetPropertyValue(ref isGlobalIntervalRespected, value); }
        }

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { if (SetPropertyValue(ref isEnabled, value)) { OnIsEnabledChanged(); } }
        }

        #endregion

        public PerfManagerBase()
        {
            Timer.Elapsed += Timer_Elapsed;
            CountersHandler.CommonCounterIntervalChanged += CountersHandler_CommonCounterIntervalChanged;
            CountersHandler.IsRunningChanged += CountersHandler_IsRunningChanged;
        }

        protected virtual void OnRun() 
        {
            if (isEnabled) Timer.Start();
        }

        protected virtual void OnSuspend() 
        {
            Timer.Stop();
        }

        public abstract void Update();

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            await Task.Run(() => { Update(); });
        }

        private void OnIsEnabledChanged()
        {
            if (isEnabled)
            {
                if (CountersHandler.IsRunning) OnRun();
            }
            else 
            {
                Timer.Stop();
            }
        }

        private void CountersHandler_CommonCounterIntervalChanged(object sender, EventArgs e)
        {
            if (isGlobalIntervalRespected) Interval = CountersHandler.CommonInterval;
        }

        private void CountersHandler_IsRunningChanged(object sender, EventArgs e)
        {
            if (CountersHandler.IsRunning)
            {
                OnRun();
            }
            else 
            {
                OnSuspend();
            }
        }

        private void SetInterval(double interval)
        {
            bool wasrunning = Timer.Enabled;
            Timer.Stop();
            Timer.Interval = interval;
            if (isEnabled && wasrunning) OnRun();
        }

        ~PerfManagerBase()
        {
            Timer.Elapsed -= Timer_Elapsed;
            CountersHandler.CommonCounterIntervalChanged -= CountersHandler_CommonCounterIntervalChanged;
            CountersHandler.IsRunningChanged -= CountersHandler_IsRunningChanged;
        }
    }
}
