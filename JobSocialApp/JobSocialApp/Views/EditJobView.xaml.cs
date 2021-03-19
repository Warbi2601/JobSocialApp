using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Models;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditJobView : ContentPage
    {
        private EditJobViewModel editJobVM = null;
        private Job jobObj = null;

        public EditJobView(Job jobData)
        {
            InitializeComponent();

            jobObj = jobData;

            BindingContext = new EditJobViewModel();
            editJobVM = BindingContext as EditJobViewModel;

            Shell.SetTabBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            editJobVM = BindingContext as EditJobViewModel;

            if (editJobVM != null)
            {
                editJobVM.PopulateEditJobVMData(jobObj);
            }
        }

        private async void UpdateJobAsync(object sender, EventArgs e)
        {
            editJobVM = BindingContext as EditJobViewModel;

            if (editJobVM != null)
            {
                try
                {
                    await editJobVM.UpdateJobAsync();
                    await DisplayAlert("Success", "Job successfully updated", "Ok");
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "Error while updating the job", "Ok");
                    Console.WriteLine(ex);
                }
            }
        }
    }
}