using System.Globalization;

namespace FuzzyLogic;

public class FuzzyNumber
{
    public FuzzyNumber(double value)
    {
        if (value is < 0.0 or > 1.0)
        {
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");
        }

        Value = value;
    }

    public double Value { get; }

    public static FuzzyNumber operator |(FuzzyNumber a, FuzzyNumber b) => new(Math.Max(a.Value, b.Value));

    public static FuzzyNumber operator &(FuzzyNumber a, FuzzyNumber b) => new(Math.Min(a.Value, b.Value));

    public static FuzzyNumber operator !(FuzzyNumber x) => new(1 - x.Value);

    public static FuzzyNumber operator *(FuzzyNumber a, FuzzyNumber b) => new(a.Value * b.Value);

    public static implicit operator FuzzyNumber(double x) => new(x);

    public static implicit operator double(FuzzyNumber x) => x.Value;

    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}