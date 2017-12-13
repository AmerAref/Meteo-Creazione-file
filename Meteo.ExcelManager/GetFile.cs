using System;
using System.IO;
using System.Text;

namespace Meteo.ExcelManager
{
    public class GetFile
    {
        string path = "/home/amer/Meteo Corretto/app/Meteo-Creazione-file/Meteo.UI/Milan.json";
        public string ReciveFile()
        {
            string readText = File.ReadAllText(path);
            Console.WriteLine(readText);
            return readText;
        }

    }
}
