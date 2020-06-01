using BUSK.Core.Utilities;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BUSK.Controls
{
    public partial class ImagePickerDialog : Window
    {

        #region Properties

        public ObservableCollection<IconType> IconTypes { get; } = new ObservableCollection<IconType>();

        private static readonly DependencyPropertyKey ImageSourcePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(ImageSource),
            typeof(ImageSource),
            typeof(ImagePickerDialog),
            new PropertyMetadata(null));

        public static readonly DependencyProperty ImageSourceProperty = ImageSourcePropertyKey.DependencyProperty;

        public ImageSource ImageSource 
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            private set => SetValue(ImageSourcePropertyKey, value);
        }

        #endregion

        public ImagePickerDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Multiselect = false, Title = "Select Image" };
            openFileDialog.Filter = "All Image files (*.bmp, *.emf, *.exif, *.gif, *.jpeg, *.jpg, *.png, *.tif, *.tiff, *.wmf)|*.bmp;*.emf;*.exif;*.gif;*.jpeg;*.jpg;*.png;*.tif;*.tiff;*.wmf|All files|*.*";
            var result = openFileDialog.ShowDialog();
            if (result.HasValue & result.Value)
            {
                using FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read)
                {
                    Position = 0
                };
                var img = new BitmapImage();
                img.BeginInit(); img.StreamSource = fileStream; img.CacheOption = BitmapCacheOption.OnLoad; img.EndInit();
                ImageSource = img;
                IconTypes.Clear();
                ImageViewer.Source = ImageSource;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var ipd = new IconPickerDialog();
            var result = ipd.ShowDialog();
            if (result.HasValue & result.Value)
            {
                var fileName = ipd.FileName;
                var index = ipd.IconIndex;
                Icon icon;
                Icon[] splitIcons;
                try
                {
                    if (Path.GetExtension(fileName).ToLower() == ".ico")
                    {
                        icon = new Icon(fileName);
                    }
                    else
                    {
                        var extractor = new IconExtractor(fileName);
                        icon = extractor.GetIcon(index);
                    }
                    splitIcons = IconUtil.Split(icon);
                } catch { return; }

                icon.Dispose();
                IconTypes.Clear();

                foreach(Icon ico in splitIcons)
                {
                    IconTypes.Add(new IconType() { Resolution = $"{ico.Width} x {ico.Height}", ImageSource = ImageHelper.GetBitmapImage(IconUtil.ToBitmap(ico)) });
                }
            }
        }

        private void ButOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            ImageSource = null;
            Close();
        }

        private void IcoSourceLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IcoSourceLV.SelectedItem != null && IcoSourceLV.SelectedItem is IconType iconType)
            {
                ImageSource = iconType.ImageSource;
                ImageViewer.Source = ImageSource;
            }
        }
    }

    public class IconType
    {
        public ImageSource ImageSource { get; set; }

        public string Resolution { get; set; }
    }
}
