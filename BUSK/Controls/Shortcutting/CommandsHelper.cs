using BUSK.Core.Shortcutting;
using BUSK.Core.Shortcutting.Commands;
using System;
using System.Collections.Generic;

namespace BUSK.Controls.Shortcutting
{
    public class CommandsHelper
    {
        public static CommandItem[] GetCommandItems()
        {
            List<CommandItem> items = new List<CommandItem>();
            var cmdtypes = ShortcutsManager.GetInheritedCommandTypes();
            foreach (var cmdtype in cmdtypes)
            {
                items.Add(GetCommandItemFromType(cmdtype));
            }

            return items.ToArray();
        }

        public static CommandItem GetCommandItemFromType(Type cmdtype)
        {
            CommandInfoAttribute cmdInfoAttribute =
                (CommandInfoAttribute)Attribute.GetCustomAttribute(cmdtype, typeof(CommandInfoAttribute));

            if (cmdInfoAttribute != null)
            {
                return new CommandItem(cmdInfoAttribute.Name, cmdInfoAttribute.Description, cmdtype);
            }
            else
            {
                return new CommandItem(cmdtype.Name, "", cmdtype);
            }
        }
    }

    public class CommandItem
    {
        public CommandItem(string name, string description, Type commandType)
        {
            Name = name;
            Description = description;
            CommandType = commandType;
        }

        public string Name { get; } = "";

        public string Description { get; } = "";

        public Type CommandType { get; }
    }
}
