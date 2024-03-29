﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services;
using JobSocialApp.Services.FirebaseActions;
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

        private INotificationManager notificationManager;

        private String firstName = "";
        private String lastName = "";
        private String position = "";
        private String profilePicture = "";
        private User user = null;

        private String updateDetailsBtnText = TranslationManager.Instance.getTranslation("UpdateDetailsButton");

        // Elements - for language
        private String pageTitleLbl = TranslationManager.Instance.getTranslation("EditProfileTitle");
        private String fNamePlaceHolder = TranslationManager.Instance.getTranslation("FirstNamePlaceholder");
        private String sNamePlaceHolder = TranslationManager.Instance.getTranslation("LastNamePlaceholder");
        private String positionPlaceHolder = TranslationManager.Instance.getTranslation("PositionPlaceholder");
        private String passwordPlaceHolder = TranslationManager.Instance.getTranslation("PasswordPlaceholder");

        #endregion

        #region Public members

        #region Public variables
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

        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Public elements - for language

        public String PageTitleLbl
        {
            get => pageTitleLbl;
            set
            {
                pageTitleLbl = value;
                OnPropertyChange();
            }
        }
        
        public String FNamePlaceHolder
        {
            get => fNamePlaceHolder;
            set
            {
                fNamePlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String SNamePlaceHolder
        {
            get => sNamePlaceHolder;
            set
            {
                sNamePlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String PositionPlaceHolder
        {
            get => positionPlaceHolder;
            set
            {
                positionPlaceHolder = value;
                OnPropertyChange();
            }
        }
        
        public String PasswordPlaceHolder
        {
            get => passwordPlaceHolder;
            set
            {
                passwordPlaceHolder = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions

        public EditProfileViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                Console.WriteLine("lang changed");
                updateDetailsBtnText = TranslationManager.Instance.getTranslation("UpdateDetailsButton");
                pageTitleLbl = TranslationManager.Instance.getTranslation("EditProfileTitle");
                fNamePlaceHolder = TranslationManager.Instance.getTranslation("FirstNamePlaceholder");
                sNamePlaceHolder = TranslationManager.Instance.getTranslation("LastNamePlaceholder");
                positionPlaceHolder = TranslationManager.Instance.getTranslation("PositionPlaceholder");
                passwordPlaceHolder = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
            });

            notificationManager = DependencyService.Get<INotificationManager>();
        }

        public async void UpdateUserDetails()
        {
            try
            {
                AppContext context = new AppContext();
                User user = await context.GetCurrentUser();

                if (user != null)
                {
                    user.firstName = FirstName;
                    user.lastName = LastName;
                    user.jobTitle = Position;

                    UserActions crud = new UserActions();
                    await crud.UpdateUser(user);

                    Routing.RegisterRoute("/main", typeof(AppShell));
                    await Shell.Current.GoToAsync("////profile");


                    //Application.Current.MainPage = new HomeView();
                    //await Navigation.PopAsync();
                } // else something went wrong
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task LoadUserDetails()
        {
            AppContext context = new AppContext();
            User = await context.GetCurrentUser();
            FirstName = User.firstName;
            LastName = User.lastName;
            Position = User.jobTitle;
        }

        #endregion
    }
}
