﻿using System;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.Services.Infrastructure
{
    public class SqlServerQueryBuilder : IQueryBuilder
    {
        private readonly IDbConnectionManager _manager;

        public SqlServerQueryBuilder(IDbConnectionManager manager)
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
            return null;
        }
        public Question GetQuestion(int IdQuestion)
        {
            return null;
        }
        public void InsertNewUser(string encryptedPwd, string usernameNewAccount, string surnameNewAccount, string nameNewAccount, int selectQuestion, string encryptedAnswer, string languageNewAccount, string measureUnit, int role)
        {
            return;
        }
        public User GetUserIfExist(string username, string psw)
        {
            return null;
        }
        public User AutentiationWithAnswer(string answer, string username)
        {
            return null;
        }
        public void QueryForUpdatePsw(string psw, string username)
        {
            return;
        }
        public List<Role> AllRoles()
        {
            return null;
        }
        public void InsertOneDayForecast(OpenWeatherMap.Models.OneDayForecast jsonObj, int idForecast)
        {
            return;
        }
        public List<Question> AllQuestionsEN()
        {
            return null;
        }
        public List<Question> AllQuestionsIT()
        {
            return null;
        }
        public void InsertDataMaster(string meteoChoiceDb, int idUserMaster, string dateOfRequist, int idCity)
        {
            return;
        }
        public List<User> GetAllUsers()
        {
            return null;
        }
        public List<Master> GetAllMasterRecords()
        {
            return null;
        }
        public void DeleteUser(string username)
        {
            return;
        }
        public void QueryForUpdateRole(string username, int role)
        {
            return;
        }
        public void Insert5DaysForecast(OpenWeatherMap.Models.LastFiveDaysForecast jsonObj, int idForecast)
        {
            return;
        }
        public City GetCityData(string lat, string lon, string place)
        {
            return null;
        }
        public void InsertDataIntoForecastTable(dynamic jsonObj, string place, int idMaster, string dateOfRequist, int idCity)
        {
            return;
        }
        public Models.Forecast GetForecastData(string dateOfRequist)
        {
            return null;
        }
        public List<Models.OneDayForecast> GetOneDayUserResearch(string username)
        {
            return null;
        }
        public List<Models.Forecast> GetForecastUserOneDayResearch(string username)
        {
            return null;
        }
        public List<Models.LastFiveDaysForecast> GetNextFiveDaysUserResearch(string username)
        {
            return null;
        }
        public Master GetMasterData(int idUser, string dateOfRequist)
        {
            return null;
        }
        public List<Models.Forecast> GetForecastUserNext5DaysResearch(string username)
        {
            return null;
        }
    }
}