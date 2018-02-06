using System;
namespace Meteo.Services
{

    public interface IService
    {
        void Exit(string readParam);

    }
    public class ExitService : IService
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
    