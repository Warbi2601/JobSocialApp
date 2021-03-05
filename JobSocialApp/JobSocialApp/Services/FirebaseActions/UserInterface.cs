using JobSocialApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    public interface UserInterface
    {
        public Task<User> AddUser(User user);
        public Task<User> GetUser(string uid);
    }
}
