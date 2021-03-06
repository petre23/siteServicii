﻿using System;
using System.Runtime.Serialization;

namespace ServiciiAuto.DataLayer.Models
{
    [DataContract]
    public class Record
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid ClientId { get; set; }
        [DataMember]
        public DateTime CreationDate { get; set; }
        [DataMember]
        public DateTime ExpirationDate { get; set; }
        [DataMember]
        public string ExpirationDateString { get { return ExpirationDate.ToString("dd/MM/yyyy"); } }
        [DataMember]
        public string CarRegistartionNumber { get; set; }
        [DataMember]
        public string AdditionalInfo { get; set; }
        [DataMember]
        public int? RecordType { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string RecordTypeName { get; set; }
        [DataMember]
        public int? VehicleTypeId { get; set; }
        [DataMember]
        public string VehicleTypeName { get; set; }
        [DataMember]
        public int? ClientInformedStatusId { get; set; }
        [DataMember]
        public string ClientInformedStatusName { get; set; }
        [DataMember]
        public int TotalRows { get; set; }
    }
}
