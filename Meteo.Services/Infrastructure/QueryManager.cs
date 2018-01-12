using System;
using System.Linq;
using MySql.Data.MySqlClient;

using Meteo.Services.Models;
namespace Meteo.Services.Infrastructure
{
    public class QueryManager : DbFactoryManager
    {
        public Question GetQuestion(int reciveIdQuestion, User userIfExist)
        {
            OpenConnection();
            reciveIdQuestion = userIfExist.IdQuestion;
            string query = $"SELECT * FROM Questions WHERE IdQuestion = '{reciveIdQuestion}'";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            return question[0];
        }

        public User GetUser(string username)
        {
            OpenConnection();
            string query = $"SELECT * FROM Users WHERE Username = '{username}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user[0];
        }
    }
}