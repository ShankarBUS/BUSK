using ModernWpf.Controls;
using ModernWpf.Media.Animation;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace BUSK.Navigation
{
    public class NavItemBase : DependencyObject
    {

    }

    public class NavItem : NavItemBase
    {
        public NavItem(Type pageType)
        {
            PageType = pageType;
        }

        public Type PageType { get; set; }

        #region Properties

        #region Title

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(NavItem),
            new PropertyMetadata(NavigationPageHelper.DefaultPageTitle));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        #endregion

        #region Icon

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(IconElement),
            typeof(NavItem),
            new PropertyMetadata(null));

        public IconElement Icon
        {
            get => (IconElement)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        #endregion

        #region Tooltip

        public static readonly DependencyProperty TooltipProperty = DependencyProperty.Register(
            nameof(Tooltip),
            typeof(string),
            typeof(NavItem),
            new PropertyMetadata(NavigationPageHelper.DefaultPageTooltip));

        public string Tooltip
        {
            get => (string)GetValue(TooltipProperty);
            set => SetValue(TooltipProperty, value);
        }

        #endregion

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register(
            nameof(IsEnabled),
            typeof(bool),
            typeof(NavItem),
            new PropertyMetadata(true));

        public bool IsEnabled
        {
            get => (bool)GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        #endregion

        #region RecommendedNavigationTransitionInfo

        public static readonly DependencyProperty RecommendedNavigationTransitionInfoProperty = DependencyProperty.Register(
            nameof(RecommendedNavigationTransitionInfo),
            typeof(NavigationTransitionInfo),
            typeof(NavItem),
            new PropertyMetadata(null));

        public NavigationTransitionInfo RecommendedNavigationTransitionInfo
        {
            get => (NavigationTransitionInfo)GetValue(RecommendedNavigationTransitionInfoProperty);
            set => SetValue(RecommendedNavigationTransitionInfoProperty, value);
        }

        #endregion

        #endregion
    }

    public class HierarchicalNavItem : NavItem
    {
        public HierarchicalNavItem(Type pageType, bool selectsOnInvoked = true) : base (pageType)
        {
            NavItems = new ObservableCollection<NavItem>();
            SelectsOnInvoked = selectsOnInvoked;
        }

        #region Properties

        public ObservableCollection<NavItem> NavItems { get; private set; }

        #region SelectsOnInvoked

        public static readonly DependencyProperty SelectsOnInvokedProperty = DependencyProperty.Register(
            nameof(SelectsOnInvoked),
            typeof(bool),
            typeof(NavItem),
            new PropertyMetadata(true));

        public bool SelectsOnInvoked
        {
            get => (bool)GetValue(SelectsOnInvokedProperty);
            set => SetValue(SelectsOnInvokedProperty, value);
        }

        #endregion

        #endregion
    }

    public class Header : NavItemBase
    {
        public string Name { get; set; }
    }

    public class Separator : NavItemBase
    {

    }
}
