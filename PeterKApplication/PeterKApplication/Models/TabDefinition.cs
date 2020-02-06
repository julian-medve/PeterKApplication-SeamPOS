using PeterKApplication.Shared.Models;
using Xamarin.Forms;

namespace PeterKApplication.Models
{
    public class TabDefinition
    {
        public string Title { get; set; }
        public Layout Element { get; set; }
        public bool IsSelected { get; set; }
        public object Object { get; set; }
        public string Id { get; set; }
    }
}