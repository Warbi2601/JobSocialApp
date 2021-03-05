using JobSocialApp.Services;
using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Models;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        private HomeViewModel homeVM = null;
        public HomeView()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel();
            homeVM = BindingContext as HomeViewModel;

            if (homeVM != null)
            {
                homeVM.setTitle();
            }

        }
        private async void TestClicked(object sender, EventArgs e)
        {
            // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
            Console.WriteLine("button pressed");
            TranslationManager.Instance.changeLang();
        }
        

    }
}