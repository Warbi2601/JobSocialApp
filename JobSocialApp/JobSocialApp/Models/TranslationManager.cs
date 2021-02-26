using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Models
{
    class TranslationManager
    {
        private static readonly TranslationManager instance = new TranslationManager();
        private static Dictionary<string, string> engMap = new Dictionary<string, string>();
        private static Dictionary<string, string> polMap = new Dictionary<string, string>();


        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static TranslationManager()
        {
            engMap.Add("title", "Launch your new career now!");
            polMap.Add("title", "KURWA!");
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
            return engMap["title"]; 
        }

    }
}
