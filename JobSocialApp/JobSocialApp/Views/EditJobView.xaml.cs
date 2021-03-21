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
                    if (string.IsNullOrEmpty(editJobVM.JobTitle))
                    {
                        await DisplayAlert("Error", "Job Title is empty, please add one and try again", "ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(editJobVM.Salary))
                    {
                        await DisplayAlert("Error", "Salary is empty, please add one and try again", "ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(editJobVM.Location))
                    {
                        await DisplayAlert("Error", "Location is empty, please add one and try again", "ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(editJobVM.Description))
                    {
                        await DisplayAlert("Error", "Description is empty, please add one and try again", "ok");
                        return;
                    }

                    if (string.IsNullOrEmpty(editJobVM.PostCode))
                    {
                        await DisplayAlert("Error", "Postcode is empty, please add one and try again", "ok");
                        return;
                    }

                    bool wasSuccessful = await editJobVM.UpdateJobAsync();
                    if(wasSuccessful)
                    {
                        await DisplayAlert("Success", "Job successfully updated", "Ok");
                        await Navigation.PopAsync();
                    }
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