using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Services.GeoLocation;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

namespace JobSocialApp.ViewModels
{
    class SearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String keyword = "";
        private String milesAway = "";

        // Elements - for language
        private String pageTitleLbl = "Search Jobs";
        private String keywordPlaceHolder = "Keyword";
        private String milesAwayPlaceHolder = "Miles Away";
        private String searchText = "Search";

        private ObservableCollection<Job> jobs = new ObservableCollection<Job>();
        //private User user = new User();

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

        //public User User
        //{
        //    get => user;
        //    set
        //    {
        //        user = value;
        //        OnPropertyChange();
        //    }
        //}

        public String Keyword
        {
            get => keyword;
            set
            {
                keyword = value;
                OnPropertyChange();
            }
        }

        public String MilesAway
        {
            get => milesAway;
            set
            {
                milesAway = value;
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

        public String KeywordPlaceHolder
        {
            get => keywordPlaceHolder;
            set
            {
                keywordPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String MilesAwayPlaceHolder
        {
            get => milesAwayPlaceHolder;
            set
            {
                milesAwayPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public String SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions

        public SearchViewModel()
        {
            Jobs = new ObservableCollection<Job>();
        }

        public async Task SearchJobs()
        {
            //Get current location
            var location = await GeoLocationManager.GetCurrentPosition();

            double milesAway;
            if(!double.TryParse(MilesAway, out milesAway))
            {
                //@todo an alert here to say miles away not a valid number?
            }

            if (location == null)
            {
                //@todo an alert here to say couldnt get location?
            }

            JobActions crud = new JobActions();
            var jobs = await crud.GetAllJobs();

            //filter so we only pull back jobs in that radius and that match the keyword if there is one
            Jobs = new ObservableCollection<Job>(
                jobs.Where(x =>
                GeoLocationManager.GetDistanceInMiles(location.Longitude, location.Latitude, x.longitude, x.latitude) <= milesAway
                && (string.IsNullOrEmpty(Keyword) || x.jobTitle.Contains(Keyword) || x.description.Contains(Keyword) || x.location.Contains(Keyword) || x.postCode.Contains(Keyword))
                ));
        }

        //public async Task PopulateUser()
        //{
        //    AppContext context = new AppContext();
        //    User = await context.GetCurrentUser();
        //    FullName = string.Format("{0} {1}", User.firstName, User.lastName);
        //    Email = User.email;
        //    JobTitle = User.jobTitle;
        //}

        #endregion
    }
}
