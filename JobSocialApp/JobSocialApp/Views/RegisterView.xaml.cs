using JobSocialApp.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterView : ContentPage
    {
        private RegisterViewModel registerVM = null;

        public RegisterView()
        {
            InitializeComponent();

            BindingContext = new RegisterViewModel();
            registerVM = BindingContext as RegisterViewModel;
        }

        protected override void OnAppearing()
        {
            BindingContext = new RegisterViewModel();
            registerVM = BindingContext as RegisterViewModel;
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            registerVM = BindingContext as RegisterViewModel;

            if (registerVM != null)
            {
                if (string.IsNullOrEmpty(registerVM.RegisterEmail))
                {
                    await DisplayAlert("Error", "Email is required", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(registerVM.RegisterFirstName))
                {
                    await DisplayAlert("Error", "First Name is required", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(registerVM.RegisterLastName))
                {
                    await DisplayAlert("Error", "Last Name is required", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(registerVM.RegisterJobTitle))
                {
                    await DisplayAlert("Error", "Job Title is required", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(registerVM.RegisterPassword1) || string.IsNullOrEmpty(registerVM.RegisterPassword2))
                {
                    await DisplayAlert("Error", "Both Password fields are required", "Ok");
                    return;
                }

                if (registerVM.RegisterPassword1.Length < 6)
                {
                    await DisplayAlert("Error", "Password needs to be minimum 6 characters", "Ok");
                    return;
                }

                if (registerVM.RegisterPassword1 != registerVM.RegisterPassword2)
                {
                    await DisplayAlert("Error", "Passwords don't match", "Ok");
                    return;
                }

                if (registerVM.IsCompany)
                {
                    if (string.IsNullOrEmpty(registerVM.CompanyName))
                    {
                        await DisplayAlert("Error", "Company Name required", "Ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(registerVM.CompanyEmail))
                    {
                        await DisplayAlert("Error", "Company Email required", "Ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(registerVM.CompanyPhone))
                    {
                        await DisplayAlert("Error", "Company Phone required", "Ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(registerVM.CompanyWebsite))
                    {
                        await DisplayAlert("Error", "Company Website required", "Ok");
                        return;
                    }
                }

                ToClientRegisterObject message = await registerVM.SignInProcedure();

                if (message != null && !message.IsSuccessful)
                {
                    await DisplayAlert(message.MessageHeader, message.MessageBody, message.ButtonText);
                }
            }
        }

        public async void LoginClicked(object sender, EventArgs e)
        {
            Routing.RegisterRoute("/main", typeof(AppShell));
            await Shell.Current.GoToAsync("////login");
        }

        private void IsCompany_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            CompanyRegisterSection.IsVisible = e.Value;
        }
    }
}