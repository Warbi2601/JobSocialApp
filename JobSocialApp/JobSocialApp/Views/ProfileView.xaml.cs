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
    public partial class ProfileView : ContentPage
    {
        public ProfileView()
        {
            InitializeComponent();
        }

        private async void AddNewJobClicked(object sender, EventArgs e)
        {
            //Routing.RegisterRoute("/main", typeof(AppShell));
            //await Shell.Current.GoToAsync("////addJob");
            await Navigation.PushAsync(new AddNewJobView());
        }
    }
}