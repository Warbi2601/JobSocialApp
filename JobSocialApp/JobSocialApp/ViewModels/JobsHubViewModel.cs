using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;

namespace JobSocialApp.ViewModels
{
    class JobsHubViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private void OnPropertyChange([CallerMemberName] String propertyName = "")
        {
            // Check if not null
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Local variables

        private String updateDetailsBtnText = "Update details";

        // Elements - for language
        private String listTitleLbl = TranslationManager.Instance.getTranslation("JobsCreatedTitle");
        private String addNewJobBtn = TranslationManager.Instance.getTranslation("AddJobButton");

        private ObservableCollection<Job> jobs = new ObservableCollection<Job>();

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

        #endregion

        #region Public elements - for language

        public String ListTitleLbl
        {
            get => listTitleLbl;
            set
            {
                listTitleLbl = value;
                OnPropertyChange();
            }
        }
        
        public String AddNewJobBtn
        {
            get => addNewJobBtn;
            set
            {
                addNewJobBtn = value;
                OnPropertyChange();
            }
        }
        

        #endregion

        #endregion

        #region Functions

        public JobsHubViewModel()
        {
            Jobs = new ObservableCollection<Job>();
        }

        public async Task PopulateJobs()
        {
            AppContext context = new AppContext();
            var user = await context.GetCurrentUser();

            JobActions crud = new JobActions();
            Jobs = await crud.GetAllJobs();
            Jobs = new ObservableCollection<Job>(Jobs.Where(x => x.userID == user._id));
        }
        
        #endregion
    }
}
