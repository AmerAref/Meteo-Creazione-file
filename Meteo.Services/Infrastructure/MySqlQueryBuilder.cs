using System;
using System.Collections.Generic;
using System.Linq;
using Meteo.Services.Models;


namespace Meteo.Services.Infrastructure
{
    public class MySqlQueryBuilder : IQueryBuilder
    {
        private readonly IDbConnectionManager _manager;

        public MySqlQueryBuilder(IDbConnectionManager manager)
        {
            _manager = manager;
        }
        public void CreateTable(string name)
        {
            _manager.Open();
            // TODO something
            _manager.Close();
        }

        public void CreateColumn(string name, Type type)
        {
            throw new NotImplementedException();
        }

        public void CreateInsert<T>(T obj)
        {
            throw new NotImplementedException();
        }
        public User GetUser(string username)
        {
            _manager.Open();
            var query = $"SELECT * FROM User WHERE Username = '{username}'";
            var cmd = _manager.GetCommand(query);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();
            if (user.Any())
            {
                return user[0];
            }
            else
            {
                return null;
            }
        }

        public Question GetQuestion(int IdQuestion)
        {
            _manager.Open();
            var query = $"SELECT * FROM Question WHERE IdQuestion = '{IdQuestion}'";
            var cmd = _manager.GetCommand(query);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            _manager.Close();
            if (question.Any())
            {
                return question[0];
            }
            else
            { return null; }

        }

        public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role)
        {
            _manager.Open();
            var query = $"INSERT INTO User (`Name`, `Surname`, `Username`, `Password`, `IdQuestion`, `Answer`, `Language`, `UnitOfMeasure`, `IdRole`) VALUES ('{nameNewAccount}', '{surnameNewAccount}','{usernameNewAccount}', '{encryptedPwd}', {selectQuestion}, '{encryptedAnswer}', '{languageNewAccount}', '{measureUnit}',  {role})";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();
        }

        public User GetUserIfExist(string username, string psw)
        {
            _manager.Open();
            var query = $"SELECT * FROM User WHERE Username = '{username}' AND Password = '{psw}'";
            var cmd = _manager.GetCommand(query);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();
            if (user.Any())
            {
                return user[0];
            }
            else
                return null;

        }
        public User AutentiationWithAnswer(string answer, string username)
        {
            _manager.Open();
            var query = $" SELECT * FROM User WHERE Username = '{username}' AND Answer = '{answer}'";
            var cmd = _manager.GetCommand(query);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();
            if (user.Any())
            {
                return user[0];
            }
            else
                return null;
        }
        public void QueryForUpdatePsw(string psw, string username)
        {
            _manager.Open();
            var query = $" UPDATE User SET Password = '{psw}' WHERE Username = '{username}'";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();
            return;
        }
        public List<Role> AllRoles()
        {
            _manager.Open();
            var query = $"SELECT * FROM Role ";
            var cmd = _manager.GetCommand(query);
            var role = cmd.ExecuteReader().DataReaderMapToList<Role>();
            _manager.Close();
            return role;
        }
        public void InsertOneDayForecast(OpenWeatherMap.Models.OneDayForecast jsonObj)
        {
            _manager.Open();
            var parameters = jsonObj.Parameters;
            var query = $"INSERT INTO `OneDayForecast`(`Pressure`, `Temp`, `Humidity`, `TempMin`, `TempMax`) VALUES ('{parameters.Pressure}', '{parameters.Temp}', '{parameters.Humidity}', '{parameters.TempMin}', '{parameters.TempMax}');";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<Models.OneDayForecast>();
            _manager.Close();
        }
        public List<Question> AllQuestionsEN()
        {
            _manager.Open();
            var query = $"SELECT * FROM Question WHERE Language = 'en'";
            var cmd = _manager.GetCommand(query);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            _manager.Close();

            return question;
        }
        public List<Question> AllQuestionsIT()
        {
            _manager.Open();
            var query = $"SELECT * FROM Question WHERE Language = 'it'";
            var cmd = _manager.GetCommand(query);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            _manager.Close();

            return question;
        }
        public void InsertDataMaster(string meteoChoiceDb, int idUserMaster)
        {
            _manager.Open();
            DateTime masterDate = DateTime.Now;
            string format = "yyyy-MM-dd hh:mm:ss";
            string str = masterDate.ToString(format);
            var query = $"INSERT INTO `Master`(`Choice5DayOrNow`, `DateOfRequist`, `IdUser`) VALUES ('{meteoChoiceDb}', '{str}', '{idUserMaster}')";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<Master>();
            _manager.Close();

        }

        //Query per eseguite da utente con ruolo Admin
        public List<User> GetAllUsers()
        {
            _manager.Open();
            var query = "SELECT * FROM User";
            var cmd = _manager.GetCommand(query);
            var users = cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();

            return users;
        }
        public List<Master> GetAllMasterRecords()
        {
            _manager.Open();
            var query = "SELECT * FROM Master";
            var cmd = _manager.GetCommand(query);
            var records = cmd.ExecuteReader().DataReaderMapToList<Master>();
            _manager.Close();

            return records;
        }

        public void DeleteUser(string name, string surname, string username)
        {
            _manager.Open();
            var query = $"DELETE FROM `User` WHERE `Name` = '{name}' AND `Surname` = '{surname}' AND `Username` = '{username}';";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();

        }
        public void QueryForUpdateRole(string username, int role)
        {
            _manager.Open();
            var query = $" UPDATE User SET IdRole = '{role}' WHERE Username = '{username}'";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();

            return;
        }
        public void Insert5DaysForecast(OpenWeatherMap.Models.LastFiveDaysForecast jsonObj)
        {
            var query = "";
            _manager.Open();
            foreach (var parameters in jsonObj.List)
            {

                query = $"INSERT INTO `Last5DaysForecast`(`Pressure`, `Temp`, `Humidity`, `TempMin`, `TempMax`) VALUES ('{parameters.Parameters.Pressure}', '{parameters.Parameters.Temp}', '{parameters.Parameters.Humidity}', '{parameters.Parameters.TempMin}', '{parameters.Parameters.TempMax}');";
                var cmd = _manager.GetCommand(query);
                cmd.ExecuteNonQuery();
            }

            _manager.Close();
        }

    }
}



