using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace JobSocialApp.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String selectedLanguage = "en"; // default language selected

        // Populate those with resex
        private String languageOptionsLlb = "Language options"; 
        private String chooseLanguageLbl = "Choose your language"; 
        private String englishRadio = "English"; 
        private String polishRadio = "Polish"; 

        #endregion

        #region Public members
        public String SelectedLanguage
        {
            get => selectedLanguage;
            set
            {
                selectedLanguage = value;
                OnPropertyChange();
            }
        }

        public String LanguageOptionsLbl
        {
            get => languageOptionsLlb;
            set
            {
                languageOptionsLlb = value;
                OnPropertyChange();
            }
        }
        
        public String EnglishRadio
        {
            get => englishRadio;
            set
            {
                englishRadio = value;
                OnPropertyChange();
            }
        }
        
        public String PolishRadio
        {
            get => polishRadio;
            set
            {
                polishRadio = value;
                OnPropertyChange();
            }
        }

        public String ChooseLanguageLbl
        {
            get => chooseLanguageLbl;
            set
            {
                chooseLanguageLbl = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Functions

        public void ChangeLanguage(String selectedLanguage)
        {
            SelectedLanguage = selectedLanguage;
        }

        #endregion
    }
}
