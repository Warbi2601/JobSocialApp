using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Services;
using JobSocialApp.Views;
using Xamarin.Forms;
using static JobSocialApp.Models.GlobalModels;

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

        private String jobTitle = "";
        private String salary = "";
        private String location = "";
        private String description = "";

        // Elements - for language
        private String jobTitlePlaceHolder = "Job Title";
        private String salaryPlaceHolder = "Salary";
        private String locationPlaceHolder = "Location";
        private String descriptionPlaceHolder = "Job Description";
        
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

        public async void CreateNewJobAsync()
        {
            // create a new object and populate it with :JobTitle, Salary, Location, Description
        }

        #endregion
    }
}
