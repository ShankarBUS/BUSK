using System;
using System.Windows.Input;
using System.Xml.Serialization;

namespace BUSK.Core.Shortcutting.Commands
{
    [Serializable]
    public abstract class Command : BindableBase
    {
        private System.Timers.Timer Timer { get; } = new System.Timers.Timer() { Interval = 1.0, Enabled = false };

        public Command()
        {
            Timer.Elapsed += Timer_Elapsed;
        }

        #region Properties

        private double delay = 1.0;

        /// <summary>
        /// Delay in milli-seconds (must be greater than 0)
        /// </summary>
        [XmlAttribute]
        public double Delay
        {
            get { return delay; }
            set { if (delay >= 0.0 && SetPropertyValue(ref delay,value)) { Timer.Stop(); Timer.Interval = delay; } }
        }

        [XmlIgnore]
        public ICommand DeleteCommand { get; internal set; }

        private Shortcut owner;

        [XmlIgnore]
        public Shortcut Owner
        {
            get { return owner; }
            internal set 
            { 
                if (owner != null && value != null)
                {
                    throw new NotSupportedException("Each Command must have only one owner Shortcut.");
                }

                SetPropertyValue(ref owner, value);
            }
        }

        #endregion

        public Command Clone()
        {
            return (Command)MemberwiseClone();
        }

        public void Execute()
        {
            Timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Timer.Stop();
            try
            {
                BuskInterop.GetUIDispatcher().Invoke(() => OnExecute());
            }
            catch { Timer.Stop(); }
        }

        public virtual void OnAdded() { }

        public virtual void OnRemoved() { }

        public abstract void OnExecute();
    }
}
