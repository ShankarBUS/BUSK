using System;

namespace BUSK.Core.Utilities
{
    public sealed class DisplayHandler : HookHandler
    {
        private const int SPI_SETWORKAREA = 0x2f;

        private const int WM_DISPLAYCHANGE = 0x7e;

        private const int WM_SETTINGCHANGE = 0x1a;

        public static event EventHandler DisplayUpdate;

        public static void Initialize()
        {
            Instance ??= new DisplayHandler();
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == WM_SETTINGCHANGE) && (wParam == ((IntPtr)SPI_SETWORKAREA)))
            {
                DisplayUpdate?.Invoke(null, null);
                handled = true;
            }
            return IntPtr.Zero;
        }
    }
}
