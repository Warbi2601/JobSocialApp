using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Models
{
    class GlobalModels
    {
        public class ToClientLoginObject
        {
            public Boolean IsSuccessful { get; set; }
            public String MessageHeader { get; set; }
            public String MessageBody { get; set; }
            public String ButtonText { get; set; }
        }
    }
}
