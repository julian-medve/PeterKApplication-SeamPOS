using Android.Content;
using Android.Graphics.Drawables;
using PeterKApplication.Android.Renderers;
using PeterKApplication.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorederlessEntryRenderer))]

namespace PeterKApplication.Android.Renderers
{
    public class BorederlessEntryRenderer: EntryRenderer
    {
        public BorederlessEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = new ColorDrawable(global::Android.Graphics.Color.Transparent);
            }
        }
    }
}