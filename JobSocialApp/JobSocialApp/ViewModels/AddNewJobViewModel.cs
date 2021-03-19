using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services;
using JobSocialApp.Models;
using Xamarin.Forms;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services.GeoLocation;
using Plugin.FacebookClient;

namespace JobSocialApp.ViewModels
{
    class AddNewJobViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private INotificationManager notificationManager;

        private String jobTitle = "";
        private String salary = "";
        private String location = "";
        private String description = "";
        private String postcode = "";
        private bool postToFB = false;

        // Elements - for language
        private String viewHeading = TranslationManager.Instance.getTranslation("CreateNewOpp");
        private String jobTitlePlaceHolder = TranslationManager.Instance.getTranslation("JobTitlePlaceHolder");
        private String salaryPlaceHolder = TranslationManager.Instance.getTranslation("SalaryPlaceHolder");
        private String locationPlaceHolder = TranslationManager.Instance.getTranslation("LocationPlaceHolder");
        private String descriptionPlaceHolder = TranslationManager.Instance.getTranslation("JobDescriptionPlaceHolder");
        private String postcodePlaceHolder = TranslationManager.Instance.getTranslation("PostcodePlaceHolder");
        private String postToFBPlaceHolder = TranslationManager.Instance.getTranslation("PostcodePlaceHolder");

        private String sendBtn = TranslationManager.Instance.getTranslation("SubmitButtonText");

        #endregion

        #region Public members

        #region Public variables
        public String JobTitle
        {
            get => jobTitle;
            set
            {
                jobTitle = value;
                OnPropertyChange();
            }
        }

        public String Salary
        {
            get => salary;
            set
            {
                salary = value;
                OnPropertyChange();
            }
        }

        public String Location
        {
            get => location;
            set
            {
                location = value;
                OnPropertyChange();
            }
        }

        public String Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChange();
            }
        }

        public String Postcode
        {
            get => postcode;
            set
            {
                postcode = value;
                OnPropertyChange();
            }
        }

        public bool PostToFB
        {
            get => postToFB;
            set
            {
                postToFB = value;
                OnPropertyChange();
            }
        }

        #endregion

        #region Public elements - for language

        public String ViewHeading
        {
            get => viewHeading;
            set
            {
                viewHeading = value;
                OnPropertyChange();
            }
        }

        public String JobTitlePlaceHolder
        {
            get => jobTitlePlaceHolder;
            set
            {
                jobTitlePlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String SalaryPlaceHolder
        {
            get => salaryPlaceHolder;
            set
            {
                salaryPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String LocationPlaceHolder
        {
            get => locationPlaceHolder;
            set
            {
                locationPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String DescriptionPlaceHolder
        {
            get => descriptionPlaceHolder;
            set
            {
                descriptionPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String PostcodePlaceholder
        {
            get => postcodePlaceHolder;
            set
            {
                postcodePlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String SendBtn
        {
            get => sendBtn;
            set
            {
                sendBtn = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions

        public AddNewJobViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                Console.WriteLine("lang changed");
                viewHeading = TranslationManager.Instance.getTranslation("CreateNewOpp");
                jobTitlePlaceHolder = TranslationManager.Instance.getTranslation("JobTitlePlaceHolder");
                salaryPlaceHolder = TranslationManager.Instance.getTranslation("SalaryPlaceHolder");
                locationPlaceHolder = TranslationManager.Instance.getTranslation("LocationPlaceHolder");
                descriptionPlaceHolder = TranslationManager.Instance.getTranslation("JobDescriptionPlaceHolder");
                postcodePlaceHolder = TranslationManager.Instance.getTranslation("PostcodePlaceHolder");
            });
            notificationManager = DependencyService.Get<INotificationManager>();
        }

        public async Task CreateNewJobAsync()
        {
            try
            {
                AppContext context = new AppContext();
                User user = await context.GetCurrentUser();

                var res = GeoLocationManager.QueryPostcode(Postcode);

                if (res == null)
                {
                    // alert that postcode isnt valid
                    //await DisplayAlert
                }

                var latitude = res.Latitude;
                var longitude = res.Longitude;
                var formattedPostcode = res.Postcode; //get nicely formatted postcode

                Job job = new Job
                {
                    jobTitle = JobTitle,
                    salary = Salary,
                    location = Location,
                    postCode = formattedPostcode,
                    longitude = longitude,
                    latitude = latitude,
                    description = Description,
                    userID = user._id
                };


                JobActions crud = new JobActions();
                Job newJob = await crud.AddJob(job);

                if (PostToFB)
                {
                    await CrossFacebookClient.Current.LoginAsync(new string[] { "email" });

                    string quote = string.Format("{0} - £{1} - {2}, {3}", job.jobTitle, job.salary, job.location, job.postCode);

                    FacebookShareLinkContent linkContent = new FacebookShareLinkContent(quote, new Uri("https://play.google.com/store"));
                    var ret = await CrossFacebookClient.Current.ShareAsync(linkContent);
                }

                notificationManager.SendNotification("Job Successfully Posted", string.Format("{0} - {1} ({2}) - {3}", job.jobTitle, job.location, job.postCode, job.salary));

                //needs some routing here
                //Routing.RegisterRoute("/main", typeof(AppShell));
                //await Shell.Current.GoToAsync("////home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion
    }
}
