using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TeamBuildScreen.Server.Resources
{
    [DataContract]
    public class ServerDto
    {
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }
    }
}
