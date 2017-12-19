using System;
using Microsoft.Extensions.Configuration;

namespace Meteo.Services
{
    public class DatabaseContext
    {
        public void ConnectToDatabase()
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("/home/gabriel/Scrivania/Progetti/Meteo-Creazione-file/Meteo.Services/Connection.To.Database/DatabaseConnection.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("SampleConnection");

            Console.WriteLine("Inserisci il nome dell'utente");
            string nomeInput = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome dell'utente");
            string cognomeInput = Console.ReadLine();

            var context = UsersContextFactory.Create(connectionString);

            context.Users.Add(new User
            {
                nome = nomeInput,
                cognome = cognomeInput
            });
            context.SaveChanges();
        }
    }
}