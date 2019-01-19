using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using DataLayer.Models;

namespace ServiciiAuto.DataLayer.Repository
{
    public class RecordRepository: BaseRepository
    {
        public List<Models.Record> GetAllRecords(FilterModel filters, int pageSize = 50, int pageNumber = 0)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllRecords", con))
                {
                    var from = filters.ExpirationDateFrom.HasValue ? filters.ExpirationDateFrom.Value : DateTime.Parse("01/01/2010");
                    var until = filters.ExpirationDateUntil.HasValue ? filters.ExpirationDateUntil.Value : DateTime.Parse("01/01/2090");

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    cmd.Parameters.AddWithValue("@StartDate", from);
                    cmd.Parameters.AddWithValue("@EndDate", until);
                    cmd.Parameters.AddWithValue("@ClientId", filters.ClientId);
                    cmd.Parameters.AddWithValue("@CarRegistrationNumber", filters.CarRegistrationNumber);
                    cmd.Parameters.AddWithValue("@RecordType", filters.RecordType);
                    cmd.Parameters.AddWithValue("@PhoneNumber", filters.PhoneNumber);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var records = new List<Models.Record>();
                    while (reader.Read())
                    {
                        var record = new Models.Record()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            AdditionalInfo = reader["AdditionalInfo"].ToString(),
                            CarRegistartionNumber = reader["CarRegistartionNumber"].ToString(),
                            ClientId = Guid.Parse(reader["ClientId"].ToString()),
                            ClientName = reader["ClientName"].ToString(),
                            CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                            ExpirationDate = DateTime.Parse(reader["ExpirationDate"].ToString()),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            RecordType = string.IsNullOrEmpty(reader["RecordType"].ToString()) ? (int?)null : int.Parse(reader["RecordType"].ToString()),
                            RecordTypeName = reader["RecordTypeName"].ToString(),
                            VehicleTypeId = string.IsNullOrEmpty(reader["VehicleType"].ToString()) ? (int?)null : int.Parse(reader["VehicleType"].ToString()),
                            VehicleTypeName = reader["VehicleTypeName"].ToString(),
                            ClientInformedStatusName = reader["ClientInformedStatusName"].ToString(),
                            TotalRows = int.Parse(reader["TotalRows"].ToString()),
                        };
                        records.Add(record);
                    }
                    con.Close();

                    return records;
                }
            }
        }

        public Models.Record GetRecordById(Guid recordId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRecordById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@recordId", recordId);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var records = new List<Models.Record>();
                    while (reader.Read())
                    {
                        var record = new Models.Record()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            AdditionalInfo = reader["AdditionalInfo"].ToString(),
                            CarRegistartionNumber = reader["CarRegistartionNumber"].ToString(),
                            ClientId = Guid.Parse(reader["ClientId"].ToString()),
                            ClientName = reader["ClientName"].ToString(),
                            CreationDate = DateTime.Parse(reader["CreationDate"].ToString()),
                            ExpirationDate = DateTime.Parse(reader["ExpirationDate"].ToString()),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            RecordType = string.IsNullOrEmpty(reader["RecordType"].ToString()) ? (int?)null : int.Parse(reader["RecordType"].ToString()),
                            RecordTypeName = reader["RecordTypeName"].ToString(),
                            VehicleTypeId = string.IsNullOrEmpty(reader["VehicleType"].ToString()) ? (int?)null : int.Parse(reader["VehicleType"].ToString()),
                            VehicleTypeName = reader["VehicleTypeName"].ToString(),
                            ClientInformedStatusId = string.IsNullOrEmpty(reader["ClientInformedStatusId"].ToString()) ? (int?)null : int.Parse(reader["ClientInformedStatusId"].ToString()),
                            ClientInformedStatusName = reader["ClientInformedStatusName"].ToString(),
                        };
                        records.Add(record);
                    }
                    con.Close();

                    return records.Any() ? records.FirstOrDefault() : new Models.Record();
                }
            }
        }

        public Guid SaveRecord(Models.Record record)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveRecords", con))
                {
                    var isNew = record.Id == Guid.Empty;
                    record.Id = isNew ? Guid.NewGuid() : record.Id;
                    record.CreationDate = DateTime.Now;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsNew", isNew);
                    cmd.Parameters.AddWithValue("@RecordId", record.Id);
                    cmd.Parameters.AddWithValue("@PhoneNumber", record.PhoneNumber);
                    cmd.Parameters.AddWithValue("@ExpirationDate", record.ExpirationDate);
                    cmd.Parameters.AddWithValue("@CreationDate", record.CreationDate);
                    cmd.Parameters.AddWithValue("@CarRegistartionNumber", record.CarRegistartionNumber);
                    cmd.Parameters.AddWithValue("@ClientId", record.ClientId);
                    cmd.Parameters.AddWithValue("@AdditionalInfo", record.AdditionalInfo);
                    cmd.Parameters.AddWithValue("@Email", record.Email);
                    cmd.Parameters.AddWithValue("@ClientName", record.ClientName);
                    cmd.Parameters.AddWithValue("@RecordType", record.RecordType);
                    cmd.Parameters.AddWithValue("@VehicleTypeId", record.VehicleTypeId);
                    cmd.Parameters.AddWithValue("@ClientInformedStatusId", record.ClientInformedStatusId);
                    cmd.Parameters.AddWithValue("@ModifiedByUser", Guid.Parse("D1839C31-D29F-4381-A105-BF3B82C664EE"));
                    con.Open();
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();

                    return record.Id;
                }
            }
        }

        public int SaveImportedRecord(Models.Record record)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveImportedRecord", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordId", record.Id);
                    cmd.Parameters.AddWithValue("@ExpirationDate", record.ExpirationDate);
                    cmd.Parameters.AddWithValue("@CreationDate", record.CreationDate);
                    cmd.Parameters.AddWithValue("@CarRegistartionNumber", record.CarRegistartionNumber);
                    cmd.Parameters.AddWithValue("@AdditionalInfo", record.AdditionalInfo);
                    cmd.Parameters.AddWithValue("@RecordTypeName", record.RecordTypeName);
                    cmd.Parameters.AddWithValue("@Email", record.Email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", record.PhoneNumber);
                    cmd.Parameters.AddWithValue("@ClientName", record.ClientName);
                    cmd.Parameters.AddWithValue("@ModifiedByUser", Guid.Parse("D1839C31-D29F-4381-A105-BF3B82C664EE"));

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return 0;
        }

        public void DeleteRecord(Guid recordId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordId", recordId);
                    con.Open();
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
