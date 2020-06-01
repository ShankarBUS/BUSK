using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using BUSK.Core.Shortcutting;

namespace BUSK.Core.Converters
{

    public class CountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int)value;

            return count > 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertCountToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int)value;

            return count == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class HeightToThicknessConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var height = (double)value;
            var p = (string)parameter;
            if (p == "m") { return new Thickness(0, height, 0, 0); }
            else if (p == "p") { return new Thickness(0, -height, 0, 0); }
            return new Thickness(0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (bool)value;
            return !input;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value == null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ParametrizedBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
            {
                flag = (bool)value;
            }
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue && nullable.Value;
            }
            if (parameter != null)
            {
                if (bool.TryParse(parameter.ToString(), out bool result) && !result)
                {
                    flag = !flag;
                }
            }
            return (flag ? Visibility.Visible : Visibility.Collapsed);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((value is Visibility visibility) && (visibility == Visibility.Visible));
        }
    }

    public class ShortcutTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var st = (ShortcutType)value;
            var param = (string)parameter;
            return param switch
            {
                "u" => st == ShortcutType.UserDefined ? Visibility.Visible : Visibility.Collapsed,
                "!u" => st == ShortcutType.UserDefined ? Visibility.Collapsed : Visibility.Visible,
                "i" => st == ShortcutType.Inbuilt ? Visibility.Visible : Visibility.Collapsed,
                "!i" => st == ShortcutType.Inbuilt ? Visibility.Collapsed : Visibility.Visible,
                "e" => st == ShortcutType.ExtensionDefined ? Visibility.Visible : Visibility.Collapsed,
                "!e" => st == ShortcutType.ExtensionDefined ? Visibility.Collapsed : Visibility.Visible,
                _ => Visibility.Collapsed,
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
