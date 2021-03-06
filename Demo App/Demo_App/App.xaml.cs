﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Demo_App.Model;

using Xamarin.Forms;

namespace Demo_App
{
    public partial class App : Application
    {
        static public int ScreenHeight;
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDYxMUAzMTM2MmUzMjJlMzBvMWo3aVRIWGF6UzRGMzdxYUZ2b0cvQzJnOGtpZnNUSzVBYVAvMU1UcEJzPQ==");
            InitializeComponent();
            DependencyService.Register<IProgressInterface>();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            //Current.MainPage = new NavigationPage(new SetAppointmentPage());
            Current.MainPage = new NavigationPage(new WelcomePage());

            //Current.MainPage.On<Xamarin.Forms.PlatformConfiguration.Windows>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            //Current.MainPage = new WelcomePage();

            //Current.MainPage = new TabbedPage
            //{
            //    Children =
            //    {
            //        new NavigationPage(new LoginPage())
            //        {
            //            Title = "Login",
            //            Icon = Device.OnPlatform<string>("Icon.png",null,null),
            //        },
            //        new NavigationPage(new RegPage())
            //        {
            //            Title = "Registration",
            //            Icon = Device.OnPlatform<string>("Icon.png",null,null)
            //        }
            //    }
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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
