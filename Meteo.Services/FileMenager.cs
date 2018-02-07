using System.IO;

namespace Meteo.Services
{
    public class FileMenager
    {
        public string CreateNewFile(string fileName, string jsonStr)
        {
            File.WriteAllText(fileName, jsonStr);
            return fileName;
        }
        public void DeleteFile(string fileNameDelete)
        {
            File.Delete(fileNameDelete);
        }
    }
}
