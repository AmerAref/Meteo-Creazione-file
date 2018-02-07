using System;

namespace Meteo.Services
{
    public interface IService
    {
        void Exit(string readParam);
    }
    public class ExitService : IService
    {
        public void Exit(string readRequest)
        {
            if (readRequest == "exit")
            {
                Console.WriteLine("Session ended");
                Environment.Exit(0);
            }
        }
    }
}