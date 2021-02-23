using JobSocialApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private LoginViewModel loginVM = null;

        public LoginView()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
        }

        private async void SignInClicked(object sender, EventArgs e)
        {
            // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
            loginVM = BindingContext as LoginViewModel;

            if (loginVM != null)
            {
                ToClientLoginObject message = await loginVM.SignInProcedure();

                if(message != null && !message.IsSuccessful)
                {
                    await DisplayAlert(message.MessageHeader, message.MessageBody, message.ButtonText);
                }
            }
        }

        private async void RegisterClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterView());
        }

        private void Button_EnterApp(object sender, EventArgs e)
        {

        }
    }
}