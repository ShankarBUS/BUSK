using System;

namespace BUSK.Core.Diagnostics
{
    public class CountersHandler
    {
        public const double DefaultInterval = 1000.0;

        public static event EventHandler CommonCounterIntervalChanged;

        public static event EventHandler IsRunningChanged;

        #region Properties

        private static double commonInterval = DefaultInterval;

        public static double CommonInterval
        {
            get { return commonInterval; }
            set { if(IsIntervalValid(value) && commonInterval != value) { commonInterval = value; OnCommonCounterIntervalChanged(); } }
        }

        private static bool isRunning = false;

        public static bool IsRunning
        {
            get { return isRunning; }
            private set { if (isRunning != value) { isRunning = value; OnIsRunningChanged(); } }
        }

        #endregion

        internal static bool IsIntervalValid(double interval)
        {
            return !double.IsNaN(interval) && double.IsFinite(interval) && (interval > 100.0);
        }

        private static void OnCommonCounterIntervalChanged()
        {
            CommonCounterIntervalChanged?.Invoke(null, null);
        }

        private static void OnIsRunningChanged()
        {
            IsRunningChanged?.Invoke(null, null);
        }

        internal static void RunAll()
        {
            IsRunning = true;
        }

        internal static void SuspendAll()
        {
            IsRunning = false;
        }
    }
}
