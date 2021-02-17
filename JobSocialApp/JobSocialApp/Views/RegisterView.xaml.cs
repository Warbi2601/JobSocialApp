using JobSocialApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
            try
            {
                var email = registerEmail.Text;
                var password = registerPassword.Text;
                var uid = await DependencyService.Get<IFirebaseAuthenticator>().SignUpWithEmailAndPassword(email, password);
                if (uid != string.Empty)
                {
                    Application.Current.MainPage = new HomeView();
                    await Navigation.PopAsync(); 
                } // else something went wrong
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}