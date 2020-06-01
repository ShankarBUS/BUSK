using System;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    [CommandInfo("Counter State Command", "A Command to control the state of performance counters in BUSK")]
    public class CounterStateCommand : Command
    {

        #region Properties

        private CounterState counterState = CounterState.Pause;

        [XmlAttribute]
        public CounterState CounterState
        {
            get { return counterState; }
            set { SetPropertyValue(ref counterState, value); }
        }

        #endregion 

        public override void OnExecute()
        {
            switch (counterState)
            {
                case CounterState.Pause:
                    BuskInterop.PauseCounters();
                    return;
                case CounterState.Resume:
                    BuskInterop.ResumeCounters();
                    return;
                case CounterState.Toggle:
                    BuskInterop.ToggleCounterState();
                    return;
            }
        }
    }
}
