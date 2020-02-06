using System.IO;
using Xamarin.Forms;

namespace PeterKApplication.Extensions
{
    public static class ByteArrayExtensions
    {
        public static ImageSource ToImageSource(this byte[] byteArray)
        {
            ImageSource retSource = null;

            if (byteArray != null)
            {
                retSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
            }

            return retSource;
        }
    }
}