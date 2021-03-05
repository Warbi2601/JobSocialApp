﻿using JobSocialApp.Services;
using JobSocialApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp
{
    public partial class App : Application
    {
        public App()
        {
            Device.SetFlags(new string[] { "SwipeView_Experimental", "Shapes_Experimental", "AppTheme_Experimental", "CarouselView_Experimental", "Brush_Experimental", "Shell_UWP_Experimental", "RadioButton_Experimental", "" });
            InitializeComponent(); 
            
            MainPage = new AppShell();
            Application.Current.MainPage = MainPage;

            //MainPage = new MainPage();
            //var authService = DependencyService.Resolve<IFirebaseAuthenticator>();
            //if (!authService.isSignedIn())
            //{
            //    MainPage = new NavigationPage(new LoginView());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new HomeView());
            //}
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
