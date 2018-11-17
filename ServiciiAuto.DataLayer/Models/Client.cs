using System;
using System.Runtime.Serialization;

namespace ServiciiAuto.DataLayer.Models
{
    [DataContract]
    public class Client
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
    }
}
