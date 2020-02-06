using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Small gray dots, usually below a carousel
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarouselDots : ContentView
    {
        public CarouselDots()
        {
            InitializeComponent();
        }
    }
}