using System;
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

        public abstract bool Validate(DateTime date);
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

}
