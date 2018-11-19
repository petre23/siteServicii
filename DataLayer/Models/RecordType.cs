using System.Runtime.Serialization;

namespace ServiciiAuto.DataLayer.Models
{
    [DataContract]
    public class RecordType
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string TypeName { get; set; }
    }
}
