using System;
using System.Net.Mail;
using System.IO;
namespace Meteo.Services
{
    public class PswManager
    {
        public static string MaskPassword(string passwordVisibile)
        {
            {
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        passwordVisibile += key.KeyChar;
                        Console.Write("*");
                    }
                }
                while (key.Key != ConsoleKey.Enter);
                return passwordVisibile;

            }
        }
    }
}