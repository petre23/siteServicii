using System;
using System.Runtime.Serialization;

namespace DataLayer.Models
{
    [DataContract]
    public class User
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int RoleLevel { get; set; }
    }
}
