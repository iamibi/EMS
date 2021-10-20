using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Employee_Management_System.Constants;

namespace Employee_Management_System.Services
{
    public class DatabaseConfigurations
    {
        private static IConfiguration Config;

        public DatabaseConfigurations(IConfiguration Config)
        {
            DatabaseConfigurations.Config = Config;
        }

        public static IMongoDatabase GetCollection()
        {
            MongoClient client = new MongoClient(Config.GetConnectionString(Database.EMSDb));
            return client.GetDatabase(Database.EMSDb);
        }
    }
}
