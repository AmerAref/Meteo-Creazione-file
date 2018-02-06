using System;
namespace Meteo.Services
{
    public class ExitService
    {
        public void Exit(string readParam)
        {
            if (readParam == "exit") 
            {
                Console.WriteLine("Session ended");
                Environment.Exit(0);
            }
        }
    }
}