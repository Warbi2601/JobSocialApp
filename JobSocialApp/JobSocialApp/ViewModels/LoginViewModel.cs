using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LoginViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                PageTitleLbl = TranslationManager.Instance.getTranslation("LoginTitle");
                LoginInstructionsLbl = TranslationManager.Instance.getTranslation("LoginInstruction");
                EmailLbl = TranslationManager.Instance.getTranslation("EmailPlaceholder");
                PasswordLbl = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
                SignInBtn = TranslationManager.Instance.getTranslation("SignInButton");
                CreateNewAccountLbl = TranslationManager.Instance.getTranslation("RegisterAccountInfo");
            });
        }

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String loginEmail = "";
        private String loginPassword = "";
        
        private String title = "";

        // Elements - for language
        private String pageTitleLbl = TranslationManager.Instance.getTranslation("LoginTitle");
        private String loginInstructionsLbl = TranslationManager.Instance.getTranslation("LoginInstruction");
        private String emailLbl = TranslationManager.Instance.getTranslation("EmailPlaceholder");
        private String passwordLbl = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
        private String signInBtn = TranslationManager.Instance.getTranslation("SignInButton");
        private String createNewAccountLbl = TranslationManager.Instance.getTranslation("RegisterAccountInfo");

        #endregion

        #region Public members

        #region Public variables

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

        #region Public elements - for language

        public String Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChange();
            }
        }


        public String PageTitleLbl
        {
            get => pageTitleLbl;
            set
            {
                pageTitleLbl = value;
                OnPropertyChange();
            }
        }

        public String LoginInstructionsLbl
        {
            get => loginInstructionsLbl;
            set
            {
                loginInstructionsLbl = value;
                OnPropertyChange();
            }
        }

        public String EmailLbl
        {
            get => emailLbl;
            set
            {
                emailLbl = value;
                OnPropertyChange();
            }
        }

        public String PasswordLbl
        {
            get => passwordLbl;
            set
            {
                passwordLbl = value;
                OnPropertyChange();
            }
        }

        public String SignInBtn
        {
            get => signInBtn;
            set
            {
                signInBtn = value;
                OnPropertyChange();
            }
        }
        
        public String CreateNewAccountLbl
        {
            get => createNewAccountLbl;
            set
            {
                createNewAccountLbl = value;
                OnPropertyChange();
            }
        }

        #endregion

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
                    var user = await DependencyService.Get<IFirebaseAuthenticator>().LoginWithEmailAndPassword(email, password);

                    //check isCompany
                    if(user == null)
                    {
                        toClient.IsSuccessful = false;
                        toClient.MessageHeader = "Login Failed";
                        toClient.MessageBody = "Error logging in";
                        toClient.ButtonText = "Ok";
                        return toClient;
                    }

                    UserActions crud = new UserActions();
                    var loggedInUser = await crud.GetUser(user);

                    // alert the app shell to update navbar.
                    if(loggedInUser.company != null)
                    {
                        MessagingCenter.Send<object, bool>(this, "IsCompany", true);
                    } 
                    else
                    {
                        MessagingCenter.Send<object, bool>(this, "IsCompany", false);
                    }

                    toClient.IsSuccessful = true;
                    Routing.RegisterRoute("/main", typeof(AppShell));
                    await Shell.Current.GoToAsync("////home");
                }
            }
            catch (Exception ex)
            {
                //if (ex.GetType().ToString() == "Firebase.Auth.FirebaseAuthInvalidUserException" || ex.GetType().ToString() == "Firebase.Auth.FirebaseAuthInvalidCredentialsException")
                //{
                    toClient.IsSuccessful = false;
                    toClient.MessageHeader = "Login Failed";
                    toClient.MessageBody = "Email or Password incorrect";
                    toClient.ButtonText = "Ok";
                //}

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

        public void setTitle()
        {
            Title = TranslationManager.Instance.getTranslation("title");
        }
        #endregion
    }
}
