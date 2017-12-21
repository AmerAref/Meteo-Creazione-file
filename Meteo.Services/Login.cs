using System;
using System.Collections.Generic;
using System.Linq;
namespace Meteo.Services {
    public class Login {
        public List<User> LoginAttempts (UsersContext context, string usernameAuthentication , string passwordAuthentication ) {
            
            
            var autentication = context.Users.Where (x => x.Username.Equals (usernameAuthentication) && x.Password.Equals (passwordAuthentication)).ToList ();
            
            return autentication;

        }
    }
}