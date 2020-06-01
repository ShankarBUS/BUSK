using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using BUSK.Core.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace BUSK.Core.Diagnostics
{
    public class DiskPerfManager : PerfManagerBase
    {
        private PerformanceCounter disk;

        public event EventHandler DiskCountersChanged;

        public event EventHandler DiskCounterAssigned;

        public static DiskPerfManager Instance { get; internal set; }

        #region Properties

        public ObservableCollection<string> DiskCounters { get; private set; } = new ObservableCollection<string>();

        private double disku = 0.0;

        public double DiskUsage
        {
            get { return disku; }
            private set { SetPropertyValue(ref disku, value); }
        }

        private string dt = "";

        public string DiskUsageText
        {
            get { return dt; }
            private set { SetPropertyValue(ref dt, value); }
        }

        private string currentDiskName = "";

        public string CurrentDiskName
        {
            get { return currentDiskName; }
            set { if (value != null && SetPropertyValue(ref currentDiskName, value)) { AssignCurrentDiskCounter(value); } }
        }

        #endregion

        public DiskPerfManager()
        {
            DiskHandler.DiskUpdate += (s, e) => LoadDiskCounters();
        }

        protected override void OnRun()
        {
            base.OnRun();
            LoadDiskCounters();
        }

        private void AssignCurrentDiskCounter(string instancename)
        {
            disk = new PerformanceCounter("PhysicalDisk", "% Disk Time", instancename);
            DiskCounterAssigned?.Invoke(this, null);
        }

        public override void Update()
        {
            if (disk == null) { DiskUsage = 0; DiskUsageText = "0%"; return; }
            try
            {
                var _ = (double)disk.NextValue();
                DiskUsage = Math.Min(_, 100);
            } catch { }
            DiskUsageText = ((int)disku).ToString()  + "%";
        }

        private void LoadDiskCounters()
        {
            PerformanceCounterCategory counterCategory = new PerformanceCounterCategory("PhysicalDisk");
            var instanceNames = counterCategory.GetInstanceNames().Except(new [] { "_Total" });

            List<string> toAdd = new List<string>();
            List<string> toRemove = new List<string>();

            foreach(var i in instanceNames)
            {
                if (!DiskCounters.Any(x => x == i))
                {
                    toAdd.Add(i);
                }
            }

            foreach(var i in DiskCounters)
            {
                if(!instanceNames.Any(x => x == i))
                {
                    toRemove.Add(i);
                }
            }

            foreach(var newone in toAdd)
            {
                DiskCounters.Add(newone);
            }

            foreach(var oldone in toRemove)
            {
                DiskCounters.Remove(oldone);
            }

            if (toRemove.Any(x => x == CurrentDiskName) && DiskCounters.Count > 0)
            {
                CurrentDiskName = DiskCounters[0];
            }

            toAdd.Clear(); toAdd = null; toRemove.Clear(); toRemove = null;

            if (disk == null && DiskCounters.Count > 0)
            {
                CurrentDiskName = DiskCounters[0];

                foreach (var instancename in DiskCounters)
                {
                    if (instancename.StartsWith("0"))
                    {
                        CurrentDiskName = instancename;
                        break;
                    }
                }
            }

            DiskCountersChanged?.Invoke(this, null);
        }
    }
}
