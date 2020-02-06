using System;
using System.Globalization;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class ByteArrayToImageSourceWithDefaultConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var istruthy = new IsTruthyConverter();
            var byteArrayConverter = new ByteArrayToImageSourceConverter();
            
            if ((bool)istruthy.Convert(value, targetType, parameter, culture))
            {
                return byteArrayConverter.Convert(value, targetType, parameter, culture);
            }

            return (string) parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}