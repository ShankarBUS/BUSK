namespace BUSK.UI.Commands
{
    public class CommandTemplateHelper
    {
        internal static event CommandTemplateAdditionEventHandler CommandTemplateAdditionRequested;

        internal static event CommandTemplateRemovalEventHandler CommandTemplateRemovalRequested;

        public static bool RequestCommandTemplateAddition(CommandTemplate commandTemplate)
        {
            if (commandTemplate == null) return false;
            if (commandTemplate.Template == null) return false;
            if (commandTemplate.CommandType == null) return false;

            var args = new CommandTemplateAdditionEventArgs(commandTemplate);
            CommandTemplateAdditionRequested?.Invoke(args);

            return args.TemplateAdded;
        }

        public static bool RequestCommandTemplateRemoval(CommandTemplate commandTemplate)
        {
            if (commandTemplate == null) return false;
            if (commandTemplate.Template == null) return false;
            if (commandTemplate.CommandType == null) return false;

            var args = new CommandTemplateRemovalEventArgs(commandTemplate);
            CommandTemplateRemovalRequested?.Invoke(args);

            return args.TemplateRemoved;
        }
    }
}