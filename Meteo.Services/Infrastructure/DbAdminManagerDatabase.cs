using System;
using MySql.Data.MySqlClient;
namespace Meteo.Services.Infrastructure
{
    public class DbAdminManagerDatabase : DbFactoryManager
    {


        public void CreateDatabaseIfNotExist()
        {
            OpenConnection();
            var queryCreateDataBase = "CREATE DATABASE IF NOT EXISTS `hello`;";
            var createDatabase = new MySqlCommand(queryCreateDataBase, _connection);
            createDatabase.ExecuteNonQuery();
            CloseConnection();
        }

        public void DropDataBase()
        {
            OpenConnection();
            var queryDropDataBase = "DROP DATABASE MeteoDatabse";
            var dropDatabase = new MySqlCommand(queryDropDataBase, _connection);
            dropDatabase.ExecuteNonQuery();
            CloseConnection();
        }
    }
}
