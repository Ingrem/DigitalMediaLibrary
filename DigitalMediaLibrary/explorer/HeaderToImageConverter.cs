using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DigitalMediaLibrary.explorer
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class HeaderToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (((string) value).Contains(@"\"))
            {
                Uri uri = new Uri("pack://application:,,,/Imgs/diskdrive.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
            else
            {
                Uri uri = new Uri("pack://application:,,,/Imgs/folder.png");
                BitmapImage source = new BitmapImage(uri);
                return source;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot convert back");
        }
    }
}