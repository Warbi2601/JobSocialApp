﻿using JobSocialApp.Models;
using JobSocialApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace JobSocialApp.ViewModels
{
    class AppShellViewModel : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
 
        public AppShellViewModel()
        {
        }

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private string jobsHubText = TranslationManager.Instance.getTranslation("JobsHubMenuItem");
        private string editDetailsText = TranslationManager.Instance.getTranslation("EditDetailsMenuItem");
        private string settingsText = TranslationManager.Instance.getTranslation("SettingsMenuItem");
        private string signOutText = TranslationManager.Instance.getTranslation("SignOutMenuItem");
        private string homeText = TranslationManager.Instance.getTranslation("HomeMenuItem");
        private string jobsText = TranslationManager.Instance.getTranslation("JobsMenuItem");
        private string profileText = TranslationManager.Instance.getTranslation("ProfileMenuItem");
        private string messagesText = TranslationManager.Instance.getTranslation("MessagesMenuItem");
        private string jobSearchText = TranslationManager.Instance.getTranslation("JobsSearchText");

        // users jobsHubText or jobSearchText translations
        private string jobsMenutext = TranslationManager.Instance.getTranslation("JobsHubMenuItem");


        #endregion

        #region Public members

        public string JobsMenuText
        {
            get => jobsMenutext;
            set
            {
                jobsMenutext = value;
                OnPropertyChange();
            }
        }

        public string JobsHubText
        {
            get => jobsHubText;
            set
            {
                jobsHubText = value;
                OnPropertyChange();
            }
        }

        public string JobSearchText
        {
            get => jobSearchText;
            set
            {
                jobSearchText = value;
                OnPropertyChange();
            }
        }

        public string EditDetailsText
        {
            get => editDetailsText;
            set
            {
                editDetailsText = value;
                OnPropertyChange();
            }
        }

        public string SettingsText
        {
            get => settingsText;
            set
            {
                settingsText = value;
                OnPropertyChange();
            }
        }
        public string SignOutText
        {
            get => signOutText;
            set
            {
                signOutText = value;
                OnPropertyChange();
            }
        }

        public string HomeText
        {
            get => homeText;
            set
            {
                homeText = value;
                OnPropertyChange();
            }
        }

        public string JobsText
        {
            get => jobsText;
            set
            {
                jobsText = value;
                OnPropertyChange();
            }
        }

        public string ProfileText
        {
            get => profileText;
            set
            {
                profileText = value;
                OnPropertyChange();
            }
        }

        public string MessageText
        {
            get => messagesText;
            set
            {
                messagesText = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Functions

        #endregion


    }


}
