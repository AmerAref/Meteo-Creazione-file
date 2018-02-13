﻿using Meteo.Services.CityJsonModels;
using System;
using System.Collections.Generic;

namespace Meteo.Services.SearchParametersInterface
{
    public class UserSearchInput : UserInput<object, UserResponse<object>>
    {
        public Coordinates coordate = new Coordinates();
        public CitiesJson city = new CitiesJson();

        List<string> _param = new List<string>();


        Dictionary<string, string> data = new Dictionary<string, string>();
        private string _input;



        public UserSearchInput(string input) : base(input)
        {
            _input = input;
        }

        public override Object Parse()
        {

            var inputFirstParse = _input.Split('&');

            foreach (var reciveKeyValue in inputFirstParse)
            {
                var keyAndValue = reciveKeyValue.Split('=');
                data.Add(keyAndValue[0], keyAndValue[1]);
            }

            foreach (var dataValue in data)
            {
                if (dataValue.Key == "lat")
                {
                    var latWithReplace = dataValue.Value.Replace(".", ",");
                    var lat = Convert.ToDouble(latWithReplace);
                    _param.Add(latWithReplace);
                    coordate.Latitude = lat;
                }
                if (dataValue.Key == "lon")
                {
                    var lonWithReplace = dataValue.Value.Replace(".", ",");

                    var lon = Convert.ToDouble(lonWithReplace);

                    _param.Add(lonWithReplace);

                    coordate.Longitude = lon;
                }

                if (dataValue.Key == "place")
                {
                    var place = dataValue.Value;
                    var key = dataValue.Key;

                    _param.Add(place);
                    _param.Add(key);
                    city.Name = place;
                }

                if (dataValue.Key == "country")
                {
                    var country = dataValue.Value;
                    var key = dataValue.Key;
                    _param.Add(country);
                    city.Country = country;
                }
            }
            return coordate;
        }


        public override bool Validate()
        {

            var lat = Convert.ToDouble(_param[0]);
            var lon = Convert.ToDouble(_param[1]);
            if (lat >= -90.0 && lat <= 90.0 && lon >= -180.0 && lon <= 180.0)
            {
                return true;
            }

            return false;
        }

        public override UserResponse<object> GetResponse()
        {
            Parse();
            var validate = false;
            if (_param != null)
            {
                return new UserResponse<object>(city);
            }
            else
            {
                validate = Validate();

                if (validate)
                {
                    return new UserResponse<object>(coordate);
                }
            }


            return null;
        }



        public override bool Validate(object userInput)
        {
            throw new NotImplementedException();
        }

        public override void GetParameters(string value)
        {
            _input = value;
        }
    }
}