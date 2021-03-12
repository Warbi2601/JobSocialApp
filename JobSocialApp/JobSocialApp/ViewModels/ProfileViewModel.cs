using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String fullName = "";
        private String email = "";
        private String position = "";
        //private String profilePicture = "";
        private String jobTitle = "";
        private ImageSource profilePicture = ImageSource.From("profile.jpg");

        private String updateDetailsBtnText = "Update details";

        // Elements - for language
        private String pageTitleLbl = "Edit personal details";
        private String fNamePlaceHolder = "First Name";
        private String sNamePlaceHolder = "Last Name";
        private String emailPlaceHolder = "Email Address";
        private String positionPlaceHolder = "Current Position";
        private String passwordPlaceHolder = "Password";

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

        #endregion

        #endregion

        #region Functions

        public ProfileViewModel()
        {
            Jobs = new ObservableCollection<Job>();
        }

        public async Task PopulateJobs()
        {
            JobActions crud = new JobActions();
            Jobs = await crud.GetAllJobs();
        }
        
        public async Task PopulateUser()
        {
            AppContext context = new AppContext();
            User = await context.GetCurrentUser();
            FullName = string.Format("{0} {1}", User.firstName, User.lastName);
            Email = User.email;
            JobTitle = User.jobTitle;
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
