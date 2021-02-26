using JobSocialApp.Services;
using JobSocialApp.ViewModels;
using JobSocialApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = new AppShellViewModel();

            var authService = DependencyService.Resolve<IFirebaseAuthenticator>();

            if (!authService.isSignedIn())
            {
                Task.Run(async () => await Shell.Current.GoToAsync("///login"));
            }
            else
            {
                Task.Run(async () => await Shell.Current.GoToAsync("///home"));
            }
        }
        private async void ShowSettingsView(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new SettingsView());
        }

        private async void SignOutClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().signOut();
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync("///login");
        }

        private async void EditProfileClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = false;
            await Navigation.PushAsync(new EditProfileView());
        }
    }
}
