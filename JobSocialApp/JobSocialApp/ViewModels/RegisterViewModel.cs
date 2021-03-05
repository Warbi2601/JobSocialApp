using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services;
using JobSocialApp.Views;
using JobSocialApp;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String registerFirstName = "";
        private String registerLastName = "";

        private String registerEmail = "";
        private String registerPassword1 = "";
        private String registerPassword2 = "";

        private DateTime registerdateOfBirth = DateTime.Now;

        // Elements - for language
        private String pageTitleLbl = "Lunch your new career now!";
        private String fNameLbl = "First Name";
        private String sNameLbl = "Last Name";
        private String emailLbl = "Email address";
        private String passwordLbl = "Password";
        private String rePasswordLbl = "Re-Password";
        private String registerBtn = "Register";
        private String loginLbl = "Already have an account? Login";

        #endregion

        #region Public members

        #region Public variables

        public String RegisterFirstName
        {
            get => registerFirstName;
            set
            {
                registerFirstName = value;
                OnPropertyChange();
            }
        }

        public String RegisterLastName
        {
            get => registerLastName;
            set
            {
                registerLastName = value;
                OnPropertyChange();
            }
        }

        public String RegisterEmail
        {
            get => registerEmail;
            set
            {
                registerEmail = value;
                OnPropertyChange();
            }
        }

        public String RegisterPassword1
        {
            get => registerPassword1;
            set
            {
                registerPassword1 = value;
                OnPropertyChange();
            }
        }

        public String RegisterPassword2
        {
            get => registerPassword2;
            set
            {
                registerPassword2 = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Public elements - for language

        public String PageTitleLbl
        {
            get => pageTitleLbl;
            set
            {
                pageTitleLbl = value;
                OnPropertyChange();
            }
        }

        public String FNameLbl
        {
            get => fNameLbl;
            set
            {
                fNameLbl = value;
                OnPropertyChange();
            }
        }

        public String SNameLbl
        {
            get => sNameLbl;
            set
            {
                sNameLbl = value;
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
        
        public String RePasswordLbl
        {
            get => rePasswordLbl;
            set
            {
                rePasswordLbl = value;
                OnPropertyChange();
            }
        }

        public String RegisterBtn
        {
            get => registerBtn;
            set
            {
                registerBtn = value;
                OnPropertyChange();
            }
        }

        public String LoginLbl
        {
            get => loginLbl;
            set
            {
                loginLbl = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions

        public async Task<ToClientRegisterObject> SignInProcedure()
        {
            //ToClientRegisterObject toClient = new ToClientRegisterObject();

            // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
            // need to check if password 1 matches password 2
            try
            {
                var email = RegisterEmail;
                var password = RegisterPassword1;
                var uid = await DependencyService.Get<IFirebaseAuthenticator>().SignUpWithEmailAndPassword(email, password);

                if (uid != string.Empty)
                {
                    Models.User user = new Models.User
                    {
                        _id = uid,
                        firstName = RegisterFirstName,
                        lastName = RegisterLastName,
                        email = RegisterEmail,
                    };

                    UserActions crud = new UserActions();
                    var newUser = await crud.AddUser(user);

                    Routing.RegisterRoute("/main", typeof(AppShell));
                    await Shell.Current.GoToAsync("////home");
                    
                    //Application.Current.MainPage = new HomeView();
                    //await Navigation.PopAsync();
                } // else something went wrong
            }
            catch (Exception ex)
            {
                // Add validation for if the email is already in use
                Console.WriteLine(ex);
            }

            return null;
            //return toClient;
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
