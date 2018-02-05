﻿using System;
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
        public void InsertDataMaster(string meteoChoiceDb, int idUserMaster, string dateOfRequist, int idCity)
        {
            _manager.Open();
            var query = $"INSERT INTO `Master`(`Choice5DayOrNow`, `DateOfRequist`, `IdUser`, `IdCity`) VALUES ('{meteoChoiceDb}', '{dateOfRequist}', '{idUserMaster}', '{idCity}')";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<Master>();
            _manager.Close();
        }
        public City GetCityData(string lat, string lon, string place)
        {
            _manager.Open();
            var query = $"SELECT * FROM `City` WHERE `Name` = '{place}' OR (`Latitude` = '{lat}' AND `Longitude` = '{lon}')";
            var cmd = _manager.GetCommand(query);
            var city = cmd.ExecuteReader().DataReaderMapToList<City>();
            _manager.Close();
            return city[0];
        }
        public void InsertDataIntoForecastTable(dynamic jsonObj, string place, int idMaster, int idCity, string oneDayOrFiveDays, string dateOfRequest)
        {
            if (oneDayOrFiveDays == "1Day")
            {
                var oneDay = jsonObj.Parameters;

                _manager.Open();
                var query = $"INSERT INTO `Forecast`(`CityName`, `IdCity`, `Pressure`, `Humidity`, `Temperature`, `TemperatureMin`, `TemperatureMax`, `WeatherDate`, `IdMaster`) VALUES ('{place}', '{idCity}', '{oneDay.Pressure}', '{oneDay.Humidity}', '{oneDay.Temp}', '{oneDay.TempMin}', '{oneDay.TempMax}', '{dateOfRequest}', '{idMaster}')";
                var cmd = _manager.GetCommand(query);
                cmd.ExecuteNonQuery();
                _manager.Close();
            }
            else if (oneDayOrFiveDays == "5Days")
            {
                var fiveDays = jsonObj.List;

                _manager.Open();
                foreach (var data in fiveDays)
                {
                    var query = $"INSERT INTO `Forecast`(`CityName`, `IdCity`, `Pressure`, `Humidity`, `Temperature`, `TemperatureMin`, `TemperatureMax`, `WeatherDate`, `IdMaster`) VALUES ('{place}', '{idCity}', '{data.Parameters.Pressure}', '{data.Parameters.Humidity}', '{data.Parameters.Temp}', '{data.Parameters.TempMin}', '{data.Parameters.TempMax}', '{data.TimeStamp}', '{idMaster}')";
                    var cmd = _manager.GetCommand(query);
                    cmd.ExecuteNonQuery();
                }
                _manager.Close();
            }
        }
        public Master GetMasterData(int idUser, string dateOfRequist)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Master` WHERE `IdUser` = {idUser} AND `DateOfRequist` = '{dateOfRequist}'";
            var cmd = _manager.GetCommand(query);
            var master = cmd.ExecuteReader().DataReaderMapToList<Master>();
            _manager.Close();
            return master[0];
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

        public void DeleteUser(string username)
        {
            _manager.Open();
            var query = $"DELETE FROM `User` WHERE `Username` = '{username}';";
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

        //Query per l'export dei dati
        public List<Models.OneDayForecast> GetOneDayUserResearch(string username)
        {
            _manager.Open();
            var query = $"SELECT * FROM `OneDayForecast`, `User` WHERE User.Username = '{username}'";
            var cmd = _manager.GetCommand(query);
            var researchData = cmd.ExecuteReader().DataReaderMapToList<Models.OneDayForecast>();
            _manager.Close();
            return researchData;
        }
        public List<Models.Forecast> GetForecastUserOneDayResearch(string username)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Forecast`, `User`, `Master` WHERE User.Username = '{username}' AND Master.Choice5DayOrNow IN ('Forecast 1Day (city)', 'Forecast 1Day (coordinates)') AND Forecast.IdMaster = Master.IdMaster";
            var cmd = _manager.GetCommand(query);
            var researchData = cmd.ExecuteReader().DataReaderMapToList<Models.Forecast>();
            _manager.Close();
            return researchData;
        }
        public List<Models.Forecast> GetForecastUserNext5DaysResearch(string username)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Forecast`, `User`, `Master` WHERE User.Username = '{username}' AND Master.Choice5DayOrNow IN ('Forecast 5Days (city)', 'Forecast 5Days (coordinates)') AND Forecast.IdMaster = Master.IdMaster";
            var cmd = _manager.GetCommand(query);
            var researchData = cmd.ExecuteReader().DataReaderMapToList<Models.Forecast>();
            _manager.Close();
            return researchData;
        }
        public List<Models.FiveDaysForecast> GetNextFiveDaysUserResearch(string username)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Last5DaysForecast`, `User` WHERE User.Username = '{username}'";
            var cmd = _manager.GetCommand(query);
            var researchData = cmd.ExecuteReader().DataReaderMapToList<Models.FiveDaysForecast>();
            _manager.Close();
            return researchData;
        }

        public List<Models.Forecast> GetForecastFilteredByDate(string username, string dataInizio, string dataFine)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Forecast`, `User` WHERE (Forecast.WeatherDate  BETWEEN '{dataInizio}' AND '{dataFine}') AND User.Username = '{username}'";
            var cmd = _manager.GetCommand(query);
            var filteredData = cmd.ExecuteReader().DataReaderMapToList<Models.Forecast>();
            _manager.Close();
            return filteredData;
        }
    }
}