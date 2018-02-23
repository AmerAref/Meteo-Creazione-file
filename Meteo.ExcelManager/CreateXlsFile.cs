﻿using System.IO;
using OfficeOpenXml;
using System.Collections.Generic;
using Meteo.Services.Models;

namespace Meteo.ExcelManager
{
    public class CreateXlsFile
    {
        private readonly string _destinationPath = Directory.GetCurrentDirectory() + "/";
        int _row = 2;
        public void CreateXlsFileWithForecastData(List<Forecast> forecastResearchData, string place, string latitude, string longitude, string xlsFileName, string dateTime, string oneOrFiveDaysChoice)
        {
            var newFile = new FileInfo(_destinationPath + $@"{xlsFileName}" + oneOrFiveDaysChoice + dateTime + ".xls");
            ExcelWorksheet worksheet;

            using (var pkg = new ExcelPackage(newFile))
            {
                if (place != null)
                {
                    worksheet = pkg.Workbook.Worksheets.Add($"{oneOrFiveDaysChoice} meteo for {place}");
                }
                else
                {
                    worksheet = pkg.Workbook.Worksheets.Add($"{oneOrFiveDaysChoice} meteo for Latitude: {latitude} & Longitude: {longitude}");
                }
                worksheet.Cells["A1:G1"].Style.Font.Bold = true;
                worksheet.Cells["A1:G1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGray);
                for (int i = 1; i < 8; i++)
                { worksheet.Column(i).Width = 20; }

                worksheet.Cells[1, 1].Value = "Pressure";
                worksheet.Cells[1, 2].Value = "Humidity";
                worksheet.Cells[1, 3].Value = "Temparature";
                worksheet.Cells[1, 4].Value = "Temperature Min";
                worksheet.Cells[1, 5].Value = "Temperature Max";
                worksheet.Cells[1, 6].Value = "City name";
                worksheet.Cells[1, 7].Value = "Weather date";

                foreach (var researchData in forecastResearchData)
                {
                    worksheet.Cells[_row, 1].Value = researchData.Pressure;
                    worksheet.Cells[_row, 2].Value = researchData.Humidity;
                    worksheet.Cells[_row, 3].Value = researchData.Temperature;
                    worksheet.Cells[_row, 4].Value = researchData.TemperatureMin;
                    worksheet.Cells[_row, 5].Value = researchData.TemperatureMax;
                    worksheet.Cells[_row, 6].Value = researchData.CityName;
                    worksheet.Cells[_row, 7].Value = researchData.WeatherDate.ToString("yyyy-MM-dd HH:mm:ss");
                    _row++;
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileWithExportedData(List<MeasureValue> humidityData, List<MeasureValue> pressureData, List<MeasureValue> temperatureData, List<MeasureValue> tempMinData, List<MeasureValue> tempMaxData, string xlsFileName, string dateTime, string exportChoice)
        {
            var dataTypeName = "";
            if (exportChoice == "1")
            { dataTypeName = "Exported"; }
            else
            { dataTypeName = "ExportedFiltered"; }
            var newFile = new FileInfo(_destinationPath + $@"{xlsFileName}" + dataTypeName + dateTime + ".xls");
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

                worksheet.Cells["A1:G1"].Style.Font.Bold = true;
                worksheet.Cells["A1:G1"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A1:G1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGray);
                for (int i = 1; i < 8; i++)
                { worksheet.Column(i).Width = 20; }
                
                worksheet.Cells[1, 1].Value = "Pressure";
                worksheet.Cells[1, 2].Value = "Humidity";
                worksheet.Cells[1, 3].Value = "Temperature";
                worksheet.Cells[1, 4].Value = "Temp Min";
                worksheet.Cells[1, 5].Value = "Temp Max";

                foreach (var humidity in humidityData)
                {
                    worksheet.Cells[_row, 1].Value = humidity.Value;
                    _row++;
                }
                _row = 2;
                foreach (var pressure in pressureData)
                {
                    worksheet.Cells[_row, 2].Value = pressure.Value;
                    _row++;
                }
                _row = 2;
                foreach (var temperature in temperatureData)
                {
                    worksheet.Cells[_row, 3].Value = temperature.Value;
                    _row++;
                }
                _row = 2;
                foreach (var tempMin in tempMinData)
                {
                    worksheet.Cells[_row, 4].Value = tempMin.Value;
                    _row++;
                }
                _row = 2;
                foreach (var tempMax in tempMaxData)
                {
                    worksheet.Cells[_row, 5].Value = tempMax.Value;
                    _row++;
                }
                _row = 2;
                pkg.Save();
            }
        }
    }
}