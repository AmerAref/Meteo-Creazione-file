using System;
using System.Data;
using MySql.Data.MySqlClient;
using Ninject.Modules;

namespace Meteo.Services.Infrastructure
{
    public interface IDbConnectionManager
    {
        void Open();
        void Close();
    }

    public sealed class MySqlConnectionManager : IDbConnectionManager, IDisposable
    {
        private string _server;
        private string _uid;
        private string _database;
        private string _password;
        private readonly MySqlConnection _connection;

        public MySqlConnectionManager()
        {
            _server = "localhost";
            _uid = "root";
            _database = "MeteoDatabase";
            _password = "";
            // TODO: da recuperare dai settings
            var connectionString = $"SERVER={_server}; DATABASE={_database}; UID={_uid}; PASSWORD= {_password};";
            _connection = new MySqlConnection(connectionString);
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
    }

    public interface IQueryBuilder
    {
        void CreateTable(string name);
        void CreateColumn(string name, Type type);
        void CreateInsert<T>(T obj);
    }

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
    }

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
    }

    public class MySqlManager
    {
        private readonly IQueryBuilder _builder;

        public MySqlManager(IQueryBuilder builder)
        {
            _builder = builder;
        }
    }

    public class MeteoServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueryBuilder>().To<SqlServerQueryBuilder>();
            Bind<IDbConnectionManager>().To<MySqlConnectionManager>();
        }
    }
}
