using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services;
using JobSocialApp.Models;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services.GeoLocation;

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

        // Elements - for language
        private String jobTitlePlaceHolder = "Job Title";
        private String salaryPlaceHolder = "Salary";
        private String locationPlaceHolder = "Location";
        private String descriptionPlaceHolder = "Job Description";
        private String postcodePlaceHolder = "Postcode";

        private String cancelBtn = "Cancel";
        private String sendBtn = "Send";

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
        #endregion

        #region Public elements - for language

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

        public String CancelBtn
        {
            get => cancelBtn;
            set
            {
                cancelBtn = value;
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
            notificationManager = DependencyService.Get<INotificationManager>();
        }

        public async Task CreateNewJobAsync()
        {
            try
            {
                AppContext context = new AppContext();
                User user = await context.GetCurrentUser();

                var res = GeoLocationManager.QueryPostcode(Postcode);
                
                if(res == null)
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
