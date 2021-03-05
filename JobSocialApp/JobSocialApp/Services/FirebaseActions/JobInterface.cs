using JobSocialApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JobSocialApp.Services.FirebaseActions
{
    public interface JobInterface
    {
        public void AddJob(Job job);
    }
}
