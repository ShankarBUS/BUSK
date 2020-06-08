using System;
using System.Windows.Threading;
using BUSK.Charting.Dtos;
using BUSK.Charting.WPF.Charts.Base;

namespace BUSK.Charting.WPF.Components
{
    internal class ChartUpdater : BUSK.Charting.ChartUpdater
    {
        public ChartUpdater(TimeSpan frequency)
        {
            Timer = new DispatcherTimer {Interval = frequency};

            Timer.Tick += OnTimerOnTick;
            Freq = frequency;
        }

        public DispatcherTimer Timer { get; set; }
        private bool RequiresRestart { get; set; }
        private TimeSpan Freq { get; set; }

        public override void Run(bool restartView = false, bool updateNow = false)
        {
            if (Timer == null)
            {
                Timer = new DispatcherTimer {Interval = Freq};
                Timer.Tick += OnTimerOnTick;
                IsUpdating = false;
            }

            if (updateNow)
            {
                UpdaterTick(restartView, true);
                return;
            }

            RequiresRestart = restartView || RequiresRestart;
            if (IsUpdating) return;

            IsUpdating = true;
            Timer.Start();
        }

        public override void UpdateFrequency(TimeSpan freq)
        {
            Timer.Interval = freq;
        }

        public void OnTimerOnTick(object sender, EventArgs args)
        {
            UpdaterTick(RequiresRestart, false);
        }

        private void UpdaterTick(bool restartView, bool force)
        {
            var wpfChart = (Chart) Chart.View;
            
            if (!force && !wpfChart.IsVisible && !wpfChart.IsMocked) return;

            Chart.ControlSize = wpfChart.IsMocked
                ? wpfChart.Model.ControlSize
                : new CoreSize(wpfChart.ActualWidth, wpfChart.ActualHeight);

            Timer.Stop();
            Update(restartView, force);
            IsUpdating = false;

            RequiresRestart = false;
            
            wpfChart.ChartUpdated();
            wpfChart.PrepareScrolBar();
        }
    }
}
