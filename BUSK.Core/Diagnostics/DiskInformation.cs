using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using BUSK.Core.Utilities;
using System.Linq;
using System.Collections.Generic;

namespace BUSK.Core.Diagnostics
{
    public sealed class DiskInformation : CounterBase
    {
        private PerformanceCounter disktime;

        private PerformanceCounter diskreads;

        private PerformanceCounter diskwrites;

        public event EventHandler DiskCountersChanged;

        public event EventHandler DiskCounterAssigned;

        public static DiskInformation Instance { get; internal set; }

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

        private string read;

        public string Read
        {
            get { return read; }
            private set { SetPropertyValue(ref read, value); }
        }

        private double readBytes;

        public double ReadBytes
        {
            get { return readBytes; }
            private set { SetPropertyValue(ref readBytes, value); }
        }

        private string write;

        public string Write
        {
            get { return write; }
            private set { SetPropertyValue(ref write, value); }
        }

        private double writeBytes;

        public double WriteBytes
        {
            get { return writeBytes; }
            private set { SetPropertyValue(ref writeBytes, value); }
        }

        private string currentDiskName = "";

        public string CurrentDiskName
        {
            get { return currentDiskName; }
            set { if (value != null && SetPropertyValue(ref currentDiskName, value)) { AssignCurrentDiskCounter(value); } }
        }

        #endregion

        public DiskInformation()
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
            disktime = new PerformanceCounter("PhysicalDisk", "% Disk Time", instancename);
            diskreads = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", instancename);
            diskwrites = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", instancename);
            DiskCounterAssigned?.Invoke(this, null);
        }

        public override void Update()
        {
            if (disktime == null || diskreads == null || diskwrites == null)
            {
                DiskUsage = 0;
                DiskUsageText = "0%";
                ReadBytes = 0;
                Read = "";
                WriteBytes = 0;
                Write = "";
                return;
            }
            try
            {
                var activetime = Math.Min((double)disktime.NextValue(), 100);
                DiskUsage = activetime;
                DiskUsageText = ((int)disku).ToString() + "%";

                var readspeed = Math.Max((double)diskreads.NextValue(), 0);
                ReadBytes = readspeed;
                Read = DataConverter.FormatBytes((long)readspeed) + "/s";

                var writespeed = Math.Max((double)diskwrites.NextValue(), 0);
                WriteBytes = writespeed;
                Write = DataConverter.FormatBytes((long)writespeed) + "/s";
            } catch { }
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

            if (disktime == null && DiskCounters.Count > 0)
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
