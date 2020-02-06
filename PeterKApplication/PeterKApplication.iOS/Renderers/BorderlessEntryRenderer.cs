using PeterKApplication.Controls;
using PeterKApplication.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorederlessEntryRenderer))]
namespace PeterKApplication.iOS.Renderers
{
    public class BorederlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Layer.BorderColor = Color.Transparent.ToCGColor();
            }
        }
    }
}