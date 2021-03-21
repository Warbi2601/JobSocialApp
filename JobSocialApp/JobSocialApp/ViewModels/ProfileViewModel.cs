using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Firebase.Storage;
using JobSocialApp.Models;
using JobSocialApp.Services;
using JobSocialApp.Services.FirebaseActions;
using Xamarin.Forms;

namespace JobSocialApp.ViewModels
{
    class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public ProfileViewModel()
        {
            Jobs = new ObservableCollection<Job>();
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                PageTitleLbl = TranslationManager.Instance.getTranslation("EditPersonalDetails");
                FNamePlaceHolder = TranslationManager.Instance.getTranslation("FirstNamePlaceholder");
                SNamePlaceHolder = TranslationManager.Instance.getTranslation("LastNamePlaceholder");
                EmailPlaceHolder = TranslationManager.Instance.getTranslation("EmailPlaceholder");
                PositionPlaceHolder = TranslationManager.Instance.getTranslation("PositionPlaceholder");
                PasswordPlaceHolder = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
            });
        }

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String fullName = "";
        private String firstName = "";
        private String lastName = "";
        private String email = "";
        private String position = "";
        //private String profilePicture = "";
        private String jobTitle = "";
        private ImageSource profilePicture = ImageSource.FromFile("profile.jpg");

        private String updateDetailsBtnText = "Update details";

        // Elements - for language
        private String pageTitleLbl = TranslationManager.Instance.getTranslation("EditPersonalDetails");
        private String fNamePlaceHolder = TranslationManager.Instance.getTranslation("FirstNamePlaceholder");
        private String sNamePlaceHolder = TranslationManager.Instance.getTranslation("LastNamePlaceholder");
        private String emailPlaceHolder = TranslationManager.Instance.getTranslation("EmailPlaceholder");
        private String positionPlaceHolder = TranslationManager.Instance.getTranslation("PositionPlaceholder");
        private String passwordPlaceHolder = TranslationManager.Instance.getTranslation("PasswordPlaceholder");
        private String profileTitleLabel = "";


        private ObservableCollection<Job> jobs = new ObservableCollection<Job>();
        private User user = new User();

        #endregion

        #region Public members

        #region Public variables

        public ObservableCollection<Job> Jobs
        {
            get => jobs;
            set
            {
                jobs = value;
                OnPropertyChange();
            }
        }

        public String JobTitle
        {
            get => jobTitle;
            set
            {
                jobTitle = value;
                OnPropertyChange();
            }
        }

        public User User
        {
            get => user;
            set
            {
                user = value;
                OnPropertyChange();
            }
        }

        public String FullName
        {
            get => fullName;
            set
            {
                fullName = value;
                OnPropertyChange();
            }
        }
        public String FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChange();
            }
        }
        public String LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChange();
            }
        }
        
        public String Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChange();
            }
        }
        
        public String Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChange();
            }
        }

        public ImageSource ProfilePicture
        {
            get => profilePicture;
            set
            {
                profilePicture = value;
                OnPropertyChange();
            }
        }

        public String UpdateDetailsBtnText
        {
            get => updateDetailsBtnText;
            set
            {
                updateDetailsBtnText = value;
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
        
        public String FNamePlaceHolder
        {
            get => fNamePlaceHolder;
            set
            {
                fNamePlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String SNamePlaceHolder
        {
            get => sNamePlaceHolder;
            set
            {
                sNamePlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String EmailPlaceHolder
        {
            get => emailPlaceHolder;
            set
            {
                emailPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String PositionPlaceHolder
        {
            get => positionPlaceHolder;
            set
            {
                positionPlaceHolder = value;
                OnPropertyChange();
            }
        }
        
        public String PasswordPlaceHolder
        {
            get => passwordPlaceHolder;
            set
            {
                passwordPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String ProfileTitleLabel
        {
            get => profileTitleLabel;
            set
            {
                profileTitleLabel = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions


        public async Task PopulateJobs()
        {
            JobActions crud = new JobActions();

            var allJobs = await crud.GetAllJobs();

            bool isCompany = User.company != null;

            if (isCompany)
            {
                ProfileTitleLabel = TranslationManager.Instance.getTranslation("ProfileTitleLabelCompany");
                Jobs = new ObservableCollection<Job>(allJobs.Where(x => x.userID == User._id));
            }
            else
            {
                ProfileTitleLabel = TranslationManager.Instance.getTranslation("ProfileTitleLabelUser");
                Jobs = new ObservableCollection<Job>(allJobs.Where(x => User.jobsAppliedFor != null && User.jobsAppliedFor.Any(y => y == x._id)));
            }
        }
        
        public async Task PopulateUser()
        {
            AppContext context = new AppContext();
            User = await context.GetCurrentUser();
            FullName = string.Format("{0} {1}", User.firstName, User.lastName);
            FirstName = User.firstName;
            LastName = User.lastName;
            Email = User.email;
            JobTitle = User.jobTitle == null ? User.jobTitle : "";
        }

        public async Task GetProfilePicture()
        {
            if (User.hasProfilePic)
            {
                try
                {
                    var uri = await new FirebaseStorage("jobsocialapp-12b52.appspot.com").Child(User._id + ".jpg").GetDownloadUrlAsync();
                    ProfilePicture = ImageSource.FromUri(new Uri(uri));
                } catch(Exception e)
                {
                    Console.WriteLine("Error retreving profile picture from Firebase Storage: " + e);
                }
            } else
            {
                ProfilePicture = ImageSource.FromFile("profile.png");
            }
        }

        public async void captureProfilePicture()
        {
            try
            {
                var fileStream = await MediaManager.Instance.captureImage();
                await new FirebaseStorage("jobsocialapp-12b52.appspot.com").Child(User._id + ".jpg").PutAsync(fileStream);
                User.hasProfilePic = true;
                UserActions crud = new UserActions();
                await crud.UpdateUser(user);
                await GetProfilePicture();
            } catch(Exception e)
            {
                Console.WriteLine("Error when attempting to capture profile picture to Firebase Storage: " + e);
            }
        }

        public async void uploadProfilePicture()
        {
            try
            {
                var fileStream = await MediaManager.Instance.uploadImage();
                if (fileStream == null) return;
                await new FirebaseStorage("jobsocialapp-12b52.appspot.com").Child(User._id + ".jpg").PutAsync(fileStream);
                User.hasProfilePic = true;
                UserActions crud = new UserActions();
                await crud.UpdateUser(user);
                await GetProfilePicture();

            } catch(Exception e)
            {
                Console.WriteLine("Error when attempting to upload profile picture to Firebase Storage: " + e);
            }
        }

        #endregion
    }
}
