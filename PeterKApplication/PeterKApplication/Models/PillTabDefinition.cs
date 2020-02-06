using System;

namespace PeterKApplication.Models
{
    public class PillTabDefinition
    {
        public PillTabDefinition()
        {
            Id = Guid.NewGuid();
        }
        public string BgColor => Selected ? "#1f4ba5" : "#ffffff";
        public string TextColor =>  Selected ? "#ffffff" : "#000000";
        public bool Selected { get; set; }
        public Guid Id { get; set; }
    }
}