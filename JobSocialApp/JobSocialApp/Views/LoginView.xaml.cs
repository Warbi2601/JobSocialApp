using JobSocialApp.ViewModels;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private LoginViewModel loginVM = null;
        //public ICommand RegisterCommand => new Command(RegisterClicked);

        public LoginView()
        {
            InitializeComponent();

            BindingContext = new LoginViewModel();
            loginVM = BindingContext as LoginViewModel;

            if (loginVM != null)
            {
                loginVM.setTitle();
            }
        }

        private async void ChangeLangClicked(object sender, EventArgs e)
        {
            loginVM = BindingContext as LoginViewModel;

            if (loginVM != null)
            {
                loginVM.setTitle();
            }
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

        public async void RegisterClicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new RegisterView());
            Routing.RegisterRoute("/main", typeof(AppShell));
            await Shell.Current.GoToAsync("////register");

            //await Navigation.PopAsync();
            //await Navigation.PushAsync(new RegisterView());
        }

        private void Button_EnterApp(object sender, EventArgs e)
        {

        }
    }
}