using System;
using Ninject.Modules;

namespace Meteo.Services.Infrastructure
{
    public class MeteoServicesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueryBuilder>().To<MySqlQueryBuilder>();
            Bind<IDbConnectionManager>().To<MySqlConnectionManager>();
        }
    }
}
