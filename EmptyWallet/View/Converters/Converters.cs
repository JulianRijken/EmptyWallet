using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EmptyWallet.View.Converters
{
    public class DateToStringConverter : IValueConverter
    {
        public object Convert(object type, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = type as DateTime? ?? throw new InvalidOperationException();
            return date.ToShortDateString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TypeToImageConverter : IValueConverter
    {
        public object Convert(object type, Type targetType, object parameter, CultureInfo culture)
        {
            string typeString = type as string ?? throw new InvalidOperationException();
            string pathString = $"pack://application:,,,/Resources/Bg/{typeString}.jpg";
            return new BitmapImage(new Uri(pathString, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UrlToImageConverter : IValueConverter
    {
        public object Convert(object urlObject, Type targetType, object parameter, CultureInfo culture)
        {
            string url = urlObject as string ?? throw new InvalidOperationException();
            url = url.Replace("\\/", "/");
            return new BitmapImage(new Uri(url, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TypeToIconConverter : IValueConverter
    {
        public object Convert(object type, Type targetType, object parameter, CultureInfo culture)
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return new BitmapImage(new Uri(
                    "https://www.clipartmax.com/png/small/129-1298272_free-icons-png-pokemon-\r\nball-no-background.png"));

            string typeString = type as string ?? throw new InvalidOperationException();
            string pathString = $"pack://application:,,,/Resources/Icons/{typeString}.png";
            return new BitmapImage(new Uri(pathString, UriKind.Absolute));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
