using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.ExcelManager
{
    public class CreateXlsFile
    {
        private readonly string _path = Directory.GetCurrentDirectory() + "/";
        int _row = 2;
        public void CreateXlsFileWithForecastData(List<Forecast> forecastData, string place, string lat, string lon, string xlsFile, string dateTime, string OneOrFiveDays)
        {
            var newFile = new FileInfo(_path + $@"{xlsFile}" + OneOrFiveDays + dateTime + ".xls");
            ExcelWorksheet worksheet;

            using (var pkg = new ExcelPackage(newFile))
            {
                if (place != null)
                {
                    worksheet = pkg.Workbook.Worksheets.Add($"{OneOrFiveDays} meteo for {place}");
                }
                else
                {
                    worksheet = pkg.Workbook.Worksheets.Add($"{OneOrFiveDays} meteo for Latitude: {lat} & Longitude: {lon}");
                }
                worksheet.Cells[1, 1].Value = "Pressure";
                worksheet.Cells[1, 2].Value = "Humidity";
                worksheet.Cells[1, 3].Value = "Temparature";
                worksheet.Cells[1, 4].Value = "Temperature Min";
                worksheet.Cells[1, 5].Value = "Temperature Max";
                worksheet.Cells[1, 6].Value = "City name";
                worksheet.Cells[1, 7].Value = "Weather date";

                foreach (var forecastResearch in forecastData)
                {
                    worksheet.Cells[_row, 1].Value = forecastResearch.Pressure;
                    worksheet.Cells[_row, 2].Value = forecastResearch.Humidity;
                    worksheet.Cells[_row, 3].Value = forecastResearch.Temperature;
                    worksheet.Cells[_row, 4].Value = forecastResearch.TemperatureMin;
                    worksheet.Cells[_row, 5].Value = forecastResearch.TemperatureMax;
                    worksheet.Cells[_row, 6].Value = forecastResearch.CityName;
                    worksheet.Cells[_row, 7].Value = forecastResearch.WeatherDate.ToString("yyyy-MM-dd HH:mm:ss");
                    _row++;
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileWithExportedData(List<Forecast> forecastResearch, string xlsFile, string dateTime, string exportChoice)
        {
            var newFile = new FileInfo(_path + $@"{xlsFile}" + "Exported" + dateTime + ".xls");
            ExcelWorksheet worksheet;

            using (var pkg = new ExcelPackage(newFile))
            {
                if (exportChoice == "1")
                {
                    worksheet = pkg.Workbook.Worksheets.Add("Exported data");
                }
                else
                {
                    worksheet = pkg.Workbook.Worksheets.Add("Exported filtered data");
                }

                worksheet.Cells[1, 1].Value = "Pressure";
                worksheet.Cells[1, 2].Value = "Humidity";
                worksheet.Cells[1, 3].Value = "Temperature";
                worksheet.Cells[1, 4].Value = "Temp Min";
                worksheet.Cells[1, 5].Value = "Temp Max";
                worksheet.Cells[1, 6].Value = "City Name";
                worksheet.Cells[1, 7].Value = "Date Of Research";

                foreach (var forecast in forecastResearch)
                {
                    worksheet.Cells[_row, 1].Value = forecast.Pressure;
                    worksheet.Cells[_row, 2].Value = forecast.Humidity;
                    worksheet.Cells[_row, 3].Value = forecast.Temperature;
                    worksheet.Cells[_row, 4].Value = forecast.TemperatureMin;
                    worksheet.Cells[_row, 5].Value = forecast.TemperatureMax;
                    worksheet.Cells[_row, 6].Value = forecast.CityName;
                    worksheet.Cells[_row, 7].Value = forecast.WeatherDate.ToString("yyyy-MM-dd hh:mm:ss");
                    _row++;
                }
                pkg.Save();
            }
        }
    }
}