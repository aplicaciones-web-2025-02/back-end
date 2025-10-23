using System;

namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.ValueObjects
{
    public sealed class Duration
    {
        public TimeSpan Value { get; }

        public double TotalMinutes => Value.TotalMinutes;
        public double TotalHours => Value.TotalHours;

        public Duration(TimeSpan value)
        {
            Value = value;
        }

        public override string ToString() => Value.ToString();
    }
}
