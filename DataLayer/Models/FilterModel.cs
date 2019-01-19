using System;

namespace DataLayer.Models
{
    public class FilterModel
    {
        public DateTime? ExpirationDateFrom { get; set; }
        public DateTime? ExpirationDateUntil { get; set; }
        public Guid? ClientId { get; set; }
        public int? RecordType { get; set; }
        public string PhoneNumber { get; set; }
        public string CarRegistrationNumber { get; set; }
    }
}
