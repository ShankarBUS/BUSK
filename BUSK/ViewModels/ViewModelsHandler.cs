using BUSK.Core;
using BUSK.Core.Diagnostics;
using BUSK.Navigation;
using BUSK.Navigation.Pages;

namespace BUSK.ViewModels
{
    public class ViewModelsHandler
    {
        public static void InitializeViewModels()
        {
            CPUPageViewModel.Instance = new CPUPageViewModel();
            DiskPageViewModel.Instance = new DiskPageViewModel();
            RAMPageViewModel.Instance = new RAMPageViewModel();
            NetPageViewModel.Instance = new NetPageViewModel();
            ShortcutsViewModel.Instance = new ShortcutsViewModel();
            ShortcutEditorViewModel.Instance = new ShortcutEditorViewModel();

            SettingsManager.Instance.ModuleEnabledChanged += Instance_ModuleEnabledChanged;
        }

        private static void Instance_ModuleEnabledChanged(object sender, ModuleEnabledChangedEventArgs e)
        {
            if (e.CounterType == CounterType.CPU)
            {
                if (e.IsEnabled)
                {
                    CPUPageViewModel.Instance.Initialize();
                    PageCollector.Instance.AddCPUPage();
                }
                else
                {
                    PageCollector.Instance.RemovePage(typeof(CPUStatusPage));
                    CPUPageViewModel.Instance.Suspend();
                }
            }
        }
    }
}
