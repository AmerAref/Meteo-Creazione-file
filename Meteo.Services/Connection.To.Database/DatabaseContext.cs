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
            string mioNome = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome dell'utente");
            string mioCognome = Console.ReadLine();

            // Create an employee instance and save the entity to the database
            var entry = new User() { nome = mioNome, cognome = mioCognome };

            using (var context = UsersContextFactory.Create(connectionString))
            {
                context.Add(entry);
                context.SaveChanges();
            }

            Console.WriteLine($"Employee was saved in the database with id: {entry.idUtente}");
        }
    }
}