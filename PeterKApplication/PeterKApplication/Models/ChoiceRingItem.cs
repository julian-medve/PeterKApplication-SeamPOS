using System;
using System.ComponentModel;

namespace PeterKApplication.Models
{
    public class ChoiceRingItem
    {
        public ChoiceRingItem(string text = null)
        {
            if(text != null) Text = text;
        }
        public string Text { get; set; }
        public bool Selected { get; set; }
        public string Image => Selected ? "EllipseShape.png" : "options.png";
        public Guid Id { get; set; }
    }
}
