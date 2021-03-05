using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services
{
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailAndPassword(string email, string password);
        Task<string> SignUpWithEmailAndPassword(string email, string password);
        bool isSignedIn();
        public string GetCurrentUserUID();

        void signOut();
    }
}
