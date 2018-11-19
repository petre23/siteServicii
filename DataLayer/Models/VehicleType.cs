using System.Runtime.Serialization;

namespace ServiciiAuto.DataLayer.Models
{
    [DataContract]
    public class VehicleType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
