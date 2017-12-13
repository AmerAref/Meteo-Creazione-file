using System;
using System.Net.Mail;
using System.IO;
namespace Meteo.Services
{
    public class PswManager
    {
        public string MaskPassword(string passwordVisibile)
        {
            {
                Console.WriteLine("Inserisci password");
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