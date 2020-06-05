using System.Windows;
using System.Windows.Controls;

namespace BUSK.Controls.Shortcutting
{
    public partial class CommandTemplates : ResourceDictionary
    {
        public CommandTemplates()
        {
            InitializeComponent();
        }

        private void PSTextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
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

        private void FileDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                var tb = sender as TextBox;
                tb.Text = files[0];
            }
        }
    }
}
