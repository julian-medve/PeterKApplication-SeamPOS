using System;
using System.Globalization;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class IfIsTruthyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var istruthy = new IsTruthyConverter();
            if ((bool) istruthy.Convert(value, targetType, parameter, culture))
            {
                return (string)parameter;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}