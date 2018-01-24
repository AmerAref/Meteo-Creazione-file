using System;
using System.Reflection;
using Ninject;

namespace Meteo.Services.Infrastructure
{
    public class QueryBuilderServices
    {
        public IQueryBuilder QueryBuilder()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetAssembly(typeof(MySqlManager)));
            var queryBuilder = kernel.Get<IQueryBuilder>();
            var manager = new MySqlManager(queryBuilder);
            return queryBuilder;
        }
    }
}
