using System;

namespace BUSK.Core.Utilities
{
    public sealed class DiskHandler : HookHandler
    {
        private const int WM_DEVICECHANGE = 0x219;

        public static void Initialize()
        {
            Instance ??= new DiskHandler();
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_DEVICECHANGE)
            {
                if (!((wParam.ToInt32() == 0x8000) | (wParam.ToInt32() == 0x8004)))
                {
                    return IntPtr.Zero;
                }

                DiskUpdate?.Invoke(null, null);
                handled = true;
            }
            return IntPtr.Zero;
        }


        public static event EventHandler DiskUpdate;
    }
}
