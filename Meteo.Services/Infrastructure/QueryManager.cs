using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.Services.Infrastructure
{
    public class QueryManager : DbFactoryManager
    {
        public Question GetQuestion(int IdQuestion)
        {
            OpenConnection();
            string query = $"SELECT * FROM Question WHERE IdQuestion = '{IdQuestion}'";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            if (question.Any())
            { return question[0]; }
            else
            { return null; }
        }
        public User GetUser(string username)
        {
            OpenConnection();
            string query = $"SELECT * FROM User WHERE Username = '{username}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            if (user.Any())
            { return user[0]; }
            else
            { return null; }
        }
        public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role)
        {
            OpenConnection();
            string query = $"INSERT INTO User (`Name`, `Surname`, `Username`, `Password`, `IdQuestion`, `Answer`, `Language`, `UnitOfMeasure`, `IdRole`) VALUES ('{nameNewAccount}', '{surnameNewAccount}','{usernameNewAccount}', '{encryptedPwd}', {selectQuestion}, '{encryptedAnswer}', '{languageNewAccount}', '{measureUnit}',  {role})";
            var cmd = new MySqlCommand(query, _connection);
            cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
        }
        public User GetUserIfExist(string username, string psw)
        {
            OpenConnection();
            string query = $"SELECT * FROM User WHERE Username = '{username}' AND Password = '{psw}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            if (user.Any())
            {
                return user[0];
            }
            else
                return null;

        }
        public User AutentiationWithAnswer(string answer, string username)
        {
            OpenConnection();
            string query = $" SELECT * FROM User WHERE Username = '{username}' AND Answer = '{answer}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            if (user.Any())
            {
                return user[0];
            }
            else
                return null;
        }
        public void QueryForUpdatePsw(string psw, string username)
        {
            OpenConnection();
            string query = $" UPDATE User SET Password = '{psw}' WHERE Username = '{username}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return;
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
        public void InsertOneDayForecast(OpenWeatherMap.Models.OneDayForecast jsonObj)
        {
            OpenConnection();
            var parameters = jsonObj.Parameters;
            string query = $"INSERT INTO `OneDayForecast`(`Pressure`, `Temp`, `Humidity`, `TempMin`, `TempMax`) VALUES ('{parameters.Pressure}', '{parameters.Temp}', '{parameters.Humidity}', '{parameters.TempMin}', '{parameters.TempMax}');";
            var cmd = new MySqlCommand(query, _connection);
            var oenDayForecastData = cmd.ExecuteReader().DataReaderMapToList<Models.OneDayForecast>();
            CloseConnection();
        }
        public List<Question> AllQuestionsEN()
        {
            OpenConnection();
            string query = $"SELECT * FROM Question WHERE Language = 'en'  ";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            return question;
        }
        public List<Question> AllQuestionsIT()
        {
            OpenConnection();
            string query = $"SELECT * FROM Question WHERE Language = 'it'  ";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            return question;
        }
        public void InsertDataMaster(string meteoChoiceDb, int idUserMaster)
        {
            OpenConnection();
            DateTime masterDate = DateTime.Now;
            string format = "yyyy-MM-dd hh:mm:ss";
            string str = masterDate.ToString(format);
            string query = $"INSERT INTO `Master` (`Choice5DayOrNow`, `DateOfRequist`, `IdUser`) VALUES ('{meteoChoiceDb}', '{str}', '{idUserMaster}');";
            var cmd = new MySqlCommand(query, _connection);
            var meteoMasterData = cmd.ExecuteReader().DataReaderMapToList<Models.Master>();
            CloseConnection();
        }

        //Query per eseguite da utente con ruolo Admin
        public List<User> GetAllUsers()
        {
            OpenConnection();
            string query = "SELECT * FROM User";
            var cmd = new MySqlCommand(query, _connection);
            var users = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return users;
        }
        public List<Master> GetAllMasterRecords()
        {
            OpenConnection();
            string query = "SELECT * FROM Master";
            var cmd = new MySqlCommand(query, _connection);
            var records = cmd.ExecuteReader().DataReaderMapToList<Master>();
            CloseConnection();
            return records;
        }

        public void DeleteUser(string name, string surname, string username)
        {
            OpenConnection();
            string query = $"DELETE FROM `User` WHERE `Name` = '{name}' AND `Surname` = '{surname}' AND `Username` = '{username}';";
            var cmd = new MySqlCommand(query, _connection);
            var delete = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
        }
        public void QueryForUpdateRole(string username, int role)
        {
            OpenConnection();
            string query = $" UPDATE User SET IdRole = '{role}' WHERE Username = '{username}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return;
        }
    }
}