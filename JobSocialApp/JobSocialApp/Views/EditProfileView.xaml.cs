using JobSocialApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static JobSocialApp.Models.GlobalModels;

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
        }

        private void LoadUserDetails()
        {
            editProfileVM = BindingContext as EditProfileViewModel;

            if (editProfileVM != null)
            {
                editProfileVM.LoadUserDetails();
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