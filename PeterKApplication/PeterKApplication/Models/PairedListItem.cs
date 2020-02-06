using System;
using PeterKApplication.Helpers;
using Xamarin.Forms;

namespace PeterKApplication.Models
{
    public class PairedListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool NotSelected => !Selected;
        public bool Selected { get; set; }
        public string Image { get; set; } = "ImaePlacement.png";

        public Color Color => Selected
            ? StaticResourceHelper.Get<Color>("MediumGrayColor")
            : StaticResourceHelper.Get<Color>("LightGrayColor");
    }
}