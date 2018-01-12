using System;
using MySql.Data.MySqlClient;
namespace Meteo.Services.Infrastructure
{
    public class CreateDatabase : DbFactoryManager
    {
        public void CreateDatabaseIfNotExist()

        {
            
            OpenConnection();
            var stringCreateDatabase = "CREATE DATABASE IF NOT EXISTS `MeteoDatabase`;";
            var queryCrateDatabase = new MySqlCommand(stringCreateDatabase, _connection);
            queryCrateDatabase.ExecuteNonQuery();
        }

        public void DeleteDatabase ()
        {
            OpenConnection();
            var stringDeleteDatabase = "DROP DATABASE `MeteoDatabase`;";
            var queryDeleteDatabase = new MySqlCommand(stringDeleteDatabase, _connection);
            queryDeleteDatabase.ExecuteNonQuery();

        }
    }
}
