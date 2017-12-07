using System;
using Meteo.Services;

namespace Meteo.UI
{
    public class Menu
    {
        public void ShowFirst()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu    ***********");
            Console.WriteLine("****1 - Cerca dati relativi ad oggi                       *");
            Console.WriteLine("****2 -  Cerca dati riferiti agli ultimi 5 giorni         *");
            Console.WriteLine("****3 - Termina sessione                                  *");
            Console.WriteLine("***********************************************************");
        }
        public void ShowMenu()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu (2)***********");
            Console.WriteLine("****1 - Cerca dati relativi a Città                       *");
            Console.WriteLine("****2 -  Cerca dati relativi a Coordinate                 *");
            Console.WriteLine("****3 - Torna al menu principale                          *");
            Console.WriteLine("****4- Termina sessione                                   *");
            Console.WriteLine("***********************************************************");
        }

        public void InsertNamePlace()
        {
            Console.WriteLine("inserisci nome città");
        }
        public void InsertCoordinates()
        {
            Console.WriteLine("inserisci longitudine e latitudine");
        }
        public void ShowFiltredMenu()
        {
            Console.WriteLine("***********************************************************");
            Console.WriteLine("*************    Digitare il valore del menu(3) ***********");
            Console.WriteLine("****1 - Filtra dati relativi a umidità                    *");
            Console.WriteLine("****2 -  Filtra dati per data e/o ora                     *");
            Console.WriteLine("****3 - Filtra per Qualità cielo                          *");
            Console.WriteLine("****4 - Torna al menu secondario                          *");
            Console.WriteLine("***********************************************************");
        }
        public void PrintForData(MeasureForToday jsonObj)
        {
            Console.WriteLine(jsonObj.main.pressure);
            Console.WriteLine("Temperature");
            Console.WriteLine(jsonObj.main.temp);
            Console.WriteLine("Humidity");
            Console.WriteLine(jsonObj.main.humidity);
            Console.WriteLine("Temperature min");
            Console.WriteLine(jsonObj.main.temp_min);
            Console.WriteLine("Temperature max");
            Console.WriteLine(jsonObj.main.temp_max);
        }
        public void PrintDataLast5Day(Repo jsonObj)
        {
            foreach (var measure in jsonObj.list)
            {
                Console.WriteLine("Pressure");
                Console.WriteLine(measure.main.pressure);
                Console.WriteLine("Temperature");
                Console.WriteLine(measure.main.temp);
                Console.WriteLine("Humidity");
                Console.WriteLine(measure.main.humidity);
                Console.WriteLine("Temperature min");
                Console.WriteLine(measure.main.temp_min);
                Console.WriteLine("Temperature max");
                Console.WriteLine(measure.main.temp_max);
            }
        }
    }
}