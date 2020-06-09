using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace BUSK.Core.Utilities
{
    public static class ImageHelper
    {

        public static Image GetBitmap(BitmapImage bmpimg)
        {
            using var ms = new MemoryStream();
            new PngBitmapEncoder() { Frames = { BitmapFrame.Create(bmpimg) } }.Save(ms);
            ms.Position = 0;
            return Image.FromStream(ms);
        }

        public static BitmapImage GetBitmapImage(Image img)
        {
            var ms = new MemoryStream();
            var b = img != null ? new Bitmap(img) : new Bitmap(64, 64);
            b.Save(ms, System.Drawing.Imaging.ImageFormat.Png); b.Dispose();
            ms.Position = 0;
            var bmpimg = new BitmapImage();
            bmpimg.BeginInit(); bmpimg.StreamSource = ms; bmpimg.CacheOption = BitmapCacheOption.OnLoad; bmpimg.EndInit();
            return bmpimg;
        }

        public static void SaveBitmapImage(BitmapImage bmpimg, string location)
        {
            using var fs = new FileStream(location, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            new PngBitmapEncoder() { Frames = { BitmapFrame.Create(bmpimg) } }.Save(fs);
            fs.Flush(); fs.Close();
        }
    }
}
