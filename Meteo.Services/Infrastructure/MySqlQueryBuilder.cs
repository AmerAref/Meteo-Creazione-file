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
            { return user[0]; }
            else
            { return null; }
        }
        public Question GetQuestion(int IdQuestion)
        {
            _manager.Open();
            var query = $"SELECT * FROM Question WHERE IdQuestion = '{IdQuestion}'";
            var cmd = _manager.GetCommand(query);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            _manager.Close();
            if (question.Any())
            { return question[0]; }
            else
            { return null; }
        }
        public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, int languageNewAccount, string measureUnit, int role)
        {
            _manager.Open();
            var query = $"INSERT INTO User (`Name`, `Surname`, `Username`, `Password`, `IdQuestion`, `Answer`, `UnitOfMeasure`, `IdRole`, `IdLanguage`) VALUES ('{nameNewAccount}', '{surnameNewAccount}','{usernameNewAccount}', '{encryptedPwd}', {selectQuestion}, '{encryptedAnswer}', '{measureUnit}',  {role}, {languageNewAccount})";
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
            { return user[0]; }
            else
            { return null; }
        }
        public User AutentiationWithAnswer(string answer, string username)
        {
            _manager.Open();
            var query = $" SELECT * FROM User WHERE Username = '{username}' AND Answer = '{answer}'";
            var cmd = _manager.GetCommand(query);
            var user = cmd.ExecuteReader().DataReaderMapToList<User>();
            _manager.Close();
            if (user.Any())
            { return user[0]; }
            else
            { return null; }
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
            var query = $"SELECT * FROM Question WHERE IdLanguage = 2";
            var cmd = _manager.GetCommand(query);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            _manager.Close();

            return question;
        }
        public List<Question> AllQuestionsIT()
        {
            _manager.Open();
            var query = "SELECT * FROM Question WHERE IdLanguage = 1";
            var cmd = _manager.GetCommand(query);
            var question = cmd.ExecuteReader().DataReaderMapToList<Question>();
            _manager.Close();

            return question;
        }

        public List<Languages> GetAllLanguages()
        {
            _manager.Open();
            var query = "SELECT * FROM Language";
            var cmd = _manager.GetCommand(query);
            var language = cmd.ExecuteReader().DataReaderMapToList<Languages>();
            _manager.Close();
            return language;
        }
        public long InsertDataMaster(string meteoChoiceDb, int idUserMaster, string dateOfRequist, int idCity)
        {
            _manager.Open();
            var query = $"INSERT INTO `Master`(`Choice5DayOrNow`, `DateOfRequist`, `IdUser`, `IdCity`) VALUES ('{meteoChoiceDb}', '{dateOfRequist}', '{idUserMaster}', '{idCity}')";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteReader().DataReaderMapToList<Master>();
            var lastInsertedForecastId = cmd.LastInsertedId;
            _manager.Close();
            return lastInsertedForecastId;
        }
        public City GetCityData(string lat, string lon, string place)
        {
            _manager.Open();
            var query = $"SELECT * FROM `City` WHERE `Name` = '{place}' OR (`Latitude` = '{lat}' AND `Longitude` = '{lon}')";
            var cmd = _manager.GetCommand(query);
            var city = cmd.ExecuteReader().DataReaderMapToList<City>();
            _manager.Close();
            if (city != null)
            { return city[0]; }
            else
            {
                Console.WriteLine("ERROR");
                Environment.Exit(0);
            }
            return city[0];
        }
        public long InsertDataIntoForecastTable(dynamic jsonObj, string place, long idMaster, int idCity, string oneDayOrFiveDays, string dateOfRequest)
        {
            long lastInsertedForecastId = 0L;
            if (oneDayOrFiveDays == "1Day")
            {
                var oneDay = jsonObj.Parameters;

                _manager.Open();
                var query = $"INSERT INTO `Forecast`(`WeatherDate`, `IdMaster`) VALUES ('{dateOfRequest}', '{idMaster}')";
                var cmd = _manager.GetCommand(query);
                cmd.ExecuteNonQuery();
                lastInsertedForecastId = cmd.LastInsertedId;
                _manager.Close();
            }
            else if (oneDayOrFiveDays == "5Days")
            {
                var fiveDays = jsonObj.List;

                _manager.Open();
                foreach (var data in fiveDays)
                {
                    var query = $"INSERT INTO `Forecast`(`WeatherDate`, `IdMaster`) VALUES ('{data.TimeStamp}', '{idMaster}')";
                    var cmd = _manager.GetCommand(query);
                    cmd.ExecuteNonQuery();
                    lastInsertedForecastId = cmd.LastInsertedId;
                }
                _manager.Close();
            }
            return lastInsertedForecastId;
        }

        public void InsertMeasureValue(dynamic jsonObj, long lastForecastId, int lang, string oneOrFiveDays)
        {
            if (oneOrFiveDays == "1Day")
            {
                _manager.Open();
                var oneDay = jsonObj.Parameters;
                string queryTemp = "", queryTempMin = "", queryTempMax = "";

                var queryHum = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.Humidity}', '1', '{lastForecastId}')";
                var queryPres = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.Pressure}', '2', '{lastForecastId}')";
                if (lang == 1)
                {
                    queryTemp = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.Temp}', '3', '{lastForecastId}')";
                    queryTempMin = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.TempMin}', '4', '{lastForecastId}')";
                    queryTempMax = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.TempMax}', '5', '{lastForecastId}')";
                }
                else if (lang == 2)
                {
                    queryTemp = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.Temp}', '6', '{lastForecastId}')";
                    queryTempMin = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.TempMin}', '7', '{lastForecastId}')";
                    queryTempMax = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{oneDay.TempMax}', '8', '{lastForecastId}')";
                }
                var cmdHum = _manager.GetCommand(queryHum);
                var cmdPres = _manager.GetCommand(queryPres);
                var cmdTemp = _manager.GetCommand(queryTemp);
                var cmdTempMin = _manager.GetCommand(queryTempMin);
                var cmdTempMax = _manager.GetCommand(queryTempMax);
                cmdHum.ExecuteNonQuery();
                cmdPres.ExecuteNonQuery();
                cmdTemp.ExecuteNonQuery();
                cmdTempMin.ExecuteNonQuery();
                cmdTempMax.ExecuteNonQuery();
                _manager.Close();
            }
            else if (oneOrFiveDays == "5Days")
            {
                _manager.Open();
                var fiveDays = jsonObj.List;
                string queryTemp = "", queryTempMin = "", queryTempMax = "";
                foreach (var data in fiveDays)
                {
                    var queryHum = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.Humidity}', '1', '{lastForecastId}')";
                    var queryPres = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.Pressure}', '2', '{lastForecastId}')";
                    if (lang == 1)
                    {
                        queryTemp = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.Temp}', '3', '{lastForecastId}')";
                        queryTempMin = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.TempMin}', '4', '{lastForecastId}')";
                        queryTempMax = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.TempMax}', '5', '{lastForecastId}')";
                    }
                    else if (lang == 2)
                    {
                        queryTemp = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.Temp}', '6', '{lastForecastId}')";
                        queryTempMin = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.TempMin}', '7', '{lastForecastId}')";
                        queryTempMax = $"INSERT INTO `MeasureValue` (`Value`, `IdMeasureType`, `IdForecast`) VALUES ('{data.Parameters.TempMax}', '8', '{lastForecastId}')";
                    }
                    var cmdHum = _manager.GetCommand(queryHum);
                    var cmdPres = _manager.GetCommand(queryPres);
                    var cmdTemp = _manager.GetCommand(queryTemp);
                    var cmdTempMin = _manager.GetCommand(queryTempMin);
                    var cmdTempMax = _manager.GetCommand(queryTempMax);
                    cmdHum.ExecuteNonQuery();
                    cmdPres.ExecuteNonQuery();
                    cmdTemp.ExecuteNonQuery();
                    cmdTempMin.ExecuteNonQuery();
                    cmdTempMax.ExecuteNonQuery();
                }
                _manager.Close();
            }
        }

        public List<Forecast> GetForecastDataByLastInsertedId(long lastInsertedForecastId)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Forecast` WHERE `IdMaster` = '{lastInsertedForecastId}'";
            var cmd = _manager.GetCommand(query);
            var myData = cmd.ExecuteReader().DataReaderMapToList<Forecast>();
            _manager.Close();
            return myData;
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

        public MeasureTrigger GetMeasureTriggerValues(int idUser)
        {
            _manager.Open();
            var query = $"SELECT * FROM `MeasureTrigger` INNER JOIN City ON MeasureTrigger.IdTrigger = City.IdTrigger";
            var cmd = _manager.GetCommand(query);
            var measure = cmd.ExecuteReader().DataReaderMapToList<MeasureTrigger>();
            _manager.Close();

            return measure[0];
        }

        public List<Master> GetAllMasterRecordsByUserId(int idUser)
        {
            _manager.Open();
            var query = $"SELECT * FROM Master WHERE IdUser = '{idUser}'";
            var cmd = _manager.GetCommand(query);
            var records = cmd.ExecuteReader().DataReaderMapToList<Master>();
            _manager.Close();

            return records;
        }

        public void DeleteMasterRecord(string idMaster)
        {
            _manager.Open();
            var query = $"DELETE FROM `Master` WHERE `IdMaster` = '{idMaster}';";
            var cmd = _manager.GetCommand(query);
            cmd.ExecuteNonQuery();
            _manager.Close();
        }

        public List<Models.Forecast> GetForecastFilteredByDate(string dataInizio, string dataFine, int idUser)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Forecast`, `Master` WHERE Master.IdUser = '{idUser}' AND (Master.DateOfRequist  BETWEEN '{dataInizio}' AND '{dataFine}') AND Master.IdMaster = Forecast.IdMaster";
            var cmd = _manager.GetCommand(query);
            var filteredData = cmd.ExecuteReader().DataReaderMapToList<Models.Forecast>();
            _manager.Close();
            return filteredData;
        }
        public void UpdateCities(List<CityJsonModels.CitiesJson> allCity)
        {
            _manager.Open();

            foreach (var city in allCity)
            {
                var query = $"UPDATE  `City` SET `IdCity` = '{city.IdCity}' , `Country` = '{city.Country}', `Longitude` = '{city.Coord.Longitude}', `Latitude`= '{city.Coord.Latitude}', `Name`='{city.Name}'  WHERE `IdCity` = '{city.IdCity}'";
                var cmd = _manager.GetCommand(query);
                cmd.ExecuteNonQuery();
            }
            _manager.Close();
            return;
        }
        public List<Models.Forecast> FilterSearcheByCity(string place, int idUser)
        {
            _manager.Open();
            var query = $"SELECT * FROM `Forecast`, `Master` WHERE Master.IdUser = '{idUser}' AND Master.IdCity = (SELECT Id FROM `City` WHERE Name = '{place}') AND Master.IdForecast = Forecast.IdForecast";
            var cmd = _manager.GetCommand(query);
            var filteredData = cmd.ExecuteReader().DataReaderMapToList<Models.Forecast>();
            _manager.Close();
            return filteredData;
        }


        //Query per l'export dei dati
        public List<MeasureValue> GetUserForecastResearch(int idUser, int idMeasureType)
        {
            _manager.Open();
            var query = $"SELECT measurevalue.IdMeasureValue, measurevalue.Value, measurevalue.IdMeasureType, measurevalue.IdForecast " +
                "FROM `MeasureValue`, `Master`, `Forecast` " +
                $"WHERE MeasureValue.IdMeasureType = '{idMeasureType}' AND Master.IdUser = '{idUser}' AND Master.IdMaster = forecast.IdMaster AND forecast.IdForecast = measurevalue.IdForecast";
            var cmd = _manager.GetCommand(query);
            var researchData = cmd.ExecuteReader().DataReaderMapToList<MeasureValue>();
            _manager.Close();
            return researchData;
        }
    }
}