using BUSK.Core.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.Controls
{
    public partial class DiskItemsControl : UserControl
    {
        public DiskItemsControl()
        {
            InitializeComponent();

            ButUpdate.Click += (s, e) => Update();

            void UpdateHandler(object s, EventArgs e)
            {
                Update();
            }

            Loaded += (s, e) => { DiskHandler.DiskUpdate += UpdateHandler; };
            Unloaded += (s, e) => { DiskHandler.DiskUpdate -= UpdateHandler;  };

            if (DesignerProperties.GetIsInDesignMode(this)) return;

            Update();
        }

        private void Update()
        {
            diskItems.Clear();
            drives.Clear();
            FixedDisks.Children.Clear();
            RemovableDisks.Children.Clear();
            CDs.Children.Clear();

            F.Visibility = Visibility.Collapsed;
            R.Visibility = Visibility.Collapsed;
            C.Visibility = Visibility.Collapsed;

            LoadDrives();
        }

        private List<DiskItem> diskItems = new List<DiskItem>();

        private List<DriveInfo> drives = new List<DriveInfo>();

        private void Add(DriveInfo driveInfo)
        {
            var diskItem = new DiskItem() { DriveInfo = driveInfo };
            switch (driveInfo.DriveType)
            {
                case DriveType.Fixed:
                    FixedDisks.Children.Add(diskItem);
                    break;
                case DriveType.Removable:
                    RemovableDisks.Children.Add(diskItem);
                    break;
                case DriveType.CDRom:
                    CDs.Children.Add(diskItem);
                    break;
                default:
                    return;
            }
            diskItems.Add(diskItem);
        }

        private async void LoadDrives()
        {
            await Task.Run(() => { drives.AddRange(DriveInfo.GetDrives()); });

            foreach (var driveInfo in drives)
            {
                Add(driveInfo);
            }

            if (FixedDisks.Children.Count > 0) F.Visibility = Visibility.Visible;
            if (RemovableDisks.Children.Count > 0) R.Visibility = Visibility.Visible;
            if (CDs.Children.Count > 0) C.Visibility = Visibility.Visible;

            FHead.Text = $"Fixed ({FixedDisks.Children.Count})";
            RHead.Text = $"Removable ({RemovableDisks.Children.Count})";
            CHead.Text = $"CD ROMs ({CDs.Children.Count})";

            foreach (var diskItem in diskItems)
            {
                diskItem.LoadDriveInfos();
            }
        }
    }
}
