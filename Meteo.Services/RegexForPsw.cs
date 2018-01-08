using System.Text.RegularExpressions;

namespace Meteo.Services
{
    public static class RegexForPsw
    {

        public static bool RegexExp(string password)
        {


            string MatchEmailPattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*)(?=.*[#$^+=!*()@%&]).{8,}$";


            if (password != null)
            {
                return Regex.IsMatch(password, MatchEmailPattern);
            }

            return false;




        }
    }
}

