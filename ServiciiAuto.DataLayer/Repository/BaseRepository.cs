using System.Configuration;

namespace ServiciiAuto.DataLayer.Repository
{
    public class BaseRepository
    {
        public string ConnectionString { get { return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString; } }
    }
}
