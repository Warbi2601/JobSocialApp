using System;
using System.Collections.Generic;
using System.Text;

namespace JobSocialApp.Models
{
    class GlobalModels
    {
        public class ToClientLogRegBaseObject
        {
            public Boolean IsSuccessful { get; set; }
            public String MessageHeader { get; set; }
            public String MessageBody { get; set; }
            public String ButtonText { get; set; }
        }

        public class ToClientLoginObject : ToClientLogRegBaseObject { }

        public class ToClientRegisterObject : ToClientLogRegBaseObject { }
        //public class ToClientEditedProfileDetails
        //{
        //    public String FirstName { get; set; }
        //    public String SecondName { get; set; }
        //    public String Email { get; set; }
        //    public String Position { get; set; }
        //    public String ProfilePicture { get; set; }
        //}
    }
}
