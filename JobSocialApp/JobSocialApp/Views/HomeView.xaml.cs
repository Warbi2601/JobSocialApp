using JobSocialApp.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private async void SignOutClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IFirebaseAuthenticator>().signOut();
            //Application.Current.MainPage = new LoginView();

            await Shell.Current.GoToAsync("////login");
        }
    }
}