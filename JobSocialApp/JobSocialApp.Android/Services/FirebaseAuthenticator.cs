using Firebase.Auth;
using JobSocialApp.Droid.Services;
using JobSocialApp.Services;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(FirebaseAuthenticator))]
namespace JobSocialApp.Droid.Services
{
    class FirebaseAuthenticator : IFirebaseAuthenticator
    {
        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
            return user.User.Uid;
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            return user.User.Uid;
        }

        public bool isSignedIn()
        {
            return FirebaseAuth.Instance.CurrentUser != null;
        }

        public void signOut()
        {
            FirebaseAuth.Instance.SignOut();
        }
    }
}