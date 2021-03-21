using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.ViewModels
{
    class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public HomeViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                Console.WriteLine("lang changed");
                Title = TranslationManager.Instance.getTranslation("Title");
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


        // for testing purposes
        private String title = "";

        private DateTime registerdateOfBirth = DateTime.Now;

        #endregion

        #region Public members

        public void setTitle()
        {
            Title = TranslationManager.Instance.getTranslation("Title");
        }

        public String Title
        {

            get => title;
            set
            {
                title = value;
                OnPropertyChange();
            }
        }

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

        #region Functions

        public async Task<ToClientRegisterObject> SignInProcedure()
        {
            ToClientRegisterObject toClient = new ToClientRegisterObject();

            // need to add validation and exception catches // crashes under certain circumstances (password less than 6)
            // need to check if password 1 matches password 2
            try
            {
                var email = RegisterEmail;
                var password = RegisterPassword1;
                var uid = await DependencyService.Get<IFirebaseAuthenticator>().SignUpWithEmailAndPassword(email, password);

                if (uid != string.Empty)
                {
                    Routing.RegisterRoute("/main", typeof(AppShell));
                    await Shell.Current.GoToAsync("////profile");
                    //Application.Current.MainPage = new HomeView();
                    //await Navigation.PopAsync();
                } // else something went wrong
            }
            catch (Exception ex)
            {
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
