using System;
using System.Globalization;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class NotConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                bool b => !b,
                null => true,
                _ => false
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}