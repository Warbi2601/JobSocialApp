using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services;
using JobSocialApp.Models;
using Xamarin.Forms;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services.GeoLocation;

namespace JobSocialApp.ViewModels
{
    class EditJobViewModel : INotifyPropertyChanged
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
        private String jobId = "";

        // Elements - for language
        private String viewHeading = TranslationManager.Instance.getTranslation("EditJob");
        private String jobTitlePlaceHolder = TranslationManager.Instance.getTranslation("JobTitlePlaceHolder");
        private String salaryPlaceHolder = TranslationManager.Instance.getTranslation("SalaryPlaceHolder");
        private String locationPlaceHolder = TranslationManager.Instance.getTranslation("LocationPlaceHolder");
        private String descriptionPlaceHolder = TranslationManager.Instance.getTranslation("JobDescriptionPlaceHolder");
        private String postcodePlaceHolder = TranslationManager.Instance.getTranslation("PostcodePlaceHolder");

        private String updateBtn = TranslationManager.Instance.getTranslation("UpdateDetailsButton");

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

        public String PostCode
        {
            get => postcode;
            set
            {
                postcode = value;
                OnPropertyChange();
            }
        }

        public String JobId
        {
            get => jobId;
            set
            {
                jobId = value;
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

        public String UpdateBtn
        {
            get => updateBtn;
            set
            {
                updateBtn = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions

        public EditJobViewModel()
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

        public async Task UpdateJobAsync()
        {
            try
            {
                AppContext context = new AppContext();
                User user = await context.GetCurrentUser();

                var res = GeoLocationManager.QueryPostcode(PostCode);

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
                await crud.UpdateJob(job);

                notificationManager.SendNotification("Job Successfully updated", string.Format("{0} - {1} ({2}) - {3}", job.jobTitle, job.location, job.postCode, job.salary));

                //needs some routing here
                //Routing.RegisterRoute("/main", typeof(AppShell));
                //await Shell.Current.GoToAsync("////home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public void PopulateEditJobVMData(Job jobObj)
        {
            JobTitle = jobObj.jobTitle;
            Salary = jobObj.salary;
            Location = jobObj.location;
            Description = jobObj.description;
            JobId = jobObj._id;
            PostCode = jobObj.postCode;
        }

        #endregion
    }
}
