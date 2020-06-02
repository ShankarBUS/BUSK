using System;
using System.Management;

namespace BUSK.Utilities
{
    internal static class HardwareInfo
    {
        public static string GetAccountName()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_UserAccount");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Name").ToString();
                }
                catch
                {
                }
            }

            return "User Account Name: Unknown";
        }

        public static string GetBIOScaption()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Caption").ToString();
                }
                catch
                {
                }
            }

            return "BIOS Caption: Unknown";
        }

        public static string GetBIOSmaker()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch
                {
                }
            }

            return "BIOS Maker: Unknown";
        }

        public static string GetBIOSserNo()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("SerialNumber").ToString();
                }
                catch
                {
                }
            }

            return "BIOS Serial Number: Unknown";
        }

        public static string GetBoardMaker()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Manufacturer").ToString();
                }
                catch
                {
                }
            }

            return "Board Maker: Unknown";
        }

        public static string GetBoardProductId()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Product").ToString();
                }
                catch
                {
                }
            }

            return "Product: Unknown";
        }

        public static string GetCdRomDrive()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_CDROMDrive");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("Drive").ToString();
                }
                catch
                {
                }
            }

            return "CD ROM Drive Letter: Unknown";
        }

        public static string GetComputerName()
        {
            var mc = new ManagementClass("Win32_ComputerSystem");
            var moc = mc.GetInstances();
            string info = string.Empty;
            foreach (ManagementObject mo in moc)
                info = (string)mo["Name"];
            return info;
        }

        public static int GetCPUCurrentClockSpeed()
        {
            int cpuClockSpeed = 0;
            var mgmt = new ManagementClass("Win32_Processor");
            var objCol = mgmt.GetInstances();
            foreach (ManagementObject obj in objCol)
            {
                if (cpuClockSpeed == 0)
                {
                    cpuClockSpeed = (int)obj.Properties["CurrentClockSpeed"].Value;
                }
            }

            return cpuClockSpeed;
        }

        public static string GetCPUManufacturer()
        {
            string cpuMan = string.Empty;
            var mgmt = new ManagementClass("Win32_Processor");
            var objCol = mgmt.GetInstances();
            foreach (ManagementObject obj in objCol)
            {
                if ((cpuMan ?? "") == (string.Empty ?? ""))
                {
                    cpuMan = obj.Properties["Manufacturer"].Value.ToString();
                }
            }

            return cpuMan;
        }

        public static double? GetCpuSpeedInGHz()
        {
            double? GHz = default;
            using (var mc = new ManagementClass("Win32_Processor"))
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    GHz = (uint)mo.Properties["CurrentClockSpeed"].Value / 1000.0;
                    break;
                }
            }

            return GHz;
        }

        public static string GetCurrentLanguage()
        {
            var searcher = new ManagementObjectSearcher(@"root\CIMV2", "SELECT * FROM Win32_BIOS");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return wmi.GetPropertyValue("CurrentLanguage").ToString();
                }
                catch
                {
                }
            }

            return "BIOS Maker: Unknown";
        }

        public static string GetDefaultIPGateway()
        {
            var mgmt = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var objCol = mgmt.GetInstances();
            string gateway = string.Empty;
            foreach (ManagementObject obj in objCol)
            {
                if ((gateway ?? "") == (string.Empty ?? ""))
                {
                    if ((bool)obj["IPEnabled"] == true)
                    {
                        gateway = obj["DefaultIPGateway"].ToString();
                    }
                }

                obj.Dispose();
            }

            gateway = gateway.Replace(":", "");
            return gateway;
        }

        public static string GetHDDSerialNo()
        {
            var mangnmt = new ManagementClass("Win32_LogicalDisk");
            var mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
                result += Convert.ToString(strt["VolumeSerialNumber"]);
            return result;
        }

        public static string GetMACAddress()
        {
            var mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            var moc = mc.GetInstances();
            string MACAddress = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                if ((MACAddress ?? "") == (string.Empty ?? ""))
                {
                    if ((bool)mo["IPEnabled"] == true)
                        MACAddress = mo["MacAddress"].ToString();
                }

                mo.Dispose();
            }

            MACAddress = MACAddress.Replace(":", "");
            return MACAddress;
        }

        public static string GetNoRamSlots()
        {
            int MemSlots = 0;
            var oMs = new ManagementScope();
            var oQuery2 = new ObjectQuery("SELECT MemoryDevices FROM Win32_PhysicalMemoryArray");
            var oSearcher2 = new ManagementObjectSearcher(oMs, oQuery2);
            var oCollection2 = oSearcher2.Get();
            foreach (ManagementObject obj in oCollection2)
                MemSlots = Convert.ToInt32(obj["MemoryDevices"]);
            return MemSlots.ToString();
        }

        public static string GetOSInformation()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject wmi in searcher.Get())
            {
                try
                {
                    return ((string)wmi["Caption"]).Trim() + ", " + (string)wmi["Version"] + ", " + (string)wmi["OSArchitecture"];
                }
                catch
                {
                }
            }

            return "BIOS Maker: Unknown";
        }

        public static long GetPhysicalMemoryBytes()
        {
            var oMs = new ManagementScope();
            var oQuery = new ObjectQuery("SELECT Capacity FROM Win32_PhysicalMemory");
            var oSearcher = new ManagementObjectSearcher(oMs, oQuery);
            var oCollection = oSearcher.Get();
            long MemSize = 0;
            foreach (ManagementObject obj in oCollection)
            {
                MemSize += Convert.ToInt64(obj["Capacity"]);
            }

            return MemSize;
        }

        public static string GetPhysicalMemory()
        {
            long MemSize = GetPhysicalMemoryBytes();

            MemSize = (long)(MemSize / (double)1024 / 1024);
            return MemSize.ToString() + "MB";
        }

        public static string GetProcessorId()
        {
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            string Id = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                Id = mo.Properties["processorID"].Value.ToString();
                break;
            }

            return Id;
        }

        public static string GetProcessorInformation()
        {
            var mc = new ManagementClass("win32_processor");
            var moc = mc.GetInstances();
            string info = string.Empty;
            foreach (ManagementObject mo in moc)
            {
                string name = (string)mo["Name"];
                name = name.Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®").Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©").Replace("    ", " ").Replace("  ", " ");
                info = name + ", " + (string)mo["Caption"] + ", " + (string)mo["SocketDesignation"];
            }

            return info;
        }
    }
}