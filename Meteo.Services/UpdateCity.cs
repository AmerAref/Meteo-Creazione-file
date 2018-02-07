using System;
using System.IO;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO.Compression;

namespace Meteo.Services
{
    public class UpdateCity
    {
        public void DownloadJsonCity()
        {
            string remoteUri = "http://bulk.openweathermap.org/sample/";
            string fileName = "current.city.list.json.gz";

            var myWebClient = new WebClient();
            var myWebResource = remoteUri + fileName;
            myWebClient.DownloadFile(myWebResource, fileName);
            return;
        }

        public List<CityJsonModels.CityJson> DataReadyToUpdateTableCity()
        {
            var pathWhereTheFileIsDownload = Directory.GetCurrentDirectory();

            string fileName = "/current.city.list.json.gz";
            pathWhereTheFileIsDownload = pathWhereTheFileIsDownload + fileName;
            var serializer = new JsonSerializer();
            var fileToDecompress = new FileInfo(pathWhereTheFileIsDownload);
            Decompress(fileToDecompress);

            fileName = "/current.city.list.json";
            var pathFileDecompressed = pathWhereTheFileIsDownload + fileName;

            var allCity = JsonConvert.DeserializeObject<List<CityJsonModels.CityJson>>(File.ReadAllText(pathFileDecompressed));
            File.Delete(pathFileDecompressed); // Delete All files
            File.Delete(pathWhereTheFileIsDownload);

            return allCity;
        }
        public static void Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Console.WriteLine("Decompressed: {0}", fileToDecompress.Name);
                    }
                }
            }
        }
    }
}