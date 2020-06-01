using System.Windows;
using System.Windows.Controls;

namespace BUSK.Controls.Shortcutting
{
    public class CommandTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            foreach (var commandTemplate in CommandTemplateHandler.Instance.CommandTemplates)
            {
                if (commandTemplate.CommandType == item.GetType())
                {
                    return commandTemplate.Template;
                }
            }
            return DefaultItemTemplate;
        }
    }
}
