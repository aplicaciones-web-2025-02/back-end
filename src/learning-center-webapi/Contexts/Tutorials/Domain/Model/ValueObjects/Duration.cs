namespace learning_center_webapi.Contexts.Tutorials.Domain.Model.ValueObjects;

public sealed class Duration
{
    public Duration(TimeSpan value)
    {
        Value = value;
    }

    public TimeSpan Value { get; }

    public double TotalMinutes => Value.TotalMinutes;
    public double TotalHours => Value.TotalHours;

    public override string ToString()
    {
        return Value.ToString();
    }
}