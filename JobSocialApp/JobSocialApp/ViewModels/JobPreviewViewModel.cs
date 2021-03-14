﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        private List<Comment> comments { get; set; }

        private String newComment = "";
        private String addCommentBtn = "Add Comment";
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

        public String NewComment
        {
            get => newComment;
            set
            {
                newComment = value;
                OnPropertyChange();
            }
        }

        public String AddCommentBtn
        {
            get => addCommentBtn;
            set
            {
                addCommentBtn = value;
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
        
        #endregion

        #endregion

        #region Functions

        public void PopulateJobVMData(Job jobObj)
        {
            Comments = jobObj.comments.OrderByDescending(x => x.time).ToList();
            JobTitle = jobObj.jobTitle;
            Salary = jobObj.salary;
            Location = jobObj.location;
            Description = jobObj.description;
            Id = jobObj._id;
        }

        public async Task AddComment()
        {
            if(string.IsNullOrEmpty(NewComment))
            {
                //return alert here
            }

            AppContext context = new AppContext();
            var currentUser = await context.GetCurrentUser();

            JobActions crud = new JobActions();

            var job = await crud.GetJob(Id);

            if (job.comments == null) job.comments = new List<Comment>();
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

        #endregion
    }
}
