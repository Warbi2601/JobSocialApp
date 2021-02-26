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

using JobSocialApp.Droid.Services.FirebaseActions;
using Xamarin.Forms;
using Android.Gms.Extensions;
using Java.Util;

[assembly: Dependency(typeof(UserActions))]
namespace JobSocialApp.Droid.Services.FirebaseActions
{
    public class UserActions : JobSocialApp.Services.FirebaseActions.UserInterface
    {
        private readonly string collectionName = "users";

        public async Task<string> AddUser(User user)
        {
            DocumentReference docRef = FirebaseFirestore.Instance.Collection(collectionName).Document(user._id);

            var bb = user.ConvertToHashMap();
            var aaa = await docRef.Set(bb);
            var qwerty = 1;

            return "";
        }
    }
}