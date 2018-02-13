using System;
using System.Collections.Generic;
using System.Text;

namespace Meteo.Services.SearchParametersInterface
{
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
            if (check)
            {
                return result;
            }
            return DateTime.MinValue;
        }

        public override bool Validate(DateTime userInput)
        {
            var date = Convert.ToDateTime(userInput);
            if (date.Year < DateTime.Now.Year)
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

        public override void GetParameters(string value)
        {
            throw new NotImplementedException();
        }
    }
}
