﻿using System;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Ninject.Modules;
using System.IO;
using Meteo.Services.Models;
using System.Linq;
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
        void InsertOneDayForecast(OpenWeatherMap.Models.OneDayForecast jsonObj, int idForecast);
        List<Question> AllQuestionsEN();
        List<Question> AllQuestionsIT();
        void InsertDataMaster(string meteoChoiceDb, int idUserMaster, string dateOfRequist, int idCity);
        List<User> GetAllUsers();
        List<Master> GetAllMasterRecords();
        void DeleteUser(string username);
        void QueryForUpdateRole(string username, int role);
        void Insert5DaysForecast(OpenWeatherMap.Models.LastFiveDaysForecast jsonobj, int idForecast);
        City GetCityData(string lat, string lon, string place);
        void InsertDataIntoForecastTable(dynamic jsonObj, string place, int idMaster, string dateOfRequist, int idCity);
        Models.Forecast GetForecastData(string dateOfRequist);
        List<Models.OneDayForecast> GetOneDayUserResearch(string username);
        List<Models.Forecast> GetForecastUserOneDayResearch(string username);
        List<Models.Forecast> GetForecastUserNext5DaysResearch(string username);
        List<Models.LastFiveDaysForecast> GetNextFiveDaysUserResearch(string username);
        Master GetMasterData(int idUser, string dateOfRequist);
        List<Models.Forecast> GetForecastFilteredByDate(string username, string dataInizio, string dataFine);
    }
}