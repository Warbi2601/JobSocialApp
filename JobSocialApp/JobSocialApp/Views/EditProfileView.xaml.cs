using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using JobSocialApp.ViewModels;

namespace JobSocialApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfileView : ContentPage
    {
        private EditProfileViewModel editProfileVM = null;
     
        public EditProfileView()
        {
            InitializeComponent();

            BindingContext = new EditProfileViewModel();
            editProfileVM = BindingContext as EditProfileViewModel;

            Shell.SetTabBarIsVisible(this, false);
        }


        protected override async void OnAppearing()
        {
            if (editProfileVM != null)
            {
                await editProfileVM.LoadUserDetails();
            }
        }


        private async void UpdateDetails(object sender, EventArgs e)
        {
            editProfileVM = BindingContext as EditProfileViewModel;

            if (editProfileVM != null)
            {
                if(string.IsNullOrEmpty(editProfileVM.FirstName))
                {
                    await DisplayAlert("Error", "First Name is required", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(editProfileVM.LastName))
                {
                    await DisplayAlert("Error", "Last Name is required", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(editProfileVM.Position))
                {
                    await DisplayAlert("Error", "Job Title is required", "Ok");
                    return;
                }

                editProfileVM.UpdateUserDetails();
            }
        }
    }
}