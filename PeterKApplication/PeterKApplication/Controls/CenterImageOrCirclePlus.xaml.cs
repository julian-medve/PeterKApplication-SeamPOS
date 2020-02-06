using System;
using System.IO;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    public class CenterImageOrCirclePlusEventArgs
    {
        public byte[] Image { get; set; }
    }

    /// <summary>
    /// Shows an image or ( + ) to add an image
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CenterImageOrCirclePlus : ContentView
    {
        public static readonly BindableProperty ImageProperty = BindableProperty.Create("Image", typeof(byte[]),
            typeof(CenterImageOrCirclePlus), null, BindingMode.TwoWay);

        public CenterImageOrCirclePlus()
        {
            InitializeComponent();
        }

        private async void AddImageTapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                UserDialogs.Instance.Alert("Warning", "Changing image requires camera feature!", "Ok");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg",
                MaxWidthHeight = 500,
                PhotoSize = PhotoSize.MaxWidthHeight
            });

            if (file == null)
                return;
            
            var ms = new MemoryStream();
            
            file.GetStream().CopyTo(ms);

            ImageChanged?.Invoke(this, new CenterImageOrCirclePlusEventArgs
            {
                Image = ms.ToArray()
            });
        }

        public byte[] Image
        {
            get => (byte[]) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }

        public event EventHandler<CenterImageOrCirclePlusEventArgs> ImageChanged;
    }
}