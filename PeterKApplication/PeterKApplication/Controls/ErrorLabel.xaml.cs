using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PeterKApplication.Shared.Dtos;
using Refit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ErrorLabel : ContentView
    {
        public static readonly BindableProperty ParameterProperty = BindableProperty.Create("Parameter", typeof(string),
            typeof(ErrorLabel), string.Empty, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty ErrorProperty = BindableProperty.Create("Error",
            typeof(ProblemDetails), typeof(ErrorLabel), null, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty AnyErrorProperty = BindableProperty.Create("AnyError",
            typeof(bool), typeof(ErrorLabel), false, BindingMode.TwoWay, propertyChanged: Reevaluate);

        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((ErrorLabel) bindable).Evaluate();
        }

        private void Evaluate()
        {
            Label.Text = null;
            Label.IsVisible = false;
            Label.HeightRequest = 0;

            string seterror = null;

            if (AnyError)
            {
                var firstError = Error?.Errors?.First().Value?.First();
                if (firstError != null)
                {
                    seterror = firstError;
                }
            }
            else
            {
                if (Parameter == null) return;
                if (Error?.Errors?.ContainsKey(Parameter) != true) return;
                if (Error.Errors.TryGetValue(Parameter, out var errs))
                {
                    seterror = errs.First();
                }
            }

            if (seterror != null)
            {
                Label.Text = seterror;
                Label.IsVisible = true;
                Label.HeightRequest = 20;
            }
        }

        public ErrorLabel()
        {
            InitializeComponent();
            Evaluate();
        }

        public string Parameter
        {
            get => (string) GetValue(ParameterProperty);
            set => SetValue(ParameterProperty, value);
        }

        public ProblemDetails Error
        {
            get => (ProblemDetails) GetValue(ErrorProperty);
            set => SetValue(ErrorProperty, value);
        }

        public bool AnyError
        {
            get => (bool)GetValue(AnyErrorProperty);
            set => SetValue(AnyErrorProperty, value);
        }
    }
}