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
using Newtonsoft.Json;
using GoogleGson;

[assembly: Dependency(typeof(UserActions))]
namespace JobSocialApp.Droid.Services.FirebaseActions
{
    public class UserActions : JobSocialApp.Services.FirebaseActions.UserInterface
    {
        private readonly string collectionName = "users";

        public async Task<User> AddUser(User user)
        {
            await FirebaseFirestore.Instance.Collection(collectionName).Document(user._id).Set(user.ConvertToHashMap());
            DocumentSnapshot newUser = (DocumentSnapshot)await FirebaseFirestore.Instance.Collection(collectionName).Document(user._id).Get();
            
            if(!newUser.Exists())
            {
                //there has been an error adding it, return something here
            }

            return FirebaseDocumentToObject<User>(newUser);
        }

        public T FirebaseDocumentToObject<T>(DocumentSnapshot snapshot)
        {
            //Get the fields for the document
            var properties = snapshot.GetData(DocumentSnapshot.ServerTimestampBehavior.Estimate);

            //Serialize it to Json and then back to our C# class
            Gson gson = new Gson();
            var serialized = gson.ToJson((Java.Lang.Object)properties);
            var deserialized = JsonConvert.DeserializeObject<T>(serialized);
            return deserialized;
        }
    }
}