using System;
using System.IO;
using System.Text;
using OfficeOpenXml;
using Newtonsoft.Json;
using Meteo.Services;

namespace Meteo.ExcelManager
{
    public class CreateXlsFile
    {
        string path, xlsFile;
        int i, j = 2, c = 1;
        public void CreateXlsFileForToday(string fileNameExcel, MeasureToday jsonObjForExcel, string place)
        {
            path = $"/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/{fileNameExcel}";
            Console.WriteLine("Insert name of the XLS file");
            xlsFile = Console.ReadLine();
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            var firstValueHeader = jsonObjForExcel.Main.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add($"Today's meteo for {place}");

                for (i = 0; i < firstValueHeader.Length; i++, c++)
                {
                    var valueWithoutSplit = firstValueHeader.GetValue(i);
                    var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];

                    var valueForColoumn = jsonObjForExcel.Main.GetType().GetProperty(valueForHeader).GetValue(jsonObjForExcel.Main, null);
                    worksheet.Cells[1, c].Value = valueForHeader;
                    worksheet.Cells[2, c].Value = valueForColoumn;
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileForTodayByCoordinates(string fileNameExcel, MeasureToday jsonObjForExcel, string lat, string lon)
        {
            path = $"/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/{fileNameExcel}";
            Console.WriteLine("Insert name of the XLS file");
            xlsFile = Console.ReadLine();
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            var firstValueHeader = jsonObjForExcel.Main.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add($"Today's meteo for Latitude: {lat} & Longitude: {lon}");

                for (i = 0; i < firstValueHeader.Length; i++, c++)
                {
                    var valueWithoutSplit = firstValueHeader.GetValue(i);
                    var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];

                    var valueForColoumn = jsonObjForExcel.Main.GetType().GetProperty(valueForHeader).GetValue(jsonObjForExcel.Main, null);
                    worksheet.Cells[1, c].Value = valueForHeader;
                    worksheet.Cells[2, c].Value = valueForColoumn;
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileForLast5Days(string fileNameExcel, ListMeasureLast5Day jsonObjForExcel, string place)
        {
            path = $"/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/{fileNameExcel}";
            Console.WriteLine("Inserisci il nome del file XLS:");
            xlsFile = Console.ReadLine();
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add($"Last five days meteo for {place}");
                foreach (var measure in jsonObjForExcel.List)
                {
                    var firstValueHeader = measure.Main.GetType().GetProperties();
                    for (i = 0; i < firstValueHeader.Length; i++, c++)
                    {
                        var valueWithoutSplit = firstValueHeader.GetValue(i);
                        var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];
                        if (c <= 5)
                        {
                            worksheet.Cells[1, c].Value = valueForHeader;
                        }
                    }
                }
                foreach (var measure in jsonObjForExcel.List)
                {
                    Console.WriteLine(measure.Main.Pressure);
                    worksheet.Cells[j, 1].Value = measure.Main.Pressure;
                    worksheet.Cells[j, 2].Value = measure.Main.Temp;
                    worksheet.Cells[j, 3].Value = measure.Main.Humidity;
                    worksheet.Cells[j, 4].Value = measure.Main.TempMin;
                    worksheet.Cells[j, 5].Value = measure.Main.TempMax;
                    j++;
                }
                pkg.Save();
            }
        }

        public void CreateXlsFileForLast5DaysByCoordinates(string fileNameExcel, ListMeasureLast5Day jsonObjForExcel, string lat, string lon) {
            path = $"/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/{fileNameExcel}";
            Console.WriteLine("Inserisci il nome del file XLS:");
            xlsFile = Console.ReadLine();
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add($"Last five days meteo for Latitude: {lat} & Longitude: {lon}");
                foreach (var measure in jsonObjForExcel.List)
                {
                    var firstValueHeader = measure.Main.GetType().GetProperties();
                    for (i = 0; i < firstValueHeader.Length; i++, c++)
                    {
                        var valueWithoutSplit = firstValueHeader.GetValue(i);
                        var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];
                        if (c <= 5)
                        {
                            worksheet.Cells[1, c].Value = valueForHeader;
                        }
                    }
                }
                foreach (var measure in jsonObjForExcel.List)
                {
                    Console.WriteLine(measure.Main.Pressure);
                    worksheet.Cells[j, 1].Value = measure.Main.Pressure;
                    worksheet.Cells[j, 2].Value = measure.Main.Temp;
                    worksheet.Cells[j, 3].Value = measure.Main.Humidity;
                    worksheet.Cells[j, 4].Value = measure.Main.TempMin;
                    worksheet.Cells[j, 5].Value = measure.Main.TempMax;
                    j++;
                }
                pkg.Save();
            }
        }
    }
}