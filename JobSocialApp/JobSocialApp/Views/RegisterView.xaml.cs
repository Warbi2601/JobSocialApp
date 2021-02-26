using JobSocialApp.Services;
using JobSocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        }

        private async void SignUpClicked(object sender, EventArgs e)
        {
            registerVM = BindingContext as RegisterViewModel;

            if (registerVM != null)
            {
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
    }
}