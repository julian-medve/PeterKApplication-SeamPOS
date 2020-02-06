using Xamarin.Forms;

namespace PeterKApplication.Helpers
{
    public static class StaticResourceHelper
    {
        public static T Get<T>(string resource)
        {
            return (T) Application.Current.Resources[resource];
        }
    }
}