using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciiAuto.DataLayer.Repository
{
    public class RecordRepository: BaseRepository
    {
        public List<Models.Record> GetBrands()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetBrands", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var reader = cmd.ExecuteReader();
                    var brands = new List<Brand>();
                    while (reader.Read())
                    {
                        var brand = new Brand()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            Value = Convert.ToInt32(reader["Value"].ToString()),
                            Name = reader["Name"].ToString()
                        };
                        brands.Add(brand);
                    }
                    con.Close();

                    return brands;
                }
            }
        }
    }
}
