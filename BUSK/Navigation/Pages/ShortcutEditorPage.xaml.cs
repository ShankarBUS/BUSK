using BUSK.Core.Shortcutting.Commands;
using BUSK.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.Navigation.Pages
{
    public partial class ShortcutEditorPage : Page
    {
        public ShortcutEditorPage()
        {
            InitializeComponent();
        }

        private void FileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ShortcutEditorViewModel.Instance.CurrentEdittingShortcut != null)
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    ShortcutEditorViewModel.Instance.CurrentEdittingShortcut.Commands.Add(new ProcessStartCommand() 
                    {
                        Target = file
                    });
                }
            }
        }

        private void FileDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) && ShortcutEditorViewModel.Instance.CurrentEdittingShortcut != null)
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }
    }
}
