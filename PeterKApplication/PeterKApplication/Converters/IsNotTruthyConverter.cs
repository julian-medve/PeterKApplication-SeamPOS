using System;
using System.Globalization;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class IsNotTruthyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var istru = new IsTruthyConverter();
            return !(bool) istru.Convert(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}