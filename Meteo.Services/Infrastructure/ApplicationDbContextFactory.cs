//using System.IO;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace Meteo.Services.Infrastructure
//{
//    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile(@"Infrastructure/DatabaseConnection.json")
//                .Build();

//            var connectionString = configuration.GetConnectionString("SampleConnection");

//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseMySQL(connectionString);

//            var dbContext = new ApplicationDbContext(optionsBuilder.Options);
//            dbContext.Database.EnsureCreated();

//            return dbContext;
//        }
//    }
//}