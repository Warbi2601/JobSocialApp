using JobSocialApp.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    public class UserActions
    {
        private readonly string collectionName = "users";


        public async Task<User> AddUser(User user)
        {
            var newUser = await CrossCloudFirestore.Current
                         .Instance
                         .Collection(collectionName)
                         .AddAsync(user);

            var userObj = await GetUser(newUser.Id);

            return userObj;
        }

        public async Task<User> GetUser(string uid)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                .Document(uid)
                .GetAsync();

            var obj = document.ToObject<User>();
            return obj;
        }

        public async Task UpdateUser(User user)
        {
            await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                .Document(user._id)
                .UpdateAsync(user);
        }
    }
}
