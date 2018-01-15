using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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
        public List<User> GetUser(string username)
        {
            OpenConnection();
            string query = $"SELECT * FROM User WHERE Username = '{username}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user;
        }
        public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role)
        {
            OpenConnection();
            string query = $"INSERT INTO User (`Name`, `Username`, `Surname`, `Password`, `Answer`, `Language`, `UnitOfMeasure`, `IdQuestion`, `IdRole`) VALUES ('{nameNewAccount}', '{surnameNewAccount}', '{encryptedPwd}', '{usernameNewAccount}', '{encryptedAnswer}', '{languageNewAccount}', '{measureUnit}', {selectQuestion}, {role})";
            var cmd = new MySqlCommand(query, _connection);
            var insert = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
        }
        public List<User> GetUSerIfExist(string username, string psw)
        {
            OpenConnection();
            string query = $"SELECT Username FROM User WHERE Username = '{username}' AND Psw ={psw}";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user;
        }
        public List<User> AutentiationWithAnswer(string answer, string username)
        {
            OpenConnection();
            string query = $" SELECT Username FROM User WHERE Username = '{username} AND Answer = {answer}";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user;
        }
        public void QueryForUpdatePsw(string psw, string username)
        {
            OpenConnection();
            string query = $" UPDATE User SET Psw = {psw} WHERE Username = {username}";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return;
        }
        public List<Question> AllQuestion()
        {
            OpenConnection();
            string query = $"SELECT * FROM Question ";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            return question;
        }
        public List<Role> AllRoles()
        {
            OpenConnection();
            string query = $"SELECT * FROM Role ";
            var cmd = new MySqlCommand(query, _connection);
            var role = cmd.ExecuteReader().DataReaderMapToList<Role>();
            CloseConnection();
            return role;
        }
    }
}