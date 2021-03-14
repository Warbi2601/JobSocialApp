using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;

namespace JobSocialApp.ViewModels
{
    class JobPreviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        public JobPreviewViewModel() { }

        #region Local variables

        private String jobTitle = "";
        private String salary = "";
        private String location = "";
        private String description = "";
        private String id = "";

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
        
        public String Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChange();
            }
        }

        #endregion

        #endregion

        #region Functions

        public async Task PopulateJobVMData(Job jobObj)
        {
            JobTitle = jobObj.jobTitle;
            Salary = jobObj.salary;
            Location = jobObj.location;
            Description = jobObj.description;
            Id = jobObj._id;
        }

        #endregion
    }
}
