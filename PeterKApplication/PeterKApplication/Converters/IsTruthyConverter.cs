using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace PeterKApplication.Converters
{
    public class IsTruthyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value switch
            {
                null => false,
                bool b => b,
                byte b => b > 0,
                sbyte s => s > 0,
                char c => c != char.MinValue,
                decimal d => d != 0,
                double d => Math.Abs(d) > 0.001,
                float f => Math.Abs(f) > 0.001,
                int i => i != 0,
                uint u => u > 0,
                long l => l != 0,
                ulong u => u > 0,
                short s => s != 0,
                ushort u => u > 0,
                string s => !string.IsNullOrEmpty(s),
                byte[] b => b != null && b.Any(),
                _ => true
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}