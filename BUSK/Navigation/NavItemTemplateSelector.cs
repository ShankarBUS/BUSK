using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BUSK.Navigation
{
    [ContentProperty("ItemTemplate")]
    class NavItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ItemTemplate { get; set; }

        public DataTemplate HierarchicItemTemplate { get; set; }

        public DataTemplate HeaderTemplate { get; set; }

        public DataTemplate SeparatorTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            return item is Separator ? SeparatorTemplate : item is Header ? HeaderTemplate : item is HierarchicalNavItem ? HierarchicItemTemplate : ItemTemplate;
        }
    }
}
