using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
                this.Initialize(win);
            }
            else
            {
                EventHandler handler = null;
                handler = (e, args) =>
                {
                    this.Initialize(BuskInterop.MainWindow);
                    BuskInterop.HookRequested -= handler;
                };
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
            source.AddHook(this.WndProc);
        }

        protected abstract IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled);

    }
}
