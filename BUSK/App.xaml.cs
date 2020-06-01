using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Windows;
using System.IO;
using System.Diagnostics;
using BUSK.Utilities;
using BUSK.Core;

namespace BUSK
{
    public partial class App : Application
    {
        public static ISplashScreen SplashScreenInstance;

        private ManualResetEvent ResetSplashCreated;

        private Thread SplashThread;

        private MainWindow mainWindow;

        private bool splashShow = false;

        private static Stopwatch stopwatch;

        public App()
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();
            Debug.WriteLine("Start : " + DateTime.Now.ToLongTimeString());
            Debug.WriteLine("Loaded");
            Domain = AppDomain.CurrentDomain;
            Domain.AssemblyResolve += Domain_AssemblyResolve;
        }

        private void ShowSplash(System.Windows.Threading.Dispatcher dispatcher)
        {
            SplashScreen splashScreen = new SplashScreen() { AppDispatcher = dispatcher };
            SplashScreenInstance = splashScreen;
            splashScreen.Show();
            ResetSplashCreated.Set();
            System.Windows.Threading.Dispatcher.Run();
        }

        public static void AddMessage(string message)
        {
            SplashScreenInstance?.AddMessage(message);
        }

        public static void LoadComplete()
        {
            SplashScreenInstance?.LoadComplete();
            Debug.WriteLine("End : " + DateTime.Now.ToLongTimeString());
            stopwatch.Stop();
            Debug.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            foreach(string s in e.Args)
            {
                Debug.WriteLine(s);
                if (s.ToLower().Contains("silent")) { splashShow = false; }
                else if (s.ToLower().Contains("showsplash")) { splashShow = true; }
            }

            if (splashShow)
            {
                ResetSplashCreated = new ManualResetEvent(false);

                SplashThread = new Thread(() => ShowSplash(Dispatcher));
                SplashThread.SetApartmentState(ApartmentState.STA);
                SplashThread.IsBackground = true;
                SplashThread.Start();
                ResetSplashCreated.WaitOne();
            }

            BuskInterop.IntializePerfCounters();
            mainWindow = new MainWindow();
            MainWindow = mainWindow;
            mainWindow.Show();
            base.OnStartup(e);
        }

        private AppDomain Domain;

        private Assembly Domain_AssemblyResolve(object sender, ResolveEventArgs e)
        {
            if (e.Name.Contains(".resources"))
            {
                return null;
            }

            Debug.WriteLine(e.RequestingAssembly.Location + " - " + e.RequestingAssembly.FullName + " Resquests - " + e.Name);

            if (!Directory.Exists(@".\ExtRefFallback"))
            {
                Directory.CreateDirectory(@".\ExtRefFallback");
            }

            // returns pre-existing ones
            Assembly assembly = Domain.GetAssemblies().FirstOrDefault((a) => a.FullName == e.Name);
            if (assembly != null) { return assembly; }

            var dllname = e.RequestingAssembly.FullName.Split(',')[0] + ".dll".ToLower();
            var fileName = e.Name.Split(',')[0] + ".dll".ToLower();

            // Method 1
            foreach (var v in ExtensionsFileHandler.paths)
            {
                if (v.Key == dllname )
                {
                    try
                    {
                        var location = Path.Combine(v.Value, fileName);
                        return Assembly.LoadFrom(location);
                    } catch { }
                }
            }

            var asmFile = Path.Combine(@".\", "ExtRefFallback", fileName);

            try { return Assembly.LoadFrom(asmFile); } catch { }

            return null;
        }
    }
}
