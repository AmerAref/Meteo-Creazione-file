using System;
using System.Text;

namespace Meteo.Services
{
    public class Registration
    {
        public string EncryptPwd(string clearPwd)
        {
            byte[] bytePwd = Encoding.Unicode.GetBytes(clearPwd);
            var hasher = System.Security.Cryptography.SHA256.Create();
            byte[] hashedBytes = hasher.ComputeHash(bytePwd);
            var encryptedPwd = Convert.ToBase64String(hashedBytes);

            return encryptedPwd;
        }

        public string DecryptPwd()
        {
            var decryptedPwd = "";

            return decryptedPwd;
        }
    }
}