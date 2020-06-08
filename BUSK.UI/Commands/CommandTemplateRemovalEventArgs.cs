namespace BUSK.UI.Commands
{
    internal class CommandTemplateRemovalEventArgs
    {
        public CommandTemplateRemovalEventArgs(CommandTemplate commandTemplate)
        {
            CommandTemplate = commandTemplate;
        }

        public CommandTemplate CommandTemplate { get; set; }

        public bool TemplateRemoved { get; set; } = false;
    }
}