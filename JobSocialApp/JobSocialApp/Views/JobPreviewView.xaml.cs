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

        public JobPreviewView(Job jobData)
        {
            InitializeComponent();

            BindingContext = new JobPreviewViewModel();
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            jobObj = jobData;

            RenderButtons();

            Shell.SetTabBarIsVisible(this, false);
        }

        protected async override void OnAppearing()
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                try
                {
                    jobObj = await jobPreviewVM.GetJobFromDb(jobObj);
                    jobPreviewVM.PopulateJobVMData(jobObj);
                }
                catch(Exception ex)
                {

                }
            }
        }

        private async void RenderButtons()
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                AppContext context = new AppContext();
                var currentUser = await context.GetCurrentUser();

                bool isCompany = currentUser.company != null;
                bool isMyJob = jobObj.userID == currentUser._id;

                if (isCompany)
                {
                    ApplyBtn.IsVisible = false;
                    MessageBtn.IsVisible = false;

                    if (isMyJob)
                    {
                        EditBtn.IsVisible = true;
                        DeleteBtn.IsVisible = true;
                    }
                }
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

                    var result = await DisplayAlert("Alert", "Do you really want to delete the post.", "Yes", "No");

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

        private void StartChatClicked(object sender, EventArgs e)
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                jobPreviewVM.MessageEmployer(Navigation);
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

                    if (result.Item1)
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
                    await DisplayAlert("Error", "Email is not supported on this device", "Ok");
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", "An error ocured when trying to apply for the job, please try again", "Ok");
                }
            }
        }

        public async void Handle_Tapped(object sender, EventArgs e)
        {
            await Browser.OpenAsync(new Uri(String.Format("{0}{1}", "http://", websiteLbl.Text)), BrowserLaunchMode.SystemPreferred);
        }
    }
}