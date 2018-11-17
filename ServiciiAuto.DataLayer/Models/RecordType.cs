using System.Runtime.Serialization;

namespace ServiciiAuto.DataLayer.Models
{
    [DataContract]
    public class RecordType
    {
        public int Id { get; set; }
        public string RecordTypeName { get; set; }
    }
}
