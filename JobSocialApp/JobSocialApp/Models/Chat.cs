using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Models
{
    public class Chat
    {
        public Chat() 
        {
            messages = new List<Message>();
        }

        public string _id { get; set; }

        public string user1 { get; set; }

        public string user2 { get; set; }

        public List<Message> messages { get; set; }
    }
}
