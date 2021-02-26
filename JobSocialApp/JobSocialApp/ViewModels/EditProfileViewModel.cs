using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.ViewModels
{
    class EditProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String firstName = "";
        private String lastName = "";
        private String email = "";
        private String position = "Student";
        private String profilePicture = "";

        private String updateDetailsBtnText = "Update details";

        #endregion

        #region Public members
        public String FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChange();
            }
        }

        public String LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChange();
            }
        }
        
        public String Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChange();
            }
        }
        
        public String Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChange();
            }
        }

        public String ProfilePicture
        {
            get => profilePicture;
            set
            {
                profilePicture = value;
                OnPropertyChange();
            }
        }

        public String UpdateDetailsBtnText
        {
            get => updateDetailsBtnText;
            set
            {
                updateDetailsBtnText = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Functions
        
        public async void UpdateUserDetails()
        {
            // update observables with new values
        }
        
        public async void LoadUserDetails()
        {
            // load details from the app or db and update the observables
        }

        #endregion
    }
}
