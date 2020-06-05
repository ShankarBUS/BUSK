using BUSK.Navigation;
using BUSK.Navigation.Pages;
using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BUSK
{
	public partial class SettingsWindow : Window
	{
		private static SettingsWindow instance;

		public static SettingsWindow Instance 
		{
			get 
			{
				if (instance == null) instance = new SettingsWindow();
				return instance;
			}
			set 
			{
				instance.Closing -= instance.WindowClosing;
				instance = value;
			}
		}

		public SettingsWindow()
		{
			InitializeComponent();

			Closing += WindowClosing;

			ContentFrame.Navigated += OnNavigated;

			NavView_Navigate(typeof(SettingsPage), new EntranceNavigationTransitionInfo());

			KeyDown += (s, e) =>
			{
				if (e.Key == Key.Back || (e.Key == Key.Left && Keyboard.Modifiers == ModifierKeys.Alt))
				{
					BackRequested();
				}
			};
		}

		private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (SettingsWindow.Instance == this)
			{
				SettingsWindow.Instance = null;
			}
		}

		private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
		{
			if (args.IsSettingsSelected == true)
			{
				NavView_Navigate(typeof(SettingsPage), args.RecommendedNavigationTransitionInfo);
			}
			else if (args.SelectedItem != null)
			{
				var item = (NavItem)args.SelectedItem;
				var transitionInfo = item.RecommendedNavigationTransitionInfo ?? args.RecommendedNavigationTransitionInfo;
				NavView_Navigate(item.PageType, transitionInfo);
			}
		}

		private void NavView_Navigate(Type pageType, NavigationTransitionInfo info)
		{
			if (pageType != null && ContentFrame.CurrentSourcePageType != pageType)
			{
				ContentFrame.Navigate(pageType, null, info);
			}
		}

		private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
		{
			BackRequested();
		}

		private bool BackRequested()
		{
			if (!ContentFrame.CanGoBack) return false;

			if (NavView.IsPaneOpen &&
				(NavView.DisplayMode == NavigationViewDisplayMode.Minimal
				 || NavView.DisplayMode == NavigationViewDisplayMode.Compact))
			{
				return false;
			}
			ContentFrame.GoBack();
			return true;
		}


		private void OnNavigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{
			NavView.IsBackEnabled = ContentFrame.CanGoBack;
			Type sourcePageType = ContentFrame.SourcePageType;
			if (ContentFrame.SourcePageType == typeof(SettingsPage))
			{
				NavView.SelectedItem = NavView.SettingsItem;
			}
			else if (sourcePageType != null)
			{
				foreach (NavItemBase item in PageCollector.Instance.NavItems)
				{
					if (item is NavItem navItem)
					{
						if (navItem.PageType == sourcePageType) NavView.SelectedItem = navItem;
					}
					else if (item is HierarchicalNavItem hierarchicalNavItem)
					{
						var navItem2 = hierarchicalNavItem.NavItems.FirstOrDefault(i => i.PageType == sourcePageType);
						if (navItem2 != null)
						{
							NavView.SelectedItem = navItem2;
						}
					}
				}
			}
		}
	}
}
