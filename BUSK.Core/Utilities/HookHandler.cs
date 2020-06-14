using System;
using System.Windows;
using System.Windows.Interop;

namespace BUSK.Core.Utilities
{
    public abstract class HookHandler
    {
        protected static HookHandler Instance { get; set; }

        static HookHandler() 
        {
            
        }

        public HookHandler()
        {
            var win = BuskInterop.MainWindow;
            if (win != null)
            {
                Initialize(win);
            }
            else
            {
                void handler(object e, EventArgs args)
                {
                    Initialize(BuskInterop.MainWindow);
                    BuskInterop.HookRequested -= handler;
                }

                BuskInterop.HookRequested += handler;
            }
        }

        private void Initialize(Window win)
        {
            if (win.IsLoaded)
            {
                InitializeCore(win);
            }
            else
            {
                win.Loaded += (_, __) =>
                {
                    InitializeCore(win);
                };
            }
        }

        protected virtual void InitializeCore(Window win)
        {
            var source = HwndSource.FromHwnd(new WindowInteropHelper(win).Handle);
            source.AddHook(WndProc);
        }

        protected abstract IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);

    }
}
