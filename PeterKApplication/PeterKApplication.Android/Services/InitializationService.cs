using System.Linq;
using Android.Views;
using PeterKApplication.ServiceInterfaces;
using Xamarin.Forms;
using Color = Android.Graphics.Color;

namespace PeterKApplication.Android.Services
{
    public class InitializationService : IInitializationService
    {
        public void SetStatusbarColor(Xamarin.Forms.Color color)
        {
            var c = Color.ParseColor(color.ToHex());
            MainActivity.Win.SetStatusBarColor(c);
        }
    }
}