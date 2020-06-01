using BUSK.Core.Shortcutting;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.Navigation.Pages
{
    public partial class ShortcutsPage : Page
    {
        public ShortcutsPage()
        {
            InitializeComponent();
        }

        private void FileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    var fileext = System.IO.Path.GetExtension(file).ToLower();
                    if (fileext == ".lnk")
                    {
                        ShortcutsManager.Instance.LoadShortcutFromlnkFile(file);
                    }
                    else if (fileext == ".xml" && ShortcutsManager.CheckShortcutXmlValidity(file))
                    {
                        ShortcutsManager.Instance.LoadShortcutFromShortcutXml(file);
                    }
                    else
                    {
                        ShortcutsManager.Instance.LoadShortcutFromFile(file);
                    }
                }
            }
        }

        private void FileDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
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
