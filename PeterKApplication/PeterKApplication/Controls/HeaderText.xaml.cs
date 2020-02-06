using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using PeterKApplication.Helpers;
using Xamarin.Forms;

namespace PeterKApplication.Controls
{
    /// <summary>
    /// Do you need a Big text, small or big text with a background and a top and bottom line, left aligned big small text, bold text, we've got it all.
    /// But seriously, it's used all over the place
    /// -----------
    /// SOME TEKST
    /// -----------
    ///
    /// SOME TEKST
    ///
    /// Some tekst
    ///
    /// Just use it 
    /// </summary>
    public partial class HeaderText : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(HeaderText), "HEADER NOT SET", BindingMode.TwoWay);
        public static readonly BindableProperty RightSideTextProperty = BindableProperty.Create("RightSideText", typeof(string), typeof(HeaderText), null, BindingMode.TwoWay);
        public static readonly BindableProperty HasBackgroundProperty = BindableProperty.Create("HasBackground", typeof(bool), typeof(HeaderText), true, BindingMode.TwoWay, propertyChanged: ReevaluateProperties);
        public static readonly BindableProperty IsBigProperty = BindableProperty.Create("IsBig", typeof(bool), typeof(HeaderText), false, BindingMode.TwoWay, propertyChanged: ReevaluateProperties);
        public static readonly BindableProperty IsBoldProperty = BindableProperty.Create("IsBold", typeof(bool), typeof(HeaderText), false, BindingMode.TwoWay, propertyChanged: ReevaluateProperties);
        public static readonly BindableProperty IsLeftAlignedProperty = BindableProperty.Create("IsLeftAligned", typeof(bool), typeof(HeaderText), false, BindingMode.TwoWay, propertyChanged: ReevaluateProperties);

        private static void ReevaluateProperties(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((HeaderText) bindable).Evaluate();
        }

        private void Evaluate()
        {
            if (HasBackground)
            {
                GridElement.BackgroundColor = StaticResourceHelper.Get<Color>("HeaderBackground");
                TextElement.BackgroundColor = RightTextElement.BackgroundColor = StaticResourceHelper.Get<Color>("HeaderBackground");
                GridElement.Margin = new Thickness
                {
                    Left = 0,
                    Top = 10,
                    Bottom = 10,
                    Right = 0
                };
                TextRow.Height = 30;
                TopLine.BackgroundColor = StaticResourceHelper.Get<Color>("HeaderTopAndBottom");
                BottomLine.BackgroundColor = StaticResourceHelper.Get<Color>("HeaderTopAndBottom");
                TextElement.FontAttributes = RightTextElement.FontAttributes = IsBold ? FontAttributes.Bold : FontAttributes.None;
            }
            else
            {
                GridElement.BackgroundColor = Color.Transparent;
                TextElement.BackgroundColor = RightTextElement.BackgroundColor = Color.Transparent;
                GridElement.Margin = 0;
                TextRow.Height = 25;
                TextElement.FontAttributes = RightTextElement.FontAttributes = !IsBold ? FontAttributes.None : FontAttributes.Bold;
                TopLine.BackgroundColor = Color.Transparent;
                BottomLine.BackgroundColor = Color.Transparent;
            }

            TextElement.HorizontalOptions = IsLeftAligned ? LayoutOptions.StartAndExpand : LayoutOptions.Center;
            if(IsLeftAligned) TextElement.Padding = new Thickness
            {
                Left = 20
            };

            TextElement.FontSize = RightTextElement.FontSize = IsBig ? 18 : 12;
        }

        public HeaderText()
        {
            InitializeComponent();
            Evaluate();
        }

        /// <summary>
        /// It's text, use it
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Should I show the light gray background with top and bottom lines? default is true
        /// </summary>
        public bool HasBackground
        {
            get => (bool)GetValue(HasBackgroundProperty);
            set => SetValue(HasBackgroundProperty, value);
        }

        /// <summary>
        /// Use big font? default is false
        /// </summary>
        public bool IsBig
        {
            get => (bool)GetValue(IsBigProperty);
            set => SetValue(IsBigProperty, value);
        }

        /// <summary>
        /// Bolder font? default false
        /// </summary>
        public bool IsBold
        {
            get => (bool) GetValue(IsBoldProperty);
            set => SetValue(IsBoldProperty, value);
        }

        /// <summary>
        /// Left aligned? default false
        /// </summary>
        public bool IsLeftAligned
        {
            get => (bool) GetValue(IsLeftAlignedProperty);
            set => SetValue(IsLeftAlignedProperty, value);
        }

        public string RightSideText
        {
            get => (string)GetValue(RightSideTextProperty);
            set => SetValue(RightSideTextProperty, value);
        }
    }
}
