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
        int i, j, c = 1, r = 2;
        public void CreateXlsFileForToday(string fileNameExcel, MeasureToday jsonObjForExcel, string place)
        {
            path = $"/home/gabriel/Scrivania/Progetti/Meteo-Creazione-file/Meteo.UI/{fileNameExcel}";
            Console.WriteLine("Insert name of the XLS file");
            xlsFile = Console.ReadLine();
            var newFile = new FileInfo("/home/gabriel/Scrivania/Progetti/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            var firstValueHeader = jsonObjForExcel.Main.GetType().GetProperties();

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add($"Meteo for {place}");

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

        public void CreateXlsFileForLast5Days(string fileNameExcel, ListMeasureLast5Day jsonObjForExcel)
        {
            path = $"/home/gabriel/Scrivania/Progetti/Meteo-Creazione-file/Meteo.UI/{fileNameExcel}";
            Console.WriteLine("Inserisci il nome del file XLS:");
            xlsFile = Console.ReadLine();
            var newFile = new FileInfo("/home/gabriel/Scrivania/Progetti/Meteo-Creazione-file/Meteo.UI/" + $@"{xlsFile}" + ".xls");

            using (var pkg = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pkg.Workbook.Worksheets.Add("Last5DaysMeteo for");
                foreach (var measure in jsonObjForExcel.List)
                {
                    var firstValueHeader = measure.Main.GetType().GetProperties();
                    for (i = 1; i < 6; i++)
                    {
                        for (j = 2; j < 42; j++)
                        {
                            var valueWithoutSplit = firstValueHeader.GetValue(i);
                            var valueForHeader = Convert.ToString(valueWithoutSplit).Split(' ')[1];
                            worksheet.Cells[1, i].Value = valueForHeader;

                            var valueForColoumn = measure.Main.GetType().GetProperty(valueForHeader).GetValue(measure.Main, null);
                            worksheet.Cells[j, i].Value = valueForColoumn;
                        }

                        /* Console.WriteLine(valueForHeader);
                        for (var c = 2; c < 42; c++)
                        {
                            for (var j = 3; j < 5; j++)

                            {  

                                worksheet.Cells [c,j].Value=valueForColoumn;
                                
                            }
                        } */
                    }
                }
                pkg.Save();
            }
        }
    }
}