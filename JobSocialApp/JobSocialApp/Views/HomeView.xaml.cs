using JobSocialApp.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private HomeViewModel homeVM = null;
        public HomeView()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
            homeVM = BindingContext as HomeViewModel;

            if (homeVM != null)
            {
                homeVM.setTitle();
            }

        }

        private async void SignOutClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().signOut();
            Routing.RegisterRoute("/main", typeof(AppShell));
            await Shell.Current.GoToAsync("////login");
        }

        private async void TestClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Test());
        }
    }
}