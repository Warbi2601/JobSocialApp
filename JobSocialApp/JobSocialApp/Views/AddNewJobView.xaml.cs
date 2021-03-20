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
            newJobVM = BindingContext as AddNewJobViewModel;

            Shell.SetTabBarIsVisible(this, false);
        }

        private async void CreateNewJob(object sender, EventArgs e)
        {
            newJobVM = BindingContext as AddNewJobViewModel;

            if (newJobVM != null)
            {
                try
                {
                    var newJob = await newJobVM.CreateNewJobAsync();

                    if (newJob != null)
                    {
                        await DisplayAlert("Success", "Job successfully posted", "Ok");
                        await Navigation.PopAsync();
                        await newJobVM.PostJobToFacebook(newJob);
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error posting the job", "Ok");
                    Console.WriteLine(ex);
                }
            }
        }
    }
}