using Plugin.CloudFirestore.Attributes;

namespace JobSocialApp.Models
{
    public class User
    {
        [Id]
        public string _id { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string jobTitle { get; set; }

        public Company company { get; set; }

        public bool hasProfilePic { get; set; }
        //string profilePicture { get; set; }

    }
}
