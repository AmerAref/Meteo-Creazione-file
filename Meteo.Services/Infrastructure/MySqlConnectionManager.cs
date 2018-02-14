using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;
using Meteo.Services.Models;
using System.Collections.Generic;

namespace Meteo.Services.Infrastructure
{
    public interface IDbConnectionManager
    {
        void Open();
        void Close();
        MySqlCommand GetCommand(string query);
    }

    public sealed class MySqlConnectionManager : IDbConnectionManager, IDisposable
    {
        private IConfigurationRoot _configuration;
        private readonly MySqlConnection _connection;

        public MySqlConnectionManager()
        {
            _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Infrastructure/DatabaseConnection.json")
                .Build();

            // TODO: da recuperare dai settings
            _connection = new MySqlConnection(_configuration.GetConnectionString("SampleConnection"));

        }
        public void Open()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        public void Dispose()
        {
            if (_connection != null)
            {
                Close();
            }
        }

        public MySqlCommand GetCommand(string query)
        {
            return new MySqlCommand(query, _connection);
        }
    }

    public interface IQueryBuilder
    {
        void CreateTable(string name);
        void CreateColumn(string name, Type type);
        void CreateInsert<T>(T obj);
        Question GetQuestion(int IdQuestion);
        User GetUser(string username);
        void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role);
        User GetUserIfExist(string username, string psw);
        User AutentiationWithAnswer(string answer, string username);
        void QueryForUpdatePsw(string psw, string username);
        List<Role> AllRoles();
        List<Question> AllQuestionsEN();
        List<Question> AllQuestionsIT();
        long InsertDataMaster(string meteoChoiceDb, int idUserMaster, string dateOfRequist, int idCity);
        List<User> GetAllUsers();
        List<Master> GetAllMasterRecords();
        void DeleteUser(string username);
        void QueryForUpdateRole(string username, int role);
        City GetCityData(string lat, string lon, string place);
        void UpdateCities(List<CityJsonModels.CitiesJson> allCity);
        long InsertDataIntoForecastTable(dynamic jsonObj, string place, long idMaster, int idCity, string oneDayOrFiveDays, string dateOfRequest);
        List<Models.Forecast> GetUserForecastResearch(int idUser);
        List<Models.Forecast> GetForecastFilteredByDate(string dataInizio, string dataFine, int idUser);
        List<Forecast> GetForecastDataByLastInsertedId(long lastInsertedForecastId);
        List<Models.Forecast> FilterSearcheByCity(string place, int idUser);
        void InsertMeasureValue(dynamic jsonObj, long lastForecastId, string lang, string oneOrFiveDays);
        List<Master> GetAllMasterRecordsByUserId(int idUser);
        void DeleteMasterRecord(string idMaster);
        MeasureTrigger GetMeasureTriggerValues(int idUser);
    }
}