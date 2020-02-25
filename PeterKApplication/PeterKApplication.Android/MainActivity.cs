using System;
using System.Drawing;
using System.Threading;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using PeterKApplication.Android.Services;
using PeterKApplication.ServiceInterfaces;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using ActionMode = Android.Support.V7.View.ActionMode;
using CarouselViewRenderer = CarouselView.FormsPlugin.Android.CarouselViewRenderer;
using Color = Android.Graphics.Color;
using Android.Support.V7.View;
using ContextMenu.Droid;
using FFImageLoading.Forms.Platform;

namespace PeterKApplication.Android
{
    
    [Activity(Label = "PeterKApplication", Theme = "@style/MainTheme", MainLauncher = true,
        Icon = "@drawable/app_icon_logo",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        public static Window Win;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            Xam.Forms.VideoPlayer.Android.VideoPlayerRenderer.Init();
            Win = Window;

            base.OnCreate(savedInstanceState);

            ContextMenuViewRenderer.Preserve();

            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CarouselViewRenderer.Init();
            UserDialogs.Init(this);
            
            App.RegisterType<IInitializationService, InitializationService>();


            //  Custom Popup
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);


            var app = new App();
            
            LoadApplication(app);

            Window.SetStatusBarColor(Color.White);


            // Show Gif image for checkout tick animation

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override global::Android.Support.V7.View.ActionMode StartSupportActionMode(global::Android.Support.V7.View.ActionMode.ICallback callback)
        {
            App.ContextMenuShowing = true;
            return base.StartSupportActionMode(callback);
        }

        public override void OnSupportActionModeFinished(global::Android.Support.V7.View.ActionMode mode)
        {
            App.ContextMenuShowing = false;
            base.OnSupportActionModeFinished(mode);
        }

    }
}