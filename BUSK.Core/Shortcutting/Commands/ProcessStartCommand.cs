using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("Process Start Command", "A Command to start a process with specified properties")]
    public class ProcessStartCommand : Command
    {

        #region Properties

        private string arguments = "";

        [XmlAttribute]
        public string Arguments
        {
            get { return arguments; }
            set { SetPropertyValue(ref arguments, value); }
        }

        private string target = "";

        [XmlAttribute]
        public string Target
        {
            get { return target; }
            set { SetPropertyValue(ref target, value); }
        }

        private ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal;

        [XmlAttribute]
        public ProcessWindowStyle WindowStyle
        {
            get { return windowStyle; }
            set { SetPropertyValue(ref windowStyle, value); }
        }

        #endregion

        public override void OnExecute()
        {
            var ps = new ProcessStartInfo() { FileName = Target, Arguments = Arguments, UseShellExecute = true, WindowStyle = WindowStyle };
            Process.Start(ps);
        }
    }
}
