using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("Custom Method Command", "A Command to execute a custom method (must only be used from code)")]
    public class CustomMethodCommand : Command
    {

        #region Properties

        public delegate void CustomMethodCallback();

        [XmlIgnore]
        public CustomMethodCallback CustomMethod { get; set; } = new CustomMethodCallback(() => Debug.WriteLine("CustomMethodSCommand : CustomMethod Test"));

        #endregion

        public override void OnExecute()
        {
            CustomMethod?.Invoke();
        }
    }
}
