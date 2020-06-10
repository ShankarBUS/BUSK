using ModernWpf.Controls;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace BUSK.UI
{
    public class FontIconEx : FontIcon
    {
        static FontIconEx()
        {
            GlyphProperty.OverrideMetadata(typeof(FontIconEx), new FrameworkPropertyMetadata(string.Empty, OnGlyphChanged));
        }

        public FontIconEx()
        {
            Loaded += FontIconEx_Loaded;
        }

        private static void OnGlyphChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var fontIconEx = (FontIconEx)d;
            if (fontIconEx._textBlock2 != null)
            {
                fontIconEx._textBlock2.Text = GetGlyph((string)e.NewValue);
            }
        }

        private TextBlock _textBlock2;

        private void FontIconEx_Loaded(object sender, RoutedEventArgs e)
        {
            if (_textBlock2 == null)
            {
                _textBlock2 = (TextBlock)typeof(FontIcon).GetField("_textBlock", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
            }

            _textBlock2.Text = GetGlyph(Glyph);
        }

        private static string GetGlyph(string value)
        {
            if (Enum.TryParse(value, true, out Symbol symbol))
            {
                return ConvertToString(symbol);
            }
            else
            {
                return value;
            }
        }

        private static string ConvertToString(Symbol symbol)
        {
            return char.ConvertFromUtf32((int)symbol).ToString();
        }
    }
}
