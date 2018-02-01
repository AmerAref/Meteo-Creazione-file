using System;
namespace Meteo.UI.ForecastManager
{

    public class CoordinatesManager
    {
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string _menuLang;

        public CoordinatesManager ReadCoordinate()
        {
            var lat = "";
            var lon = "";
            if (_menuLang == "it")
            {
                Console.WriteLine(DataInterface.insertLatIT);
                lat = Console.ReadLine();
                Console.WriteLine(DataInterface.insertLonIT);
                lon = Console.ReadLine();
            }
            else
            {
                Console.WriteLine(DataInterface.insertLatEN);
                lat = Console.ReadLine();
                Console.WriteLine(DataInterface.insertLonEN);
                lon = Console.ReadLine();
            }
            var coordinates = new CoordinatesManager();
            coordinates.Lat = lat;
            coordinates.Lon = lon;



            return coordinates;

        }
    }

}
