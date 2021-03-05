using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Models
{
    public class Company
    {
        public string _id { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string phone { get; set; }

        public string email { get; set; }

        public string website { get; set; }

        public HashMap ConvertToHashMap()
        {
            HashMap map = new HashMap();

            map.Put("name", name);
            map.Put("description", description);
            map.Put("phone", phone);
            map.Put("email", email);
            map.Put("website", website);

            return map;
        }
    }
}
