using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using PeterKApplication.Data;
using PeterKApplication.Enums;
using PeterKApplication.Extensions;
using PeterKApplication.Pages;
using PeterKApplication.Pages.DpsTabbedPages;
using PeterKApplication.Pages.OwnerTabbedPages;
using PeterKApplication.Shared.Models;
using PeterKApplication.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PeterKApplication.Helpers;
using PeterKApplication.Shared.Enums;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace PeterKApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        public static bool DatabaseInitialized = false;
        
        public LoadingPage()
        {
            InitializeComponent();

            BindingContext = DependencyService.Resolve<LandingPageViewModel>();
            new Thread(async () => await InitializeDatabase()).Start();
        }

        private async Task InitializeDatabase()
        {
            try
            {
                using (var db = new LocalDbContext())
                {
                    using (UserDialogs.Instance.Loading("Preparing database"))
                    {
                        var refs = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                        var shared = refs.First(f => f.Name == "PeterKApplication.Shared");
                        var migrations = Assembly.Load(shared).GetTypes()
                            .Where(s => s.Namespace?.Equals("PeterKApplication.Shared.Migrations") == true)
                            .Select(s => s.Name).ToList();
                        var lastMigrations =
                            JsonConvert.DeserializeObject<string[]>(Preferences.Get("LastMigrations", "[]"));

                        if (!migrations.All(lastMigrations.Contains))
                        {
                            Console.WriteLine("Had to delete database");
                            await db.Database.EnsureDeletedAsync();
                        }

                        await db.Database.EnsureCreatedAsync();

                        Preferences.Set("LastMigrations", JsonConvert.SerializeObject(migrations));

                        db.SaveChanges();

                        DatabaseInitialized = true;
                    }
                }

            }
            catch (Exception e) {
                System.Diagnostics.Debug.Write("InitializeDatabase : " + e.ToString());
            }
            Device.BeginInvokeOnMainThread(async () => await CheckLogin());
        }

        private async Task CheckLogin()
        {
            Console.WriteLine("Refreshing token, or showing login screen");

            var bc = BindingContext.As<LandingPageViewModel>();

            try
            {
                await bc.RefreshToken();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception on CheckLogin : " + e.ToString());
            }


            switch (bc.GetLoginStatus())
            {
                case LoginStatus.Dps:
                    Application.Current.MainPage = new DpsMasterDetailPage();
                    break;
                case LoginStatus.Owner:
                    Application.Current.MainPage = new OwnerMasterDetailPage();
                    break;
                case LoginStatus.NotLoggedIn:
                    Application.Current.MainPage = new LoginPage();
                    break;
                case LoginStatus.OwnerNotSetup:
                    Application.Current.MainPage = new OwnerWalkThroughPage();
                    break;
                case LoginStatus.OwnerNotVerified:
                    Application.Current.MainPage = new SmsConfirmationPage(bc.GetPhoneNumber());
                    break;
            }
        }
    }
}