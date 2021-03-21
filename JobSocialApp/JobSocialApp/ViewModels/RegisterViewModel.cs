using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services;
using JobSocialApp.Views;
using JobSocialApp.Models;
using JobSocialApp;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;
using JobSocialApp.Models;

namespace JobSocialApp.ViewModels
{
    class RegisterViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RegisterViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                PageTitleLbl = TranslationManager.Instance.getTranslation("RegisterTitle");
                FNameLbl = TranslationManager.Instance.getTranslation("FirstNamePlaceholder");
                SNameLbl = TranslationManager.Instance.getTranslation("LastNamePlaceholder");
                EmailLbl = TranslationManager.Instance.getTranslation("EmailPlaceholder");
                PasswordLbl = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
                RePasswordLbl = TranslationManager.Instance.getTranslation("RePasswordPlaceholder");
                RegisterBtn = TranslationManager.Instance.getTranslation("RegisterButton");
                LoginLbl = TranslationManager.Instance.getTranslation("LoginAccountInfo");
            });
        }

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

        private String registerJobTitle = "";

        private bool isCompany = false;
        private String companyName = "";
        private String companyEmail = "";
        private String companyWebsite = "";
        private String companyPhone = "";


        private DateTime registerdateOfBirth = DateTime.Now;

        // Elements - for language
        private String pageTitleLbl = TranslationManager.Instance.getTranslation("RegisterTitle");
        private String fNameLbl = TranslationManager.Instance.getTranslation("FirstNamePlaceholder");
        private String sNameLbl = TranslationManager.Instance.getTranslation("LastNamePlaceholder");
        private String emailLbl = TranslationManager.Instance.getTranslation("EmailPlaceholder");
        private String passwordLbl = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
        private String rePasswordLbl = TranslationManager.Instance.getTranslation("RePasswordPlaceholder");
        private String registerBtn = TranslationManager.Instance.getTranslation("RegisterButton");
        private String loginLbl = TranslationManager.Instance.getTranslation("LoginAccountInfo");

        private String isCompanyLbl = "Company?";
        private String companyNameLbl = "Company Name";
        private String companyEmailLbl = "Company Email";
        private String companyWebsiteLbl = "Company Website";
        private String companyPhoneLbl = "Company Phone Number";

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

        public String RegisterJobTitle
        {
            get => registerJobTitle;
            set
            {
                registerJobTitle = value;
                OnPropertyChange();
            }
        }

        public bool IsCompany
        {
            get => isCompany;
            set
            {
                isCompany = value;
                OnPropertyChange();
            }
        }

        public String CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChange();
            }
        }

        public String CompanyEmail
        {
            get => companyEmail;
            set
            {
                companyEmail = value;
                OnPropertyChange();
            }
        }

        public String CompanyWebsite
        {
            get => companyWebsite;
            set
            {
                companyWebsite = value;
                OnPropertyChange();
            }
        }

        public String CompanyPhone
        {
            get => companyPhone;
            set
            {
                companyPhone = value;
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

        public String JobTitleLbl
        {
            get => jobTitleLbl;
            set
            {
                jobTitleLbl = value;
                OnPropertyChange();
            }
        }

        public String IsCompanyLbl
        {
            get => isCompanyLbl;
            set
            {
                isCompanyLbl = value;
                OnPropertyChange();
            }
        }

        public String CompanyNameLbl
        {
            get => companyNameLbl;
            set
            {
                companyNameLbl = value;
                OnPropertyChange();
            }
        }

        public String CompanyEmailLbl
        {
            get => companyEmailLbl;
            set
            {
                companyEmailLbl = value;
                OnPropertyChange();
            }
        }

        public String CompanyWebsiteLbl
        {
            get => companyWebsiteLbl;
            set
            {
                companyWebsiteLbl = value;
                OnPropertyChange();
            }
        }

        public String CompanyPhoneLbl
        {
            get => companyPhoneLbl;
            set
            {
                companyPhoneLbl = value;
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
            //check email is valid
            if (!IsValidEmail(RegisterEmail))
            {
                return new ToClientRegisterObject
                {
                    ButtonText = "Ok",
                    IsSuccessful = false,
                    MessageBody = "Email isn't a valid email",
                    MessageHeader = "Error"
                };
            }

            try
            {
                var email = RegisterEmail;
                var password = RegisterPassword1;

                //Sign them up on firebase
                var uid = await DependencyService.Get<IFirebaseAuthenticator>().SignUpWithEmailAndPassword(email, password);

                if (uid != string.Empty)
                {
                    //if the user is signing up as a company, create that object
                    Company company = IsCompany ? company = new Company
                    {
                        email = CompanyEmail,
                        name = CompanyName,
                        phone = CompanyPhone,
                        website = CompanyWebsite
                    } : null;

                    User user = new User
                    {
                        _id = uid,
                        firstName = RegisterFirstName,
                        lastName = RegisterLastName,
                        email = RegisterEmail,
                        jobTitle = RegisterJobTitle,
                        company = company
                    };

                    //add them to our users collection
                    UserActions crud = new UserActions();
                    var newUser = await crud.AddUser(user);

                    //redirect to main app
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
