using Android;
using Android.Content.Res;
using Java.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Text;
using Xamarin.Forms;
using System.Reflection;
using System.Globalization;

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

        public void changeLang()
        {
            lang = lang == Languages.English ? lang = Languages.Polish : lang = Languages.English;
            MessagingCenter.Send(this, "langChanged");
        }


    }
}
