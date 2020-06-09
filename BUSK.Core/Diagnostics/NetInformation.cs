using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Diagnostics;
using BUSK.Core.Utilities;
using System.Collections.Generic;

namespace BUSK.Core.Diagnostics
{
    public sealed class NetInterface
    {
        public string Name { get; set; } = "";

        public string Id { get; set; } = "";

        public override string ToString()
        {
            return Name;
        }
    }

    public sealed class NetInformation : CounterBase
    {
        private long prevDown = 0;

        private long prevUp = 0;

        public string DefaultCounterName = "WiFi";

        private int currentIndex = 0;

        private NetworkInterface[] prevNetworkInterfaces;

        public event EventHandler NetworkInterfacesChanged;

        public event EventHandler NetworkInterfaceAssigned;

        public static NetInformation Instance { get; internal set; }

        [Flags]
        public enum Required
        {
            Up = 1,
            Down = 2,
            TotalUp = 4,
            TotalDown = 8
        }

        #region Properties

        public ObservableCollection<NetInterface> NetInterfaces { get; private set; } = new ObservableCollection<NetInterface>();

        private NetInterface currentNetInterface = default;

        public NetInterface CurrentNetInterface
        {
            get { return currentNetInterface; }
            set { if (SetPropertyValue(ref currentNetInterface, value)) { AssignCurrentNetworkInterface(value); } }
        }

        private string down;

        public string Down
        {
            get { return down; }
            private set { SetPropertyValue(ref down, value); }
        }

        private double downBytes;

        public double DownBytes
        {
            get { return downBytes; }
            private set { SetPropertyValue(ref downBytes, value); }
        }

        private string totalDown;

        public string TotalDown
        {
            get { return totalDown; }
            private set { SetPropertyValue(ref totalDown, value); }
        }

        private double totalDownBytes;

        public double TotalDownBytes
        {
            get { return totalDownBytes; }
            private set { SetPropertyValue(ref totalDownBytes, value); }
        }

        private string up;

        public string Up
        {
            get { return up; }
            private set { SetPropertyValue(ref up, value); }
        }

        private double upBytes;

        public double UpBytes
        {
            get { return upBytes; }
            private set { SetPropertyValue(ref upBytes, value); }
        }

        private string totalUp;

        public string TotalUp
        {
            get { return totalUp; }
            private set { SetPropertyValue(ref totalUp, value); }
        }

        private double totalUpBytes;

        public double TotalUpBytes
        {
            get { return totalUpBytes; }
            private set { SetPropertyValue(ref totalUpBytes, value); }
        }

        private bool isNetAvailable = false;

        public bool IsNetAvailable
        {
            get { return isNetAvailable; }
            private set { SetPropertyValue(ref isNetAvailable, value); }
        }

        #endregion

        public NetInformation()
        {
            IsGlobalIntervalRespected = false;

            var NIS = NetworkInterface.GetAllNetworkInterfaces();

            UpdateNetworkInterfaces(NIS);
        }

        protected override void OnRun()
        {
            base.OnRun();

            IsNetAvailable = NetworkInterface.GetIsNetworkAvailable();

            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;

            prevNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            prevDown = prevNetworkInterfaces[currentIndex].GetIPv4Statistics().BytesReceived;
            prevUp = prevNetworkInterfaces[currentIndex].GetIPv4Statistics().BytesSent;
        }

        protected override void OnSuspend()
        {
            base.OnSuspend();
            NetworkChange.NetworkAvailabilityChanged -= NetworkChange_NetworkAvailabilityChanged;
        }

        private void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            IsNetAvailable = e.IsAvailable;
        }

        public static int GetIndexOfNetInterface(NetworkInterface[] NIS, NetInterface netInterface)
        {
            for(int i = 0; i < NIS.Count(); i++)
            {
                if (NIS[i].Id == netInterface.Id)
                {
                    return i;
                }
            }

            return 0;
        }

        public static string GetFormattedMACAddress(string MAC)
        {
            var adMAC = "";
            for (int i = 1; i <= MAC.Length; i += 2)
            {
                adMAC += MAC.Substring(i - 1, 1) + MAC.Substring(i, 1) + ":";
            }
            return adMAC.Length > 0 ? adMAC.Remove(adMAC.Length - 1) : adMAC;
        }

        public static string GetIPv4(NetworkInterface networkInterface)
        {
            var o = "";
            var ips = networkInterface.GetIPProperties().UnicastAddresses;
            foreach (var IpInfo in ips)
            {
                if(IpInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    o = IpInfo.Address.ToString();
                }
            }
            return o;
        }

