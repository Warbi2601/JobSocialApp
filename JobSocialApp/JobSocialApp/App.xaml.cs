using JobSocialApp.Models;
using JobSocialApp.Services;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Views;
using System;
using Xamarin.Essentials;
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

            String themeName = Preferences.Get("Theme", "light");

            if (themeName == "light")
            {
                ResourcesHelper.SetLightMode();
            }
            else
            {
                ResourcesHelper.SetDarkMode();
            }

            Application.Current.Properties["id"] = "";
            Application.Current.MainPage = MainPage;
        }

        protected override void OnStart()
        {
            UpdateLastActiveUser();
            TranslationManager.Instance.setLanguage();
        }


        protected override void OnSleep()
        {
            UpdateLastActiveUser();
        }

        protected override void OnResume()
        {
            UpdateLastActiveUser();
        }

        private async void UpdateLastActiveUser()
        {
            try
            {
                AppContext context = new AppContext();
                var user = await context.GetCurrentUser();

                if (user != null)
                {
                    //update last active
                    UserActions crud = new UserActions();
                    user.lastActive = DateTime.Now;
                    crud.UpdateUser(user);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lifecycle method error");
                Console.WriteLine(ex);
            }
        }

    }
}
