using System.Collections.Generic;
using System.Linq;
using Meteo.Services.Infrastructure;
using Meteo.Services.Models;
namespace Meteo.Services
{
    public class Login
    {
        public List<User> LoginAttempts(ApplicationDbContext context, string usernameAuthentication,
            string passwordAuthentication)
        {
            var autentication = context.Users
                .Where(x => x.Username.Equals(usernameAuthentication) && x.Password.Equals(passwordAuthentication))
                .ToList();

            return autentication;
        }

        public List<User> ControlUserIfExist(ApplicationDbContext context, string usernameAuthentication)
        {
            var autentication = context.Users.Where(x => x.Username.Equals(usernameAuthentication)).ToList();


            return autentication;
        }

        public List<User> ControlAnswer(string insertAnswerForAccess, ApplicationDbContext context,
            string usernameAuthentication)
        {
            var autentication = context.Users
                .Where(x => x.Username.Equals(usernameAuthentication) && x.Answer.Equals(insertAnswerForAccess))
                .ToList();
            return autentication;
        }
    }
}