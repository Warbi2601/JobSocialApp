using JobSocialApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Plugin.CloudFirestore;
using Xamarin.Forms;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services;
using System.Threading.Tasks;

namespace JobSocialApp
{
    public class AppContext
    {
        public async Task<User> GetCurrentUser()
        {
            try
            {
                var uid = DependencyService.Get<IFirebaseAuthenticator>().GetCurrentUserUID();
                UserActions crud = new UserActions();
                return await crud.GetUser(uid);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting user");
                return null;
            }
        }
    }
}
