namespace BUSK.UI.Commands
{
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