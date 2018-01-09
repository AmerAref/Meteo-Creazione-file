using System;
using System.Text;

namespace Meteo.Services
{
    public class Register
    {
        public string EncryptPwd(string clearPwd)
        {
            var bytePwd = Encoding.Unicode.GetBytes(clearPwd);
            var hasher = System.Security.Cryptography.SHA256.Create();
            var hashedBytes = hasher.ComputeHash(bytePwd);
            var encryptedPwd = Convert.ToBase64String(hashedBytes);

            return encryptedPwd;
        }
    }
}
