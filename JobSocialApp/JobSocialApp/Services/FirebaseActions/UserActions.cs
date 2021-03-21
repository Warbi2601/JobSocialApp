using JobSocialApp.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    public class UserActions
    {
        private readonly string collectionName = "users";

        public async Task<User> AddUser(User user)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection(collectionName)
                         .Document(user._id)
                         .SetAsync(user);

            var userObj = await GetUser(user._id);

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

        public async Task<ObservableCollection<User>> GetAllUsers()
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                .GetAsync();

            var users = document.ToObjects<User>();
            var userCollection = new ObservableCollection<User>(users);

            return userCollection;
        }
    }
}
