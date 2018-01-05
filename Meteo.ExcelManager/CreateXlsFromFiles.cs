using System;
using System.IO;
using System.Text;
using OfficeOpenXml;
using Newtonsoft.Json;
using Meteo.Services;
using System.Threading.Tasks;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Meteo.ExcelManager
{
    public class CreateXlsFromFiles
    {
        int i, j = 2, c = 1;
        public void CreateXlsFromFileForToday(string filePath, string xlsFile)
        {
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            string dataFromJson = System.IO.File.ReadAllText(@filePath);
            MeasureToday jsonObj = JsonConvert.DeserializeObject<MeasureToday>(dataFromJson);
            var jsonData = jsonObj.Main.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Meteo from file");

                for (i = 0; i < jsonData.Length; i++, c++)
                {
                    var valueWithoutSplit = jsonData.GetValue(i);
                    var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];

                    var valueForColoumn = jsonObj.Main.GetType().GetProperty(valueForHeader).GetValue(jsonObj.Main, null);
                    worksheet.Cells[1, c].Value = valueForHeader;
                    worksheet.Cells[2, c].Value = valueForColoumn;
                    worksheet.Cells[1, c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, c].Style.Fill.BackgroundColor.SetColor(Color.LawnGreen);
                }
                pkg.Save();
            }
        }

        public void CreateXlsFromFileFor5Days(string filePath, string xlsFile)
        {
            var newFile = new FileInfo("/home/gabriel/Scrivania/GitRepos/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            string dataFromJson = System.IO.File.ReadAllText(@filePath);
            ListMeasureLast5Day jsonObj = JsonConvert.DeserializeObject<ListMeasureLast5Day>(dataFromJson);
            var jsonData = jsonObj.List.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add($"Last five days meteo from file");
                foreach (var measure in jsonObj.List)
                {
                    var firstValueHeader = measure.Main.GetType().GetProperties();
                    for (i = 0; i < firstValueHeader.Length; i++, c++)
                    {
                        var valueWithoutSplit = firstValueHeader.GetValue(i);
                        var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];
                        if (c <= 5)
                        {
                            worksheet.Cells[1, c].Value = valueForHeader;
                            worksheet.Cells[1, c].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[1, c].Style.Fill.BackgroundColor.SetColor(Color.LawnGreen);
                        }
                    }
                }
                foreach (var measure in jsonObj.List)
                {
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