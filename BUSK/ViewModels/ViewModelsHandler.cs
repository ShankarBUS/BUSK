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
        }
    }
}
