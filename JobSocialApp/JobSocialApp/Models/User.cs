using System;
using System.Collections.Generic;
using System.Text;
using JobSocialApp.Models;

namespace JobSocialApp.Models
{
    public class User
    {
        string _id { get; set; }

        string firstName { get; set; }

        string lastName { get; set; }

        string email { get; set; }

        Company company { get; set; }
            //string Password          { get; set; }
            //string profilePicture { get; set; }
    }
}
