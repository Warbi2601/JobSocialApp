﻿using System.Resources;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace JobSocialApp.Models
{
    enum Languages
    {
        English, 
        Polish
    }

    class TranslationManager
    {
        private static readonly TranslationManager instance = new TranslationManager();
        private static Languages lang;

        // Explicit static constructor to tell C# compiler
        static TranslationManager()
        {
            lang = Languages.English;
        }

        private TranslationManager()
        {
            
        }

        public static TranslationManager Instance
        {
            get
            {
                return instance;
            }
        }

        public string getTranslation(string key)
        {
            string text = "ERROR";

            switch(lang)
            {
                case Languages.English:
                    text = new ResourceManager(typeof(AppResources.English)).GetString(key);
                    break;
                case Languages.Polish:
                    text = new ResourceManager(typeof(AppResources.Polish)).GetString(key);
                    break;
                default:
                    break;
            }
            return text;
        }

        public void setLanguage()
        {
            var langString = Preferences.Get("lang", "en");
            lang = langString == "en" ? Languages.English : Languages.Polish;
        }

        public void changeLang()
        {
            lang = lang == Languages.English ? lang = Languages.Polish : lang = Languages.English;
            var langString = lang == Languages.English ? "en" : "pl";
            Preferences.Set("lang", langString);
            MessagingCenter.Send(this, "langChanged");
        }

        public bool isEnglishSelected()
        {
            return lang == Languages.English;
        }

    }
}
