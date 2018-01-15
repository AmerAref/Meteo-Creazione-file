using System;

namespace Meteo.Services
{
    public static class DataMaskManager
    {
        public static string MaskData(string dataNotMaskered)
        {
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (key.Key != ConsoleKey.Enter)
                {

                    if (key.Key == ConsoleKey.Backspace)
                    {
                        dataNotMaskered = dataNotMaskered.Remove(dataNotMaskered.Length - 1, 1);
                        ClearCurrentConsoleLine();
                        foreach (var i in dataNotMaskered)
                        {
                            Console.Write("*");
                        }
                    }
                    else
                    {
                        dataNotMaskered += key.KeyChar;
                        Console.Write("*");
                    }
                }
            }
            while (key.Key != ConsoleKey.Enter);
            return dataNotMaskered;
        }
        private static void ClearCurrentConsoleLine()
        {
            var currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}