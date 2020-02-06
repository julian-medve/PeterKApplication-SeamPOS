using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace PeterKApplication
{
    public partial class App : Application
    {
        static IContainer _container;
        static readonly ContainerBuilder Builder = new ContainerBuilder();

        public App()
        {
            InitializeComponent();

            DependencyResolver.ResolveUsing(type => _container.IsRegistered(type) ? _container.Resolve(type) : null);
            
            if (_container == null)
            {
                var dataAccess = Assembly.GetExecutingAssembly();

                Builder.RegisterAssemblyTypes(dataAccess)
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .AsSelf();
            
                Builder.RegisterAssemblyTypes(dataAccess)
                    .Where(t => t.Name.EndsWith("ViewModel"))
                    .AsImplementedInterfaces()
                    .AsSelf();

                BuildContainer();
            }

            MainPage = new NavigationPage(new LoadingPage());
        }

        public new static App Current => Application.Current as App;

        public static bool ContextMenuShowing { get; set; }

        public static void RegisterType<T>() where T : class
        {
            Builder.RegisterType<T>();
        }

        public static void RegisterType<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            Builder.RegisterType<T>().As<TInterface>();
        }

        public static void RegisterTypeWithParameters<T>(Type param1Type, object param1Value, Type param2Type,
            string param2Name) where T : class
        {
            Builder.RegisterType<T>()
                .WithParameters(new List<Parameter>
                {
                    new TypedParameter(param1Type, param1Value),
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == param2Type && pi.Name == param2Name,
                        (pi, ctx) => ctx.Resolve(param2Type))
                });
        }

        public static void RegisterTypeWithParameters<TInterface, T>(Type param1Type, object param1Value,
            Type param2Type, string param2Name) where TInterface : class where T : class, TInterface
        {
            Builder.RegisterType<T>()
                .WithParameters(new List<Parameter>
                {
                    new TypedParameter(param1Type, param1Value),
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == param2Type && pi.Name == param2Name,
                        (pi, ctx) => ctx.Resolve(param2Type))
                }).As<TInterface>();
        }

        public static void BuildContainer()
        {
            _container = Builder.Build();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            AppCenter.Start("android=434a55a9-d07f-4796-87dc-f43ad2b8dd31;" +
                            "uwp={Your UWP App secret here};" +
                            "ios={Your iOS App secret here}",
                typeof(Analytics), typeof(Crashes));
            
            Crashes.TrackError(new Exception("WOOOO:" + DateTime.Now));

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}