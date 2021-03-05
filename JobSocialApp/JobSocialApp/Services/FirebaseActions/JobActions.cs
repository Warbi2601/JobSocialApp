using JobSocialApp.Models;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    public class JobActions
    {
        private readonly string collectionName = "jobs";

        public async Task<Job> AddJob(Job job)
        {
            var newJob = await CrossCloudFirestore.Current
                         .Instance
                         .Collection(collectionName)
                         .AddAsync(job);

            var jobObj = await GetJob(newJob.Id);

            return jobObj;
        }

        public async Task<Job> GetJob(string uid)
        {
            var document = await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                .Document(uid)
                .GetAsync();

            var obj = document.ToObject<Job>();
            return obj;
        }

        public async Task<ObservableCollection<Job>> GetAllJobs()
        {
            var jobsQuery = await CrossCloudFirestore.Current
                .Instance
                .Collection(collectionName)
                //.Document()
                .GetAsync();

            var jobs = jobsQuery.ToObjects<Job>();
            var jobsCollection = new ObservableCollection<Job>(jobs);
            return jobsCollection;
        }

        //public async Task UpdateJob(Job job)
        //{
        //    await CrossCloudFirestore.Current
        //                   .Instance
        //                   .Collection(collectionName)
        //                   .Document(job._id)
        //                   .UpdateAsync(new { Value = 10 });
        //}

        public async Task DeleteJob(string uid)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .Collection(collectionName)
                         .Document(uid)
                         .DeleteAsync();
        }
    }
}
