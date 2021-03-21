using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Models;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobsHubView : ContentPage
    {
        private JobsHubViewModel jobsHubVM = null;

        public JobsHubView()
        {
            InitializeComponent();

            BindingContext = new JobsHubViewModel();
            jobsHubVM = BindingContext as JobsHubViewModel;

            Shell.SetTabBarIsVisible(this, false);
        }

        protected override async void OnAppearing()
        {
            jobsHubVM = BindingContext as JobsHubViewModel;

            if (jobsHubVM != null)
            {
                await jobsHubVM.PopulateJobs();
            }
        }

        private async void CreateNewJob(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddNewJobView());
        }

        private async void ViewSelectedJobClicked(object sender, ItemTappedEventArgs e)
        {
            var selectedJobData = e.Item as Job;

            try
            {
                await Navigation.PushAsync(new JobPreviewView(selectedJobData));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}