using JobSocialApp.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace JobSocialApp.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public SettingsViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                LanguageOptionsLbl = TranslationManager.Instance.getTranslation("LanguageOptions");
                ChooseLanguageLbl = TranslationManager.Instance.getTranslation("ChooseLanguage");
                EnglishRadio = TranslationManager.Instance.getTranslation("EnglishTitle");
                PolishRadio = TranslationManager.Instance.getTranslation("PolishTitle");
                ThemeOptionsLbl = TranslationManager.Instance.getTranslation("ThemeOptionsTitle");
                ChooseThemeLbl = TranslationManager.Instance.getTranslation("ChooseTheme");
                LightRadio = TranslationManager.Instance.getTranslation("LightTitle");
                DarkRadio = TranslationManager.Instance.getTranslation("DarkTitle");
            });
        }

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String selectedLanguage = "en"; // default language selected
        private String selectedTheme;

        // Populate those with resex
        private String languageOptionsLlb = TranslationManager.Instance.getTranslation("LanguageOptions"); 
        private String chooseLanguageLbl = TranslationManager.Instance.getTranslation("ChooseLanguage"); 
        private String englishRadio = TranslationManager.Instance.getTranslation("EnglishTitle");
        private String polishRadio = TranslationManager.Instance.getTranslation("PolishTitle");

        private String themeOptionsLlb = TranslationManager.Instance.getTranslation("ThemeOptionsTitle");
        private String chooseThemeLbl = TranslationManager.Instance.getTranslation("ChooseTheme");
        private String lightRadio = TranslationManager.Instance.getTranslation("LightTitle");
        private String darkRadio = TranslationManager.Instance.getTranslation("DarkTitle");

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
            TranslationManager.Instance.changeLang();
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
