using System;
using System.Windows;

namespace BUSK.UI
{
    public class ShortcutCommandTemplateHelper
    {
        internal static event CommandTemplateAdditionEventHandler CommandTemplateAdditionRequested;

        public static bool RequestCommandTemplateAddition(CommandTemplate commandTemplate)
        {
            if (commandTemplate == null) return false;
            if (commandTemplate.Template == null) return false;
            if (commandTemplate.CommandType == null) return false;

            var args = new CommandTemplateAdditionEventArgs(commandTemplate);
            CommandTemplateAdditionRequested?.Invoke(args);

            return args.TemplateAdded;
        }
    }

    public class CommandTemplate
    {
        public Type CommandType { get; set; }

        public DataTemplate Template { get; set; }
    }

    internal delegate void CommandTemplateAdditionEventHandler(CommandTemplateAdditionEventArgs eventArgs);

    internal class CommandTemplateAdditionEventArgs
    {
        public CommandTemplateAdditionEventArgs(CommandTemplate commandTemplate)
        {
            CommandTemplate = commandTemplate;
        }

        public CommandTemplate CommandTemplate { get; set; }

        public bool TemplateAdded { get; set; } = false;
    }
}
