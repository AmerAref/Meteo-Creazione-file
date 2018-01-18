using System;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Meteo.Services.OpenWeatherMap.Models;
using Meteo.Services.Models;
using System.IO;
using Newtonsoft.Json;

namespace Meteo.Services.Infrastructure
{
    public class QueryManager : DbFactoryManager
    {
        public Question GetQuestion(int ciao)
        {
            OpenConnection();
            string query = $"SELECT * FROM Question WHERE IdQuestion = '{ciao}'";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            return question[0];
        }
        public User GetUsername()
        {
            OpenConnection();
            string query = $"SELECT * FROM User";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user[0];
        }
        public User GetUser(string username)
        {
            OpenConnection();
            string query = $"SELECT * FROM User WHERE Username = '{username}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user[0];
        }
        public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role)
        {
            OpenConnection();
            string query = $"INSERT INTO User (`Name`, `Surname`, `Username`, `Password`, `IdQuestion`, `Answer`, `Language`, `UnitOfMeasure`, `IdRole`) VALUES ('{nameNewAccount}', '{surnameNewAccount}','{usernameNewAccount}', '{encryptedPwd}', {selectQuestion}, '{encryptedAnswer}', '{languageNewAccount}', '{measureUnit}',  {role})";
            var cmd = new MySqlCommand(query, _connection);
            cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
        }
        public bool CheckIfUsernameExisit(string username)
        {
            OpenConnection();
            string query = $"SELECT * FROM User WHERE Username = '{username}'";
            try
            {
                var cmd = new MySqlCommand(query, _connection);
                var user = cmd.ExecuteReader().DataReaderMapToList<User>();
                CloseConnection();
                if (user.Any())
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return true;
            }
        }

        public Question GetQuestionIfUserNotRemeberPsw(int reciveIdQuestion)
        {
            OpenConnection();
            string query = $"SELECT * FROM Question WHERE IdQuestion = '{reciveIdQuestion}'";
            var cmd = new MySqlCommand(query, _connection);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            CloseConnection();
            return question[0];
        }
        public List<User> GetUserIfExist(string username, string psw)
        {
            OpenConnection();
            string query = $"SELECT * FROM User WHERE Username = '{username}' AND Password = '{psw}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user;
        }
        public List<User> AutentiationWithAnswer(string answer, string username)
        {
            OpenConnection();
            string query = $" SELECT * FROM User WHERE Username = '{username}' AND Answer = '{answer}'";
            var cmd = new MySqlCommand(query, _connection);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            CloseConnection();
            return user;
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

        public void InsertOneDayForecast(OpenWeatherMap.Models.OneDayForecast jsonObj)
        {
            OpenConnection();
            var parameters = jsonObj.Parameters;
            string query = $"INSERT INTO `OneDayForecast`(`Pressure`, `Temp`, `Humidity`, `TempMin`, `TempMax`) VALUES ('{parameters.Pressure}', '{parameters.Temp}', '{parameters.Humidity}', '{parameters.TempMin}', '{parameters.TempMax}');";
            var cmd = new MySqlCommand(query, _connection);
            var oenDayForecastData = cmd.ExecuteReader().DataReaderMapToList<Models.OneDayForecast>();
            CloseConnection();
        }


            public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role)
            {
                OpenConnection();
                string query = $"INSERT INTO User (`Name`, `Surname`, `Username`, `Password`, `IdQuestion`, `Answer`, `Language`, `UnitOfMeasure`, `IdRole`) VALUES ('{nameNewAccount}', '{surnameNewAccount}','{usernameNewAccount}', '{encryptedPwd}', {selectQuestion}, '{encryptedAnswer}', '{languageNewAccount}', '{measureUnit}',  {role})";
                var cmd = new MySqlCommand(query, _connection);
                cmd.ExecuteReader().DataReaderMapToList<User>();
                CloseConnection();
            }
            public List<User> GetUserIfExist(string username, string psw)
            {
                OpenConnection();
                string query = $"SELECT * FROM User WHERE Username = '{username}' AND Password = '{psw}'";
                var cmd = new MySqlCommand(query, _connection);
                var user = cmd.ExecuteReader().DataReaderMapToList<User>();
                CloseConnection();
                return user;
            }
            public List<User> AutentiationWithAnswer(string answer, string username)
            {
                OpenConnection();
                string query = $" SELECT * FROM User WHERE Username = '{username}' AND Answer = '{answer}'";
                var cmd = new MySqlCommand(query, _connection);
                var user = cmd.ExecuteReader().DataReaderMapToList<User>();
                CloseConnection();
                return user;
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
            public List<Question> AllQuestionIT()
            {
                OpenConnection();
                string query = $"SELECT * FROM Question WHERE Language = 'it' ";
                var cmd = new MySqlCommand(query, _connection);
                var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
                CloseConnection();
                return question;
            }
        public List<Question> AllQuestionEN()
        {
            OpenConnection();
            string query = $"SELECT * FROM Question WHERE Language = 'en'  ";
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

            public void InsertOneDayForecast(OpenWeatherMap.Models.OneDayForecast jsonObj)
            {
                OpenConnection();
                var parameters = jsonObj.Parameters;
                string query = $"INSERT INTO `OneDayForecast`(`Pressure`, `Temp`, `Humidity`, `TempMin`, `TempMax`) VALUES ('{parameters.Pressure}', '{parameters.Temp}', '{parameters.Humidity}', '{parameters.TempMin}', '{parameters.TempMax}');";
                var cmd = new MySqlCommand(query, _connection);
                var oenDayForecastData = cmd.ExecuteReader().DataReaderMapToList<Models.OneDayForecast>();
                CloseConnection();
            }
        public void InsertDataMaster(string meteoChoiceDb, int idUserMaster)
        {
            OpenConnection();
            DateTime masterDate = DateTime.Now;
            string format = "yyy-MM-dd hh:mm:ss";
            string str = masterDate.ToString(format);
            string query = $"INSERT INTO `Master` (`Choice5DayOrNow`, `DateOfRequist`, `IdUser`) VALUES ('{meteoChoiceDb}', '{str}', '{idUserMaster}');";
            var cmd = new MySqlCommand(query, _connection);
            var meteoMasterData = cmd.ExecuteReader().DataReaderMapToList<Models.Master>();
            CloseConnection();
        }
    }
}