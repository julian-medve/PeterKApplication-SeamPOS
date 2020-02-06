using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using PeterKApplication.Extensions;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] b)
            {
                return b.ToImageSource();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
