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

        protected override async void OnAppearing()
        {
            jobPreviewVM = BindingContext as JobPreviewViewModel;

            if (jobPreviewVM != null)
            {
                await jobPreviewVM.PopulateJobVMData(jobObj);
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
    }
}