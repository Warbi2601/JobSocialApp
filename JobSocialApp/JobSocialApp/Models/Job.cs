using Plugin.CloudFirestore.Attributes;
using JobSocialApp.Services.GeoLocation;
using System.Collections.Generic;

namespace JobSocialApp.Models
{
    public class Job
    {
        [Id]
        public string _id { get; set; }

        public string jobTitle { get; set; }

        public string salary { get; set; }

        public string location { get; set; }

        public string description { get; set; }

        public string postCode { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public string userID { get; set; }

        public List<Comment> comments { get; set; }

        [Ignored]
        public double distanceAway { get; set; }

        [Ignored]
        public string distanceAwayDisplay { get; set; }

    }
}
