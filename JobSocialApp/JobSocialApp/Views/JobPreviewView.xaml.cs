using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Models;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobPreviewView : ContentPage
    {
        private JobPreviewViewModel jobPreviewVM = null;
        private Job jobObj = null;

        public JobPreviewView(Job jobData, Boolean isEdited)
        {
            InitializeComponent();

            BindingContext = new JobPreviewViewModel();
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            jobObj = jobData;

            RenderButtons(isEdited);

            Shell.SetTabBarIsVisible(this, false);
        }

        protected override void OnAppearing()
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                jobPreviewVM.PopulateJobVMData(jobObj);
            }
        }

        private void RenderButtons(Boolean isEdited)
        {
            if (isEdited)
            {
                EditBtn.IsVisible = true;
                DeleteBtn.IsVisible = true;
            }
            else
            {
                ApplyBtn.IsVisible = true;
            }
        }

        private async void AddComment_Clicked(object sender, EventArgs e)
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                await jobPreviewVM.AddComment();
            }
        }

        private async void EditCurentJob(object sender, EventArgs e)
        {
            if(jobPreviewVM != null)
            {
                Job jobData = new Job()
                {
                    _id = jobPreviewVM.Id,
                    jobTitle = jobPreviewVM.JobTitle,
                    description = jobPreviewVM.Description,
                    location = jobPreviewVM.Location,
                    salary = jobPreviewVM.Salary,
                    postCode = jobPreviewVM.PostCode
                };

                try
                {
                    await Navigation.PushAsync(new EditJobView(jobData));
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        private async void DeleteCurentJob(object sender, EventArgs e)
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                await jobPreviewVM.DeleteJob();
            }
        }
    }
}