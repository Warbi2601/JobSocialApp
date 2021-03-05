using JobSocialApp.ViewModels;
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
    public partial class AddNewJobView : ContentPage
    {
        private AddNewJobViewModel newJobVM = null;

        public AddNewJobView()
        {
            InitializeComponent();

            BindingContext = new AddNewJobViewModel();

            Shell.SetTabBarIsVisible(this, false);
        }

        private void CreateNewJob(object sender, EventArgs e)
        {
            newJobVM = BindingContext as AddNewJobViewModel;

            if (newJobVM != null)
            {
                newJobVM.CreateNewJobAsync();
            }
        }

        private async void CancelAndGoBack(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}