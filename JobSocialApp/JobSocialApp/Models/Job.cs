using System;
using System.Collections.Generic;
using System.Text;
using Java.Util;
using JobSocialApp.Models;

namespace JobSocialApp.Models
{
    public class Job
    {
        public string _id { get; set; }

        public string jobTitle { get; set; }
        public string salary { get; set; }

        public string location { get; set; }

        public string description { get; set; }

        public string companyID { get; set; }

        public HashMap ConvertToHashMap()
        {
            HashMap map = new HashMap();

            map.Put("jobTitle", jobTitle);
            map.Put("salary", salary);
            map.Put("location", location);
            map.Put("description", description);
            map.Put("companyID", companyID);

            return map;
        }
    }
}
