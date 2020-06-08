using System.Windows;

namespace BUSK.Controls
{
    public partial class ExtConfigMaker : Window
    {
        public ExtConfigMaker()
        {
            InitializeComponent();
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    var fi = new System.IO.FileInfo(file);
                    if (fi.Extension.ToLower() == ".dll")
                    {
                        Utilities.ExtensionsFileHandler.paths.Add(fi.Name, fi.DirectoryName);
                        var exbase = Utilities.ExtensionsFileHandler.LoadExtensionBaseFromFile(fi.FullName);
                        if (exbase != null)
                        {
                            var configpath = Utilities.ExtensionsFileHandler.GetOtherFileWithExtension(fi.FullName, Utilities.ExtensionsFileHandler.cfgfileext);
                            Utilities.ExtensionsFileHandler.SaveExtConfig(exbase.ExtensionInfo, configpath);
                        }
                    }
                }
            }
        }

        private void Grid_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            } else { e.Effects = DragDropEffects.None; }
        }
    }
}
