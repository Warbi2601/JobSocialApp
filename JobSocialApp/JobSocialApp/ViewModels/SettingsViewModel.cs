using JobSocialApp.Models;
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
        private String selectedTheme;

        // Populate those with resex
        private String languageOptionsLlb = "Language options"; 
        private String chooseLanguageLbl = "Choose your language"; 
        private String englishRadio = "English"; 
        private String polishRadio = "Polish";

        private String themeOptionsLlb = "Theme options";
        private String chooseThemeLbl = "Choose your theme";
        private String lightRadio = "Light";
        private String darkRadio = "Dark";

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
        
        public String ThemeOptionsLbl
        {
            get => themeOptionsLlb;
            set
            {
                themeOptionsLlb = value;
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

        public String SelectedTheme
        {
            get => selectedTheme;
            set
            {
                selectedTheme = value;
                OnPropertyChange();
            }
        }

        public String ThemeOptionsLlb
        {
            get => themeOptionsLlb;
            set
            {
                themeOptionsLlb = value;
                OnPropertyChange();
            }
        }

        public String ChooseThemeLbl
        {
            get => chooseThemeLbl;
            set
            {
                chooseThemeLbl = value;
                OnPropertyChange();
            }
        }

        public String LightRadio
        {
            get => lightRadio;
            set
            {
                lightRadio = value;
                OnPropertyChange();
            }
        }

        public String DarkRadio
        {
            get => darkRadio;
            set
            {
                darkRadio = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Functions

        public void ChangeLanguage(String selectedLanguage)
        {
            SelectedLanguage = selectedLanguage;
        }

        public String ChangeTheme(String selectedLanguage)
        {
            String toClient = "light";

            if (selectedLanguage == "dark")
            {
                ResourcesHelper.SetDarkMode();
                toClient = "dark";
            }
            else if(selectedLanguage == "light")
            {
                ResourcesHelper.SetLightMode();
                toClient = "light";
            }

            return toClient;
        }



        #endregion
    }
}
