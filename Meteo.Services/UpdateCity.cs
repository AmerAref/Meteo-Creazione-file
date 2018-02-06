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
            var myStringWebResource = remoteUri + fileName;
            myWebClient.DownloadFile(myStringWebResource, fileName);
            return;
        }

        public List<CityJsonModels.CityJson> DataReadyToInsert()
        {
            var path = Directory.GetCurrentDirectory();

            string fileName = "/current.city.list.json.gz";
            var pathFileCompressed = path + fileName;
            var serializer = new JsonSerializer();
            var fileToDecompressed = new FileInfo(pathFileCompressed);
            Decompress(fileToDecompressed);
            fileName = "/current.city.list.json";
            var pathFileDecompressed = path + fileName;

            var allCity = JsonConvert.DeserializeObject<List<CityJsonModels.CityJson>>(File.ReadAllText(pathFileDecompressed));
            File.Delete(pathFileDecompressed);
            File.Delete(pathFileCompressed);

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