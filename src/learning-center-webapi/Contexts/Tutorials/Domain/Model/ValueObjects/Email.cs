using System;
using System.Text.RegularExpressions;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Invalid email format.");
            Value = value;
        }
        public override string ToString() => Value;
    }
}
