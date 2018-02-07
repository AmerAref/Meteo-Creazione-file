using System;
using System.Text.RegularExpressions;

namespace Meteo.Services
{
    public static class Helper
    {
        public static bool RegexForPsw(string password)
        {
            const string matchEmailPattern = "^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*)(?=.*[#$^+=!*()@%&]).{8,}$";
            return password != null && Regex.IsMatch(password, matchEmailPattern);
        }
        public static DateTime UnixTimeStampToDateTime(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}