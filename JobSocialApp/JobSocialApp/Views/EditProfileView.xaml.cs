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


        private void UpdateDetails(object sender, EventArgs e)
        {
            editProfileVM = BindingContext as EditProfileViewModel;

            if (editProfileVM != null)
            {
                editProfileVM.UpdateUserDetails();
            }
        }
    }
}