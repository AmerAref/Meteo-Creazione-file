﻿using System;
namespace Meteo.Services
{
    public interface IUserInput<T>
    {
        bool Validate();
        T Parse();
    }
    public class UserResponse<T>
    {
        T Value { get; set; }
        public UserResponse(T obj)
        {
            Value = obj;
        }
    }

    public abstract class UserInput<T, TResponse> : IUserInput<T> where TResponse : UserResponse<T>
    {
        private string _input;
        protected UserInput(string input)
        {
            _input = input;
        }

        public abstract bool Validate(T userInput);
        public bool CheckSpecialCharacter()
        {
            if (_input.Contains("%"))
            {
                return false;
            }
            return true;
        }

        public abstract T Parse();
        public abstract TResponse GetResponse();

        public abstract bool Validate();
    }

    public class DateTimeUserInput : UserInput<DateTime, UserResponse<DateTime>>
    {
        private string _input;

        public DateTimeUserInput(string input) : base(input)
        {
            _input = input;
        }

        public override DateTime Parse()
        {
            DateTime result;
            var check = DateTime.TryParse(_input, out result);
            return result;
        }

        public override bool Validate(DateTime date)
        {
            if (date.Year < 2018)
            {
                return false;
            }
            return true;
        }
        public override UserResponse<DateTime> GetResponse()
        {
            var date = Parse();
            var validate = Validate(date);
            if (validate)
            {
                return new UserResponse<DateTime>(date);
            }
            return null;
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }

    public class CoordinatesUserInput : UserInput<double, UserResponse<double>>
    {
        private string _input;

        public CoordinatesUserInput(string input) : base(input)
        {
            _input = input;
        }
        public override double Parse()
        {
            double result;
            var check = double.TryParse(_input.Replace(',', '.'), out result);
            return result;
        }

        public override bool Validate(double coordinate)
        {
            if(coordinate >=-180 && coordinate<=180)
            {
                return false;
            }
            return true;
        }
        public override UserResponse<double> GetResponse()
        {
            var coordinate = Parse();
            var validate = Validate(coordinate);
            if (validate)
            {
                return new UserResponse<double>(coordinate);
            }
            return null;
        }

        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}