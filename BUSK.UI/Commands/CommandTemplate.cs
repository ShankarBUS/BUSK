using System;
using System.Windows;

namespace BUSK.UI.Commands
{
    public sealed class CommandTemplate
    {
        public Type CommandType { get; set; }

        public DataTemplate Template { get; set; }
    }
}