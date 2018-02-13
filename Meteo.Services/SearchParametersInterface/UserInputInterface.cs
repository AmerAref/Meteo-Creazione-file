using System;
using System.Collections.Generic;
using Meteo.Services;

namespace Meteo.Services.SearchParametersInterface
{
    public interface IUserInput<T>
    {
        bool Validate();
        T Parse();
    }
    public class UserResponse<T>
    {
        public T Value { get; set; }
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
        public abstract void GetParameters(string value);
        public abstract T Parse();
        public abstract TResponse GetResponse();
        public abstract bool Validate();
    }
}

