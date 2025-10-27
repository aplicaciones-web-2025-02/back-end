using System.Text.RegularExpressions;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.ValueObjects;

public sealed class Email
{
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid email format.");
        Value = value;
    }

    public string Value { get; }

    public override string ToString()
    {
        return Value;
    }
}