using System;
using System.Globalization;
using Xam.Forms.VideoPlayer;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class ToVideoSourceConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string s)
            {
                var vs = VideoSource.FromUri(s);
                return vs;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}