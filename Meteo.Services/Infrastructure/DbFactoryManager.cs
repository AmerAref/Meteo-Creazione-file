using System;
using MySql.Data.MySqlClient;

namespace Meteo.Services.Infrastructure
{
    public class DbFactoryManager
    {
        private MySqlConnection _connection;
        private string _server;
        private string _database;
        private string _uid;

        private string _password;
        //Constructor
        public DbFactoryManager()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            _server = "localhost";
            _database = "MeteoDatabase";
            _uid = "root";
            _password = "";
            // TODO: da recuperare dai settings
            var connectionString = $"SERVER={_server}; DATABASE={_database}; UID={_uid}; PASSWORD= {_password};";
            _connection = new MySqlConnection(connectionString);
        }

        public bool OpenConnection()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 0)
                    Console.WriteLine("Cannot connect to server.  Contact administrator");
                else if (ex.Number == 1045)
                {
                    Console.WriteLine("Invalid username/password, please try again");
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                _connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}