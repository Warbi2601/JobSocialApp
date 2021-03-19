using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Models
{
    public class Messages
    {
        public string user1 { get; set; }

        public string user2 { get; set; }

        public List<string> messages { get; set; }

        public void addMessage(string message)
        {
            messages.Add(message);
        }

    }
}
