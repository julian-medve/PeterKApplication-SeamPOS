using System;
using System.Collections.Generic;
using System.Linq;
using PeterKApplication.Converters;
using PeterKApplication.Extensions;
using PeterKApplication.Helpers;
using Xamarin.Forms;

namespace PeterKApplication.Controls
{
    public class HorizontalButtonEventArgs : EventArgs
    {
        public object OriginalObject { get; set; }
    }

    /// <summary>
    /// A button with left side image, text and then a chevron to the right
    /// There you go:
    ///------------------
    /// [+] Button text >
    ///------------------
    /// </summary>
    public partial class HorizontalButton : ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string),
            typeof(HorizontalButton), "BUTTON TEXT", BindingMode.TwoWay);

        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(string),
            typeof(HorizontalButton), "ImaePlacement.png", BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty FallbackImageProperty = BindableProperty.Create("FallbackImage",
            typeof(string), typeof(HorizontalButton), null, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty RemoveChevronProperty = BindableProperty.Create("RemoveChevron",
            typeof(bool),
            typeof(HorizontalButton), false, BindingMode.TwoWay);

        public static readonly BindableProperty IsBoldProperty = BindableProperty.Create("IsBold", typeof(bool),
            typeof(HorizontalButton), false, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty SubtitleProperty =
            BindableProperty.Create("Subtitle", typeof(string), typeof(HorizontalButton), null);

        public static readonly BindableProperty OriginalObjectProperty =
            BindableProperty.Create("OriginalObject", typeof(object), typeof(HorizontalButton), null);

        public static readonly BindableProperty HideImageProperty = BindableProperty.Create("HideImage", typeof(bool),
            typeof(HorizontalButton), false, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty RawImageProperty = BindableProperty.Create("RawImage", typeof(byte[]),
            typeof(HorizontalButton), null, BindingMode.TwoWay, propertyChanged: Reevaluate);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color),
            typeof(HorizontalButton), Color.Black, BindingMode.TwoWay);

        public static readonly BindableProperty IsSmallProperty = BindableProperty.Create("IsSmall",
            typeof(bool),
            typeof(HorizontalButton), false, BindingMode.TwoWay, propertyChanged: Reevaluate);

        private int _gridRowHeight = 80;
        private int _imagesMargin;

        private static void Reevaluate(BindableObject bindable, object oldvalue, object newvalue)
        {
            ((HorizontalButton) bindable).Evaluate();
        }

        private void Evaluate()
        {
            TextElement.FontAttributes = IsBold ? FontAttributes.Bold : FontAttributes.None;

            if (RawImage?.Any() == true)
            {
                ImageElement.Source = RawImage.ToImageSource();
            }
            else if (!string.IsNullOrEmpty(FallbackImage))
            {
                ImageElement.Source = FallbackImage;
            }
            else
            {
                ImageElement.Source = Image;
            }

            if (HideImage)
            {
                TextElement.Margin = new Thickness
                {
                    Left = 20
                };
            }
            else
            {
                TextElement.Margin = 0;
            }

            if (IsSmall)
            {
                GridRowHeight = 50;
                ImagesMargin = 10;
            }
            else
            {
                GridRowHeight = 80;
                ImagesMargin = 20;
            }
        }

        public HorizontalButton()
        {
            InitializeComponent();

            Grid.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    OnClicked?.Invoke(this, new HorizontalButtonEventArgs
                    {
                        OriginalObject = OriginalObject
                    });
                })
            });

            Evaluate();
        }

        /// <summary>
        /// Text, use it
        /// </summary>
        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// Subtitle, use it optionally
        /// </summary>
        public string Subtitle
        {
            get => (string) GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        /// <summary>
        /// Image, use it
        /// </summary>
        public string Image
        {
            get => (string) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        /// <summary>
        /// Remove right chevron arrow? default false
        /// </summary>
        public bool RemoveChevron
        {
            get => (bool) GetValue(RemoveChevronProperty);
            set => SetValue(RemoveChevronProperty, value);
        }

        /// <summary>
        /// Bold text
        /// </summary>
        public bool IsBold
        {
            get => (bool) GetValue(IsBoldProperty);
            set => SetValue(IsBoldProperty, value);
        }

        /// <summary>
        /// Object you want passed through the EventArgs of clicked event handler
        /// </summary>
        public object OriginalObject
        {
            get => GetValue(OriginalObjectProperty);
            set => SetValue(OriginalObjectProperty, value);
        }

        /// <summary>
        /// Hide right side image? default false
        /// </summary>
        public bool HideImage
        {
            get => (bool) GetValue(HideImageProperty);
            set => SetValue(HideImageProperty, value);
        }

        public string FallbackImage
        {
            get => (string) GetValue(FallbackImageProperty);
            set => SetValue(FallbackImageProperty, value);
        }

        public byte[] RawImage
        {
            get => (byte[]) GetValue(RawImageProperty);
            set => SetValue(RawImageProperty, value);
        }

        public Color TextColor
        {
            get => (Color) GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        public int GridRowHeight
        {
            get => _gridRowHeight;
            set
            {
                _gridRowHeight = value;
                OnPropertyChanged(nameof(GridRowHeight));
            }
        }

        public bool IsSmall
        {
            get => (bool) GetValue(IsSmallProperty);
            set => SetValue(IsSmallProperty, value);
        }

        public int ImagesMargin
        {
            get => _imagesMargin;
            set
            {
                _imagesMargin = value;
                OnPropertyChanged(nameof(ImagesMargin));
            }
        }

        /// <summary>
        /// Called when clicked
        /// </summary>
        public event EventHandler<HorizontalButtonEventArgs> OnClicked;
    }
}