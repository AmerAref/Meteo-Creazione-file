using System;
using System.IO;
using OfficeOpenXml;
using Newtonsoft.Json;
using System.Drawing;
using Meteo.Services.OpenWeatherMap.Models;
using OfficeOpenXml.Style;

namespace Meteo.ExcelManager
{
    public class CreateXlsFromFiles
    {
        private int _i;
        private int _j = 2;
        private int _c = 1;

        public void CreateXlsFromFileForToday(string filePath, string xlsFile)
        {
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            var dataFromJson = File.ReadAllText(@filePath);
            var jsonObj = JsonConvert.DeserializeObject<OneDayForecast>(dataFromJson);
            var jsonData = jsonObj.Parameters.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add("Meteo from file");

                for (_i = 0; _i < jsonData.Length; _i++, _c++)
                {
                    var valueWithoutSplit = jsonData.GetValue(_i);
                    var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];

                    var valueForColoumn = jsonObj.Parameters.GetType().GetProperty(valueForHeader).GetValue(jsonObj.Parameters, null);
                    worksheet.Cells[1, _c].Value = valueForHeader;
                    worksheet.Cells[2, _c].Value = valueForColoumn;
                    worksheet.Cells[1, _c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, _c].Style.Fill.BackgroundColor.SetColor(Color.LawnGreen);
                }
                pkg.Save();
            }
        }

        public void CreateXlsFromFileFor5Days(string filePath, string xlsFile)
        {
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            var dataFromJson = File.ReadAllText(@filePath);
            var jsonObj = JsonConvert.DeserializeObject<LastFiveDaysForecast>(dataFromJson);
            var jsonData = jsonObj.List.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add($"Last five days meteo from file");
                foreach (var measure in jsonObj.List)
                {
                    var firstValueHeader = measure.Parameters.GetType().GetProperties();
                    for (_i = 0; _i < firstValueHeader.Length; _i++, _c++)
                    {
                        var valueWithoutSplit = firstValueHeader.GetValue(_i);
                        var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];
                        if (_c <= 5)
                        {
                            worksheet.Cells[1, _c].Value = valueForHeader;
                            worksheet.Cells[1, _c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[1, _c].Style.Fill.BackgroundColor.SetColor(Color.LawnGreen);
                        }
                    }
                }
                foreach (var measure in jsonObj.List)
                {
                    worksheet.Cells[_j, 1].Value = measure.Parameters.Pressure;
                    worksheet.Cells[_j, 2].Value = measure.Parameters.Temp;
                    worksheet.Cells[_j, 3].Value = measure.Parameters.Humidity;
                    worksheet.Cells[_j, 4].Value = measure.Parameters.TempMin;
                    worksheet.Cells[_j, 5].Value = measure.Parameters.TempMax;
                    _j++;
                }
                pkg.Save();
            }
        }
    }
}