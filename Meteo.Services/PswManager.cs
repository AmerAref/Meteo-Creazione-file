using System;
using System.Text.RegularExpressions;
namespace Meteo.Services
{
    public static class PswManager
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
        public static string MaskPasswordLogin(string passwordLogin)
        {
            {
                ConsoleKeyInfo key;

                do
                {
                    key = Console.ReadKey(true);

                    if (key.Key != ConsoleKey.Enter)
                    {
                        if (key.Key == ConsoleKey.Backspace)
                        {
                            passwordLogin = passwordLogin.Remove(passwordLogin.Length - 1, 1);
                            ClearCurrentConsoleLine();
                            foreach (var i in passwordLogin)
                            {
                                Console.Write("*");

                            }

                        }
                        else
                        {
                            passwordLogin += key.KeyChar;
                            Console.Write("*");

                        }

                    }

                }
                while (key.Key != ConsoleKey.Enter);
                return passwordLogin;

            }
        }

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static bool CheckPassword(string password)
        {
            string MatchEmailPattern = "^(?=.*[a - z])(?=.*[A - Z])(?=.*\"d)(?=.*[#$^+=!*()@%&]).{8,}$";

            if (password != null)

                return Regex.IsMatch(password, MatchEmailPattern);

            else 

                return false;


        }
    }
}



