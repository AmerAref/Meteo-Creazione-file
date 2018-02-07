using System;

namespace Meteo.Services.Infrastructure
{
    public class MySqlManager
    {
        private readonly IQueryBuilder _builder;

        public MySqlManager(IQueryBuilder builder)
        {
            _builder = builder;
        }
    }
}