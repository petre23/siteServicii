using System.Runtime.Serialization;

namespace ServiciiAuto.DataLayer.Models
{
    [DataContract]
    public class ClientInformedStatus
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string StatusName { get; set; }
    }
}
