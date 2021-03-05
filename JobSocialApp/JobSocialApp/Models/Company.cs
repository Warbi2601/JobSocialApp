using Java.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //public List<Job> jobs { get; set; }

        public HashMap ConvertToHashMap()
        {
            HashMap map = new HashMap();

            //List<HashMap> jobsMap = new List<HashMap>();
            //jobs.ForEach(x => jobsMap.Add(x.ConvertToHashMap()));

            //string jobbies = JsonConvert.SerializeObject(jobsMap);

            map.Put("name", name);
            map.Put("description", description);
            map.Put("phone", phone);
            map.Put("email", email);
            map.Put("website", website);
            //map.Put("jobs", jobbies);

            return map;
        }
    }
}
