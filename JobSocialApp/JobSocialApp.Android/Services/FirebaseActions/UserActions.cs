using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JobSocialApp.Models;
using Firebase.Firestore;
using System.Threading.Tasks;

using JobSocialApp.Droid.Services;
using JobSocialApp.Services;
using Xamarin.Forms;
using Android.Gms.Extensions;

namespace JobSocialApp.Droid.Services.FirebaseActions
{
    public class UserActions
    {
        private readonly string collectionName = "users";

        public async Task<string> AddUser(User user)
        {
            DocumentReference docRef = FirebaseFirestore.Instance.Collection(collectionName).Document(user._id);

            var a = await docRef.Set(user.ConvertToHashMap());

            //var newUser = await FirebaseFirestore.Instance.Collection(collectionName).Document(user._id);

            return "";
        }
    }
}