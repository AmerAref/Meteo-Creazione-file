 using System;
 using Microsoft.EntityFrameworkCore;
 using MySql;

 namespace Meteo.Services {
     public static class UsersContextFactory {
         public static UsersContext Create (string connectionString) {
             var optionsBuilder = new DbContextOptionsBuilder<UsersContext> ();
             optionsBuilder.UseMySQL (connectionString);

             //Ensure database creation
             var context = new UsersContext (optionsBuilder.Options);
             context.Database.EnsureCreated ();

             return context;
         }
     }

 }