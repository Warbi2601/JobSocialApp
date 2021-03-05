using JobSocialApp.Models;
using Firebase.Firestore;
using System.Threading.Tasks;
using JobSocialApp.Droid.Services.FirebaseActions;
using Xamarin.Forms;
using Android.Gms.Extensions;
using Java.Util;
using JobSocialApp.Droid.Util;
using Newtonsoft.Json;

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

            //newUser.Get()

            var userObj = Utility.FirebaseDocumentToObject<User>(newUser);
            return userObj;
        }

        public async Task<User> GetUser(string uid)
        {
            DocumentSnapshot user = (DocumentSnapshot)await FirebaseFirestore.Instance.Collection(collectionName).Document(uid).Get();

            if (!user.Exists())
            {
                //there has been an error getting it, return something here
            }

            return Utility.FirebaseDocumentToObject<User>(user);
        }
        
    }
}