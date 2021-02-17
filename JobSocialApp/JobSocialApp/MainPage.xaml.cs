using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using JobSocialApp.Services;
using JobSocialApp.Views;

namespace JobSocialApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            //CheckWhetherUserIsSignedIn(); 
        }

        private async void CheckWhetherUserIsSignedIn()
        {
            var authService = DependencyService.Resolve<IFirebaseAuthenticator>();
            if(!authService.isSignedIn())
            {
                await Navigation.PushModalAsync(new LoginView());
            } else
            {
                await Navigation.PushModalAsync(new HomeView());
            }
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            // need to add validation 
            try
            {
                var email = registerEmail.Text;
                var password = registerPassword.Text;
                var uid = await DependencyService.Get<IFirebaseAuthenticator>().SignUpWithEmailAndPassword(email, password);
                await Navigation.PushModalAsync(new HomeView());
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
