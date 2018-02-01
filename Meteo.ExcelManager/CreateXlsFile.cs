using System;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using Meteo.Services.OpenWeatherMap.Models;
using System.Collections.Generic;

namespace Meteo.ExcelManager
{
    public class CreateXlsFile
    {
        private readonly string _path = Directory.GetCurrentDirectory() + "/";
        int _i, _j = 2, _c = 1;
        public void CreateXlsFileForToday(OneDayForecast jsonObjForExcel, string place, string xlsFile, string dateTime)
        {
            var newFile = new FileInfo(_path + $@"{xlsFile}" + "1Day" + dateTime + ".xls");

            var firstValueHeader = jsonObjForExcel.Parameters.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add($"Today's meteo for {place}");

                for (_i = 0; _i < firstValueHeader.Length; _i++, _c++)
                {
                    var valueWithoutSplit = firstValueHeader.GetValue(_i);
                    var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];

                    var valueForColoumn = jsonObjForExcel.Parameters.GetType().GetProperty(valueForHeader).GetValue(jsonObjForExcel.Parameters, null);
                    worksheet.Cells[1, _c].Value = valueForHeader;
                    worksheet.Cells[2, _c].Value = valueForColoumn;
                    worksheet.Cells[1, _c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, _c].Style.Fill.BackgroundColor.SetColor(Color.LawnGreen);
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileForTodayByCoordinates(OneDayForecast jsonObjForExcel, string lat, string lon, string xlsFile, string dateTime)
        {
            var newFile = new FileInfo(_path + $@"{xlsFile}" + "1Day" + dateTime + ".xls");

            var firstValueHeader = jsonObjForExcel.Parameters.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add($"Today's meteo for Latitude: {lat} & Longitude: {lon}");

                for (_i = 0; _i < firstValueHeader.Length; _i++, _c++)
                {
                    var valueWithoutSplit = firstValueHeader.GetValue(_i);
                    var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];

                    var valueForColoumn = jsonObjForExcel.Parameters.GetType().GetProperty(valueForHeader).GetValue(jsonObjForExcel.Parameters, null);
                    worksheet.Cells[1, _c].Value = valueForHeader;
                    worksheet.Cells[2, _c].Value = valueForColoumn;
                    worksheet.Cells[1, _c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, _c].Style.Fill.BackgroundColor.SetColor(Color.LawnGreen);
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileForLast5Days(LastFiveDaysForecast jsonObjForExcel, string place, string xlsFile, string dateTime)
        {

            var newFile = new FileInfo(_path + $@"{xlsFile}" + "5Days" + dateTime + ".xls");

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add($"Last five days meteo for {place}");

                foreach (var forecast in jsonObjForExcel.List)
                {
                    var firstValueHeader = forecast.Parameters.GetType().GetProperties();
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

                foreach (var forecast in jsonObjForExcel.List)
                {
                    var parameters = forecast.Parameters;
                    worksheet.Cells[_j, 1].Value = parameters.Pressure;
                    worksheet.Cells[_j, 2].Value = parameters.Temp;
                    worksheet.Cells[_j, 3].Value = parameters.Humidity;
                    worksheet.Cells[_j, 4].Value = parameters.TempMin;
                    worksheet.Cells[_j, 5].Value = parameters.TempMax;
                    _j++;
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileForLast5DaysByCoordinates(LastFiveDaysForecast jsonObjForExcel, string lat, string lon, string xlsFile, string dateTime)
        {

            var newFile = new FileInfo(_path + $@"{xlsFile}" + "5Days" + dateTime + ".xls");

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add($"Last five days meteo for Latitude: {lat} & Longitude: {lon}");
                foreach (var measure in jsonObjForExcel.List)
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
                foreach (var measure in jsonObjForExcel.List)
                {
                    Console.WriteLine(measure.Parameters.Pressure);
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
        public void CreateXlsFileWithExportedOneDayData(List<Services.Models.OneDayForecast> oneDayResearch, List<Services.Models.Forecast> forecastResearch, string xlsFile, string dateTime)
        {
            var newFile = new FileInfo(_path + $@"{xlsFile}" + "1DayExported" + dateTime + ".xls");

            using (var pkg = new ExcelPackage(newFile))
            {
                var worksheet = pkg.Workbook.Worksheets.Add("Exported data");
                worksheet.Cells[1, 1].Value = "Pressure";
                worksheet.Cells[1, 2].Value = "Humidity";
                worksheet.Cells[1, 3].Value = "Temperature";
                worksheet.Cells[1, 4].Value = "Temp Min";
                worksheet.Cells[1, 5].Value = "Temp Max";
                worksheet.Cells[1, 6].Value = "City Name";
                worksheet.Cells[1, 7].Value = "Date Of Research";

                foreach (var oneDay in oneDayResearch)
                {
                    worksheet.Cells[_j, 1].Value = oneDay.Pressure;
                    worksheet.Cells[_j, 2].Value = oneDay.Humidity;
                    worksheet.Cells[_j, 3].Value = oneDay.Temp;
                    worksheet.Cells[_j, 4].Value = oneDay.TempMin;
                    worksheet.Cells[_j, 5].Value = oneDay.TempMax;
                    _j++;
                }
                _j = 2;
                foreach (var forecast in forecastResearch)
                {
                    worksheet.Cells[_j, 6].Value = forecast.CityName;
                    worksheet.Cells[_j, 7].Value = forecast.TimeStamp.ToString();
                    _j++;
                }
                pkg.Save();
            }
        }
    }
}