using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;
using Xamarin.Forms;

namespace JobSocialApp.ViewModels
{
    class JobPreviewViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public JobPreviewViewModel()
        {
            MessagingCenter.Subscribe<TranslationManager>(this, "langChanged", (w) =>
            {
                Console.WriteLine("lang changed");
                editButtonText = TranslationManager.Instance.getTranslation("EditButtonText");
                deleteButtonText = TranslationManager.Instance.getTranslation("DeleteButtonText");
            });
        }

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
        private String id = "";
        private String editButtonText = TranslationManager.Instance.getTranslation("EditButtonText");
        private String deleteButtonText = TranslationManager.Instance.getTranslation("DeleteButtonText");
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

        public String EditButtonText
        {
            get => editButtonText;
            set
            {
                editButtonText = value;
                OnPropertyChange();
            }
        }

        public String DeleteButtonText
        {
            get => deleteButtonText;
            set
            {
                deleteButtonText = value;
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
