using Plugin.CloudFirestore.Attributes;

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

        public string userID { get; set; }

    }
}
