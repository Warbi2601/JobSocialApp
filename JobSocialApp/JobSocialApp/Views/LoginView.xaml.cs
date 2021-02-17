using JobSocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();

    }

    private async void SignInClicked(object sender, EventArgs e)
    {
        // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
        try
        {
            var email = loginEmail.Text;
            var password = loginPassword.Text;
            await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailAndPassword(email, password);
            Application.Current.MainPage = new HomeView();
        } catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterView());
        }
    }
}