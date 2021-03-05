using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String loginEmail = "";
        private String loginPassword = "";

        #endregion

        #region Public members
        public String LoginEmail
        {
            get => loginEmail;
            set
            {
                loginEmail = value;
                OnPropertyChange();
            }
        }

        public String LoginPassword
        {
            get => loginPassword;
            set
            {
                loginPassword = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Functions
        
        public async Task<ToClientLoginObject> SignInProcedure()
        {
            ToClientLoginObject toClient = new ToClientLoginObject();

            try
            {
                var email = LoginEmail; // valid email
                var password = LoginPassword; // min 6 chars

                bool validEmail = IsValidEmail(email);
                bool validPass = password.Length >= 6;

                if (!validEmail || !validPass)
                {
                    if (!validEmail)
                    {
                        //await DisplayAlert("Login Failed", "Email is not valid", "Ok");
                        toClient.IsSuccessful = false;
                        toClient.MessageHeader = "Login Failed";
                        toClient.MessageBody = "Email is not valid";
                        toClient.ButtonText = "Ok";
                    }
                    else if (!validPass)
                    {
                        //await DisplayAlert("Login Failed", "Password does not meet requirements", "Ok");
                        toClient.IsSuccessful = false;
                        toClient.MessageHeader = "Login Failed";
                        toClient.MessageBody = "Password does not meet requirements";
                        toClient.ButtonText = "Ok";
                    }
                }
                else
                {
                    var a = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailAndPassword(email, password);
                    
                    //check isCompany

                    toClient.IsSuccessful = true;

                    Routing.RegisterRoute("/main", typeof(AppShell));
                    await Shell.Current.GoToAsync("////home");
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == "Firebase.Auth.FirebaseAuthInvalidUserException")
                {
                    //await DisplayAlert("Login Failed", "Email or Password incorrect", "Ok");
                    toClient.IsSuccessful = false;
                    toClient.MessageHeader = "Login Failed";
                    toClient.MessageBody = "Password does not meet requirements";
                    toClient.ButtonText = "Ok";
                }
                Console.WriteLine(ex);
            }

            return toClient;
        }


        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
