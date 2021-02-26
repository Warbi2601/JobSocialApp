using System;
using System.Collections.Generic;
using System.Text;
using Java.Util;
using JobSocialApp.Models;

namespace JobSocialApp.Models
{
    public class User
    {
        public string _id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public Company? company { get; set; }
        //string Password          { get; set; }
        //string profilePicture { get; set; }

        public HashMap ConvertToHashMap()
        {
            HashMap map = new HashMap();

            map.Put("firstName", firstName);
            map.Put("lastName", lastName);
            map.Put("email", email);
            if(company != null) map.Put("company", company.ConvertToHashMap());

            return map;
        }
    }
}
