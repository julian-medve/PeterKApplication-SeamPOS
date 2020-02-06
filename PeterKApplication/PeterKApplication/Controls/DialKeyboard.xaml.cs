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
    /// Event send when any of the buttons is clicked
    /// </summary>
    public class DialKeyboardEventArgs: EventArgs
    {
        /// <summary>
        /// Clicked number value
        /// </summary>
        public int Clicked { get; set; }
    }

    /// <summary>
    /// Dial keyboard, such as the one you'd encounter when visiting your grandma
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DialKeyboard : ContentView
    {
        public DialKeyboard()
        {
            InitializeComponent();

            Button0.GestureRecognizers.Add(ButtonClicked(0));
            Button1.GestureRecognizers.Add(ButtonClicked(1));
            Button2.GestureRecognizers.Add(ButtonClicked(2));
            Button3.GestureRecognizers.Add(ButtonClicked(3));
            Button4.GestureRecognizers.Add(ButtonClicked(4));
            Button5.GestureRecognizers.Add(ButtonClicked(5));
            Button6.GestureRecognizers.Add(ButtonClicked(6));
            Button7.GestureRecognizers.Add(ButtonClicked(7));
            Button8.GestureRecognizers.Add(ButtonClicked(8));
            Button9.GestureRecognizers.Add(ButtonClicked(9));
        }

        /// <summary>
        /// Public handler that's called on button click
        /// </summary>
        public event EventHandler<DialKeyboardEventArgs> OnKeyPressed;

        public TapGestureRecognizer ButtonClicked(int b)
        {
            return new TapGestureRecognizer {
                Command = new Command(() =>
                {
                    OnKeyPressed?.Invoke(this, new DialKeyboardEventArgs
                    {
                        Clicked = b
                    });
                })
            };
        }
    }
}