        public static string GetIPv6(NetworkInterface networkInterface)
        {
            var ips = networkInterface.GetIPProperties().UnicastAddresses;
            var o = ips[0].Address.ToString();
            foreach (var IpInfo in ips)
            {
                if (IpInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                {
                    o = IpInfo.Address.ToString();
                }
            }
            return o;
        }

        private void AssignCurrentNetworkInterface(NetInterface netInterface)
        {
            var NIS = NetworkInterface.GetAllNetworkInterfaces();
            prevNetworkInterfaces = NIS;
            currentIndex = GetIndexOfNetInterface(NIS, netInterface);
            prevDown = NIS[currentIndex].GetIPv4Statistics().BytesReceived;
            prevUp = NIS[currentIndex].GetIPv4Statistics().BytesSent;
            NetworkInterfaceAssigned?.Invoke(this, null);
        }

        public override void Update()
        {
            try 
            {
                if (IsNetAvailable)
                {
                    var NIS = NetworkInterface.GetAllNetworkInterfaces();

                    #region Update NetworkInterfaces

                    if (UpdateNetworkInterfaces(NIS, true))
                    {
                        return;
                    }

                    #endregion

                    #region  Update Values

                    long newDown = NIS[currentIndex].GetIPv4Statistics().BytesReceived;
                    long newUp = NIS[currentIndex].GetIPv4Statistics().BytesSent;

                    if (d)
                    {
                        var sd = Math.Max(newDown - prevDown, 0);
                        DownBytes = sd;
                        Down = DataConverter.FormatBytes(sd) + "/s";
                    }
                    else { DownBytes = 0; Down = "Turn it On!"; }

                    if (u)
                    {
                        var su = Math.Max(newUp - prevUp, 0);
                        UpBytes = su;
                        Up = DataConverter.FormatBytes(su) + "/s";
                    }
                    else { UpBytes = 0; Up = "Turn it On!"; }

                    if (td)
                    {
                        TotalDownBytes = newDown;
                        TotalDown = DataConverter.FormatBytes(newDown);
                    }
                    else { TotalDown = "Turn it On!"; }

                    if (tu)
                    {
                        TotalUpBytes = newUp;
                        TotalUp = DataConverter.FormatBytes(newUp);
                    }
                    else { TotalUp = "Turn it On!"; }

                    #endregion

                    prevDown = newDown;
                    prevUp = newUp;
                    prevNetworkInterfaces = NIS;
                }
                else
                {
                    Down = "ಠ_ಠ";
                    DownBytes = 0;
                    Up = "ಠ_ಠ";
                    UpBytes = 0;
                }
            }
            catch { }
        }

        private bool UpdateNetworkInterfaces(NetworkInterface[] NIS, bool crossthread = false)
        {
            List<NetInterface> toAdd = new List<NetInterface>();
            List<NetInterface> toRemove = new List<NetInterface>();

            bool removed = false;
            bool added = false;

            foreach (var ni in NIS)
            {
                if (!NetInterfaces.Any(pni => pni.Id == ni.Id))
                {
                    toAdd.Add(new NetInterface() { Id = ni.Id, Name = ni.Name });
                    Debug.WriteLine("Network changed - Added : " + ni.Name + " - " + ni.Id);
                    added = true;
                }
            }

            foreach (var pni in NetInterfaces)
            {
                if (!NIS.Any(ni => ni.Id == pni.Id))
                {
                    toRemove.Add(pni);
                    Debug.WriteLine("Network changed - Removed : " + pni.Name + " - " + pni.Id);
                    removed = true;
                }
            }

            void Update()
            {
                foreach (var newone in toAdd)
                {
                    NetInterfaces.Add(newone);
                }

                foreach (var oldone in toRemove)
                {
                    NetInterfaces.Remove(oldone);
                }
            }

            if(crossthread)
            {
                BuskInterop.GetUIDispatcher().Invoke(() => Update());
            }
            else
            {
                Update();
            }

            if (toRemove.Any(x => x.Id == CurrentNetInterface.Id) && NetInterfaces.Count > 0)
            {
                CurrentNetInterface = NetInterfaces[0];
            }

            toAdd.Clear(); toAdd = null; toRemove.Clear(); toRemove = null;

            if (CurrentNetInterface == null && NetInterfaces.Count > 0)
            {
                CurrentNetInterface = NetInterfaces[0];

                foreach (var netInterface in NetInterfaces)
                {
                    if (netInterface.Name == DefaultCounterName)
                    {
                        CurrentNetInterface = netInterface;
                        break;
                    }
                }
            }

            if (removed | added)
            {
                NetworkInterfacesChanged?.Invoke(this, null);
                return true;
            }
            else
            {
                return false;
            }
        }

        #region Filter Counter Values

        private bool d = true;
        private bool td = true;
        private bool u = true;
        private bool tu = true;

        public void SetCounterFilter(Required required, bool switchto)
        {
            if (required.HasFlag(Required.Down)) { d = switchto; }
            if (required.HasFlag(Required.Up)) { u = switchto; }
            if (required.HasFlag(Required.TotalDown)) { td = switchto; }
            if (required.HasFlag(Required.TotalUp)) { tu = switchto; }
        }

        #endregion
    }
}
