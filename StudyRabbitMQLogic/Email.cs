using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudyRabbitMQLogic
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Email>("Значение является обязательным!");

            if (!Regex.IsMatch(value, "^\\S+@\\S+\\.\\S+$"))
                return Result.Failure<Email>("Значение не является валидным Email!");

            return Result.Success(new Email(value));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }

        public static explicit operator Email(string value)
        {
            return Create(value).Value;
        }
    }
}
