using ServiciiAuto.DataLayer.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ServiciiAuto.DataLayer.Repository
{
    public class EnumsRepository: BaseRepository
    {
        public List<RecordType> GetRecordTypes()
        {
            using(SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetRecordTypes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var recordTypes = new List<RecordType>();
                    while (reader.Read())
                    {
                        var recordType = new RecordType()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            TypeName = reader["TypeName"].ToString(),
                        };
                        recordTypes.Add(recordType);
                    }
                    con.Close();

                    return recordTypes;
                }
            }
        }

        public List<VehicleType> GetVehicleTypes()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetVehicleTypes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var vehicleTypes = new List<VehicleType>();
                    while (reader.Read())
                    {
                        var vehicleType = new VehicleType()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            Name = reader["Name"].ToString(),
                        };
                        vehicleTypes.Add(vehicleType);
                    }
                    con.Close();

                    return vehicleTypes;
                }
            }
        }

        public List<ClientInformedStatus> GetClientInformedStatueses()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetClientInformedStatueses", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var clientInformedStatuses = new List<ClientInformedStatus>();
                    while (reader.Read())
                    {
                        var clientInformedStatuse = new ClientInformedStatus()
                        {
                            Id = int.Parse(reader["Id"].ToString()),
                            StatusName = reader["StatusName"].ToString(),
                        };
                        clientInformedStatuses.Add(clientInformedStatuse);
                    }
                    con.Close();

                    return clientInformedStatuses;
                }
            }
        }
    }
}
