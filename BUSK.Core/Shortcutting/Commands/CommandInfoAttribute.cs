using System;

namespace BUSK.Core.Shortcutting.Commands
{
    /// <summary>
    /// Defines the information of a <see cref="Command"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class CommandInfoAttribute : Attribute
    {
        public CommandInfoAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; } = "";

        public string Description { get; } = "";
    }
}
