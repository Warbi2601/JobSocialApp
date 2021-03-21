using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;
using JobSocialApp.Models;
using Xamarin.Essentials;

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

        protected async override void OnAppearing()
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
                if (string.IsNullOrEmpty(jobPreviewVM.NewComment))
                {
                    await DisplayAlert("Error", "The comment box was empty", "Ok");
                    return;
                }

                await jobPreviewVM.AddComment();
            }
        }

        private async void EditCurentJob(object sender, EventArgs e)
        {
            if (jobPreviewVM != null)
            {
                if (string.IsNullOrEmpty(jobPreviewVM.Id))
                {
                    await DisplayAlert("Error", "No ID for Job", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(jobPreviewVM.JobTitle))
                {
                    await DisplayAlert("Error", "Job Title is empty, please add one and try again", "ok");
                    return;
                }

                if (string.IsNullOrEmpty(jobPreviewVM.Salary))
                {
                    await DisplayAlert("Error", "Salary is empty, please add one and try again", "ok");
                    return;
                }

                if (string.IsNullOrEmpty(jobPreviewVM.Location))
                {
                    await DisplayAlert("Error", "Location is empty, please add one and try again", "ok");
                    return;
                }

                if (string.IsNullOrEmpty(jobPreviewVM.Description))
                {
                    await DisplayAlert("Error", "Description is empty, please add one and try again", "ok");
                    return;
                }

                if (string.IsNullOrEmpty(jobPreviewVM.PostCode))
                {
                    await DisplayAlert("Error", "Postcode is empty, please add one and try again", "ok");
                    return;
                }

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
                try
                {
                    if (string.IsNullOrEmpty(jobPreviewVM.Id))
                    {
                        await DisplayAlert("Error", "No ID for Job", "Ok");
                        return;
                    }

                    var result = await DisplayAlert("Alert", "Do you realy want to delete the post.", "Yes", "No");

                    if (result)
                    {
                        await jobPreviewVM.DeleteJob();
                        await Navigation.PopAsync();
                        await Navigation.PushAsync(new JobsHubView());
                        await DisplayAlert("Success", "Job successfully deleted.", "Ok");
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private async void ApplyForJob_Clicked(object sender, EventArgs e)
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                try
                {
                    var result = await jobPreviewVM.ApplyForJob();

                    if(result.Item1)
                    {
                        await DisplayAlert("Success", "Job successfully applied for.", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Error", result.Item2, "Ok");
                    }

                    //await Navigation.PopAsync();
                    //await Navigation.PushAsync(new JobsHubView());
                }
                catch (FeatureNotSupportedException fbsEx)
                {
                    // 
                    await DisplayAlert("Error", "Email is not supported on this device", "Ok");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "An error ocured when trying to apply for the job, please try again", "Ok");
                }
            }
        }
    }
}