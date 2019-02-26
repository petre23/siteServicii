using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiciiAuto.DataLayer.Repository
{
    public class ClientRepository: BaseRepository
    {
        public List<Models.Client> GetAllClients()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllClients", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var clients = new List<Models.Client>();
                    while (reader.Read())
                    {
                        var client = new Models.Client()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            Email = reader["Email"].ToString(),
                            Name = reader["Name"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                        clients.Add(client);
                    }
                    con.Close();

                    return clients;
                }
            }
        }

        public Models.Client GetClientById(Guid clientId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetClientById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var clients = new List<Models.Client>();
                    while (reader.Read())
                    {
                        var client = new Models.Client()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            Email = reader["Email"].ToString(),
                            Name = reader["Name"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                        clients.Add(client);
                    }
                    con.Close();

                    return clients.Any() ? clients.FirstOrDefault() : new Models.Client();
                }
            }
        }

        public Guid SaveClient(Models.Client client)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SaveClient", con))
                {
                    var isNew = client.Id == Guid.Empty;
                    client.Id = isNew ? Guid.NewGuid() : client.Id;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IsNew", isNew);
                    cmd.Parameters.AddWithValue("@Id", client.Id);
                    cmd.Parameters.AddWithValue("@PhoneNumber", client.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", client.Email);
                    cmd.Parameters.AddWithValue("@Name", client.Name);
                    con.Open();
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();

                    return client.Id;
                }
            }
        }

        public void DeleteClient(Guid clientId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteClientById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@clientId", clientId);
                    con.Open();
                    var reader = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
