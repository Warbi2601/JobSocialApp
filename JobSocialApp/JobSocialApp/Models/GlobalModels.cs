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
    }
}
