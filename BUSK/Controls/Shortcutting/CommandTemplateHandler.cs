﻿using BUSK.Core.Shortcutting.Commands;
using BUSK.UI.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BUSK.Controls.Shortcutting
{
    public class CommandTemplateHandler
    {
        public static CommandTemplateHandler Instance { get; set; }

        public ObservableCollection<CommandTemplate> CommandTemplates { get; } = new ObservableCollection<CommandTemplate>();

        public CommandTemplateHandler()
        {
            AddDefaultTemplates();

            CommandTemplateHelper.CommandTemplateAdditionRequested += (e) =>
            {
                e.TemplateAdded = AddTemplate(e.CommandTemplate);
            };

            CommandTemplateHelper.CommandTemplateRemovalRequested += (e) =>
            {
                e.TemplateRemoved = RemoveTemplate(e.CommandTemplate);
            };
        }

        private void AddDefaultTemplates()
        {
            var cmdtemplates = new CommandTemplates();
            foreach (var i in cmdtemplates.Values)
            {
                if (i is DataTemplate dataTemplate)
                {
                    AddTemplate(new CommandTemplate() { CommandType = (Type)dataTemplate.DataType, Template = dataTemplate });
                }
            }
        }

        private bool AddTemplate(CommandTemplate commandTemplate)
        {
            if (!Exists(commandTemplate.CommandType) && commandTemplate.CommandType.BaseType == typeof(Command))
            {
                CommandTemplates.Add(commandTemplate);
                return true;
            }

            return false;
        }

        private bool RemoveTemplate(CommandTemplate commandTemplate)
        {
            if (Exists(commandTemplate) && commandTemplate.CommandType.BaseType == typeof(Command))
            {
                CommandTemplates.Remove(commandTemplate);
                return true;
            }

            return false;
        }

        public bool Exists(Type commandType)
        {
            return CommandTemplates.Any(template => template.CommandType == commandType);
        }

        public bool Exists(CommandTemplate commandTemplate)
        {
            return CommandTemplates.Contains(commandTemplate);
        }
    }
}
