using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace JobSocialApp.Models
{
    class TranslationManager
    {
        private static readonly TranslationManager instance = new TranslationManager();
        private static Dictionary<string, string> engMap = new Dictionary<string, string>();
        private static Dictionary<string, string> polMap = new Dictionary<string, string>();
        private static string lang;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static TranslationManager()
        {
            lang = "en";
            engMap.Add("title", "Launch your new career now!");
            engMap.Add("homeTitle", "Welcome!!!");
            polMap.Add("title", "KURWA!!!");
            polMap.Add("homeTitle", "KURWA!!!");
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
            return lang == "pl" ? polMap[key] : engMap[key]; 
        }

        public void changeLang()
        {
            lang = lang == "en" ? lang = "pl" : lang = "en";
            MessagingCenter.Send(this, "langChanged");
        }

    }
}
