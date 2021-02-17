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
                var email = loginEmail.Text; // valid email
                var password = loginPassword.Text; // min 6 chars

                bool validEmail = IsValidEmail(email);
                bool validPass = password.Length >= 6;

                if(!validEmail || !validPass)
                {
                    if (!validEmail) await DisplayAlert("Login Failed", "Email is not valid", "Ok");
                    if (!validPass) await DisplayAlert("Login Failed", "Password does not meet requirements", "Ok");
                }
                else
                {
                    var a = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailAndPassword(email, password);
                    Application.Current.MainPage = new HomeView();
                }
            }
            catch (Exception ex)
            {
                if(ex.GetType().ToString() == "Firebase.Auth.FirebaseAuthInvalidUserException") 
                    await DisplayAlert("Login Failed", "Email or Password incorrect", "Ok");
                Console.WriteLine(ex);
            }
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterView());
        }
    }
}