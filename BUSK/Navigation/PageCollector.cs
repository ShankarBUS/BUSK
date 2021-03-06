﻿using BUSK.Controls.Shortcutting;
using BUSK.Core.Diagnostics;
using BUSK.Navigation.Pages;
using BUSK.Utilities;
using BUSK.ViewModels;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace BUSK.Navigation
{
    public class PageCollector
    {
        #region Properties

        public static PageCollector Instance { get; internal set; }

        public ObservableCollection<NavItemBase> NavItems { get; private set; } = new ObservableCollection<NavItemBase>();

        #endregion

        public void Initialize()
        {
            ViewModelsHandler.InitializeViewModels();

            AddDefaultPages();

            NavigationPageHelper.NavigationPageAdditionRequested += (e) =>
            {
                e.PageAdded = AddPage(e.PageType, e.Title, e.Icon, e.Tooltip);
            };

            NavigationPageHelper.NavigationPageRemovalRequested += (e) =>
            {
                e.PageRemoved = RemovePage(e.PageType);
            };

            CommandTemplateHandler.Instance = new CommandTemplateHandler();
        }

        public void AddDefaultPages()
        {
            NavItems.Add(new Header() { Name = "Status" });

            AddCPUPage();

            AddRAMPage();

            AddDiskPage();

            AddNetPage();

            NavItems.Add(new Separator());

            if (!Exists(typeof(ShortcutsPage)))
            {
                var navitem = new HierarchicalNavItem(typeof(ShortcutsPage))
                { 
                    Title = "Shortcuts",
                    Icon = new FontIcon() { Glyph = BUSKGlyphs.Shortcut },
                    Tooltip = "Shortcuts"
                };
                var subnavitem = new NavItem(typeof(ShortcutEditorPage))
                { 
                    Title = "Shortcut Editor",
                    Icon = new FontIcon() { Glyph = BUSKGlyphs.Editor },
                    Tooltip = "Shortcut Editor",
                    RecommendedNavigationTransitionInfo = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight }
                };
                var binding = new Binding(nameof(ShortcutsViewModel.IsEditorEnabled)) { Source = ShortcutsViewModel.Instance };
                BindingOperations.SetBinding(subnavitem, NavItem.IsEnabledProperty, binding);

                navitem.NavItems.Add(subnavitem);
                NavItems.Add(navitem);
            }

            AddPage(typeof(ExtensionsPage), "Extensions", new FontIcon() { Glyph = BUSKGlyphs.Puzzle }, "Extensions");
        }

        #region Default Pages

        public void AddCPUPage()
        {
            if (!Exists(typeof(CPUStatusPage)))
            {
                var cpuitem = new NavItem(typeof(CPUStatusPage)) { Icon = new FontIcon() { Glyph = BUSKGlyphs.Processor } };
                var binding = new Binding(nameof(CPUInformation.CPUUsageText)) { Source = CPUInformation.Instance, StringFormat = "CPU : {0}" };
                BindingOperations.SetBinding(cpuitem, NavItem.TitleProperty, binding);
                BindingOperations.SetBinding(cpuitem, NavItem.TooltipProperty, binding);
                NavItems.Insert(1, cpuitem);
            }
        }

        public void AddRAMPage()
        {
            if (!Exists(typeof(RAMStatusPage)))
            {
                var ramitem = new NavItem(typeof(RAMStatusPage)) { Icon = new FontIcon() { Glyph = BUSKGlyphs.Memory } };
                var binding = new Binding(nameof(RAMInformation.RAMUsageText)) { Source = RAMInformation.Instance, StringFormat = "RAM : {0}" };
                BindingOperations.SetBinding(ramitem, NavItem.TitleProperty, binding);
                BindingOperations.SetBinding(ramitem, NavItem.TooltipProperty, binding);
                NavItems.Insert(2, ramitem);
            }
        }

        public void AddDiskPage()
        {
            if (!Exists(typeof(DiskStatusPage)))
            {
                var diskitem = new NavItem(typeof(DiskStatusPage)) { Icon = new FontIcon() { Glyph = BUSKGlyphs.HardDrive } };
                var binding = new Binding(nameof(DiskInformation.DiskUsageText)) { Source = DiskInformation.Instance, StringFormat = "Disk : {0}" };
                BindingOperations.SetBinding(diskitem, NavItem.TitleProperty, binding);
                BindingOperations.SetBinding(diskitem, NavItem.TooltipProperty, binding);
                NavItems.Insert(3, diskitem);
            }
        }

        public void AddNetPage()
        {
            if (!Exists(typeof(NetStatusPage)))
            {
                var netitem = new NavItem(typeof(NetStatusPage)) { Icon = new FontIcon() { Glyph = BUSKGlyphs.Network } };
                var binding = new Binding(nameof(NetInformation.Down)) { Source = NetInformation.Instance, StringFormat = "Net : {0}" };
                BindingOperations.SetBinding(netitem, NavItem.TitleProperty, binding);
                BindingOperations.SetBinding(netitem, NavItem.TooltipProperty, binding);
                NavItems.Insert(4, netitem);
            }
        }

        #endregion

        public bool AddPage(Type pageType, string title, IconElement icon, string tooltip)
        {
            if (!Exists(pageType))
            {
                NavItems.Add(new NavItem(pageType) { Title = title, Icon = icon, Tooltip = tooltip });
                return true;
            }

            return false;
        }

        public bool RemovePage(Type pageType)
        {
            if (Exists(pageType))
            {
                var navItem = NavItems.FirstOrDefault(item => item is NavItem navItem && navItem?.PageType == pageType);

                if (navItem == null)
                {
                    return false;
                }

                navItem.ClearValue(NavItem.TitleProperty);

                navItem.ClearValue(NavItem.TooltipProperty);

                NavItems.Remove(navItem);

                var contentFrame = SettingsWindow.Instance?.ContentFrame;
                if (contentFrame != null)
                {
                    if (contentFrame.SourcePageType == pageType)
                    {
                        contentFrame.Navigate(typeof(SettingsPage), null, new EntranceNavigationTransitionInfo());
                    }

                    contentFrame.RemoveBackEntry();
                    RemoveFrameHistory(contentFrame);
                }

                return true;
            }

            return false;
        }

        private void RemoveFrameHistory(Frame contentFrame, bool navigateToSettings = true)
        {
            if (!contentFrame.CanGoBack)
            {
                return;
            }

            var entry = contentFrame.RemoveBackEntry();
            while (entry != null)
            {
                entry = contentFrame.RemoveBackEntry();
            }
        }

        public bool Exists(Type pageType)
        {
            return NavItems.Any(item => item is NavItem navItem && navItem?.PageType == pageType);
        }
    }
}
