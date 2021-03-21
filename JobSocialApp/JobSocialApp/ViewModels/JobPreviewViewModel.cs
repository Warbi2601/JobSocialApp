using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JobSocialApp.Models;
using JobSocialApp.Services.FirebaseActions;
using JobSocialApp.Views;
using Xamarin.Essentials;
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
        private String salaryLabel = "Salary";
        private String salary = "";
        private String locationLabel = "Location";
        private String location = "";
        private String descriptionLabel = "Description";
        private String description = "";
        private String id = "";
        private String userID = "";
        private String postCode = "";
        private String editButtonText = TranslationManager.Instance.getTranslation("EditButtonText");
        private String deleteButtonText = TranslationManager.Instance.getTranslation("DeleteButtonText");
        private String employerId = "";

        //Company data
        private String companyNameLabel = "Company Name";
        private String companyName = "";
        private String companyPhoneLabel = "Phone Number";
        private String companyPhone = "";
        private String companyEmailLabel = "Email Address";
        private String companyEmail = "";
        private String companyWebsiteLabel = "Website";
        private String companyWebsite = "";


        private List<Comment> comments { get; set; }

        private String newComment = "";
        private bool btnApplyEnabled = true;
        private String btnApplyText = "Apply";
        private String commentsPlaceHolder = "Add a comment";

        #endregion

        #region Public members

        #region Public variables

        public List<Comment> Comments
        {
            get => comments;
            set
            {
                comments = value;
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
        
        public String SalaryLabel
        {
            get => salaryLabel;
            set
            {
                salaryLabel = value;
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
        
        public String LocationLabel
        {
            get => locationLabel;
            set
            {
                locationLabel = value;
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
        
        public String DescriptionLabel
        {
            get => descriptionLabel;
            set
            {
                descriptionLabel = value;
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

        public String UserID
        {
            get => userID;
            set
            {
                userID = value;
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

        public String NewComment
        {
            get => newComment;
            set
            {
                newComment = value;
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

        public String CommentsPlaceHolder
        {
            get => commentsPlaceHolder;
            set
            {
                commentsPlaceHolder = value;
                OnPropertyChange();
            }
        }

        public bool BtnApplyEnabled
        {
            get => btnApplyEnabled;
            set
            {
                btnApplyEnabled = value;
                OnPropertyChange();
            }
        }

        public string BtnApplyText
        {
            get => btnApplyText;
            set
            {
                btnApplyText = value;
                OnPropertyChange();
            }
        }

        public String PostCode
        {
            get => postCode;
            set
            {
                postCode = value;
                OnPropertyChange();
            }
        }

        //Company data

        public String CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChange();
            }
        }

        public String CompanyPhone
        {
            get => companyPhone;
            set
            {
                companyPhone = value;
                OnPropertyChange();
            }
        }

        public String CompanyEmail
        {
            get => companyEmail;
            set
            {
                companyEmail = value;
                OnPropertyChange();
            }
        }

        public String CompanyWebsite
        {
            get => companyWebsite;
            set
            {
                companyWebsite = value;
                OnPropertyChange();
            }
        }

        public String CompanyNameLabel
        {
            get => companyNameLabel;
            set
            {
                companyNameLabel = value;
                OnPropertyChange();
            }
        }
        
        public String CompanyPhoneLabel
        {
            get => companyPhoneLabel;
            set
            {
                companyPhoneLabel = value;
                OnPropertyChange();
            }
        }
        
        public String CompanyEmailLabel
        {
            get => companyEmailLabel;
            set
            {
                companyEmailLabel = value;
                OnPropertyChange();
            }
        }

        public String CompanyWebsiteLabel
        {
            get => companyWebsiteLabel;
            set
            {
                companyWebsiteLabel = value;
                OnPropertyChange();
            }
        }
        
        #endregion

        #endregion

        #region Functions

        private async Task<Company> GetCompanyData(String userId)
        {
            Company companyObj = null;
            UserActions crud = new UserActions();
            try
            {
                User user = await crud.GetUser(userId);
                if (user != null)
                {
                    companyObj = user.company;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return companyObj;
        }

        public async void PopulateJobVMData(Job jobObj)
        {
            Company companyData = await GetCompanyData(jobObj.userID);

            AppContext context = new AppContext();
            var currentUser = await context.GetCurrentUser();

            //order comments by newest
            if (jobObj.comments != null)
            {
                Comments = jobObj.comments.OrderByDescending(x => x.time).ToList();
            }

            if(companyData != null)
            {
                CompanyName = companyData.name;
                CompanyPhone = companyData.phone;
                CompanyEmail = companyData.email;
                CompanyWebsite = companyData.website;
            }

            bool alreadyApplied = currentUser.jobsAppliedFor == null ? false : currentUser.jobsAppliedFor.Any(x => x == Id);

            JobTitle = jobObj.jobTitle;
            Salary = jobObj.salary;
            Location = jobObj.location;
            Description = jobObj.description;
            Id = jobObj._id;
            PostCode = jobObj.postCode;
            userID = jobObj.userID;
            BtnApplyEnabled = !alreadyApplied;
            BtnApplyText = !alreadyApplied ? "Apply" : "Already Applied";
        }

        public async Task AddComment()
        {
            AppContext context = new AppContext();
            var currentUser = await context.GetCurrentUser();

            JobActions crud = new JobActions();

            var job = await crud.GetJob(Id);

            if (job.comments == null) job.comments = new List<Comment>();

            //Add a comment
            job.comments.Add(new Comment
            {
                content = NewComment,
                time = DateTime.Now,
                username = string.Format("{0} {1}", currentUser.firstName, currentUser.lastName)
            });

            await crud.UpdateJob(job);

            //refresh comments and reset new comment field
            NewComment = "";
            Comments = job.comments.OrderByDescending(x => x.time).ToList();
        }

        public async Task DeleteJob()
        {
            try
            {
                JobActions crud = new JobActions();
                await crud.DeleteJob(Id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async void MessageEmployer(INavigation navigation)
        {
            try
            {
                AppContext context = new AppContext();
                var currentUser = await context.GetCurrentUser();
                UserActions crud = new UserActions();
                var employer = await crud.GetUser(userID);
                await navigation.PushAsync(new ChatView(currentUser, employer));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Tuple<bool, string>> ApplyForJob()
        {
            AppContext context = new AppContext();
            var user = await context.GetCurrentUser();

            UserActions crud = new UserActions();

            //in case of old data
            if (user.jobsAppliedFor == null) user.jobsAppliedFor = new List<string>();

            //check the user hasnt already applied
            if (user.jobsAppliedFor.Any(x => x == Id))
            {
                return new Tuple<bool, string>(false, "You have already applied for this job");
            }

            //get employer user object
            var employer = await crud.GetUser(userID);

            var recipients = new List<string>();
            recipients.Add(employer.company.email);

            var message = new EmailMessage
            {
                Subject = string.Format("JobSocialApp Application - {0} - {1} - {2}", JobTitle, Location, Salary),
                Body = "Hello, I would like to apply for the advertised job.",
                To = recipients,
            };

            await Email.ComposeAsync(message);

            user.jobsAppliedFor.Add(Id);

            //save the application
            await crud.UpdateUser(user);

            // disable the button so they can't apply again
            BtnApplyEnabled = false;

            return new Tuple<bool, string>(true, "");
        }

        #endregion
    }
}
