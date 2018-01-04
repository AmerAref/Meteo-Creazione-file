using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace Meteo.Services
{
    public class Login
    {
        public List<User> LoginAttempts(UsersContext context, string usernameAuthentication, string passwordAuthentication)
        {
            var autentication = context.Users.Where(x => x.Username.Equals(usernameAuthentication) && x.Password.Equals(passwordAuthentication)).ToList();

            return autentication;
        }


        public string EncryptInsertedPwd(string authenticationPwd)
        {
            byte[] bytePwd = Encoding.Unicode.GetBytes(authenticationPwd);
            var hasher = System.Security.Cryptography.SHA256.Create();
            byte[] hashedBytes = hasher.ComputeHash(bytePwd);
            var encryptedAuthPwd = Convert.ToBase64String(hashedBytes);

            return encryptedAuthPwd;
        }

        public List<User> ControlUserIfExist(UsersContext context, string usernameAuthentication )
        {
            var autentication = context.Users.Where(x => x.Username.Equals(usernameAuthentication)).ToList();


            return autentication; 
        }
    }
}