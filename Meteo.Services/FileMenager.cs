using System;
using System.Net.Mail;
using System.IO;
namespace Meteo.Services
{
    public class FileMenager
    {
        public string CreateNewFile(string fileName, string jsonStr)
        {
            System.IO.File.WriteAllText(fileName, jsonStr);
            return fileName;

        }
      
        public void DeleteFile(string fileNameDelete)
        {
            File.Delete(fileNameDelete);
        }

    }
}
