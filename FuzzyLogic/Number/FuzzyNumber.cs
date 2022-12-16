using System.Globalization;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.FuzzyNumber>;

namespace FuzzyLogic.Number;

public readonly record struct FuzzyNumber : IFuzzyNumber<FuzzyNumber>
{
    private static readonly FuzzyNumber Min = Of(0);
    private static readonly FuzzyNumber Max = Of(1);

    private FuzzyNumber(double value)
    {
        if (Math.Abs(0 - value) < Tolerance) Value = 0;
        if (Math.Abs(1 - value) < Tolerance) Value = 1;
        RangeCheck(value);
        Value = value;
    }

    public double Value { get; }

    public static FuzzyNumber Of(double value) => new(value);

    public static bool TryCreate(double number, out FuzzyNumber fuzzyNumber)
    {
        try
        {
            fuzzyNumber = Of(number);
            return true;
        }
        catch (ArgumentException)
        {
            fuzzyNumber = number < 0 ? Math.Max(0, number) : Math.Min(1, number);
            return false;
        }
    }

    public static FuzzyNumber MinValue() => Min;

    public static FuzzyNumber MaxValue() => Max;

    public static FuzzyNumber operator |(FuzzyNumber a, FuzzyNumber b) => new(Math.Max(a.Value, b.Value));

    public static FuzzyNumber operator &(FuzzyNumber a, FuzzyNumber b) => new(Math.Min(a.Value, b.Value));

    public static FuzzyNumber operator !(FuzzyNumber x) => new(1 - x.Value);

    public static FuzzyNumber Implication(FuzzyNumber a, FuzzyNumber b) =>
        Math.Max(1 - a.Value, Math.Min(a.Value, b.Value));

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="double" /> value to a <see cref="FuzzyNumber" />. Note that
    ///     this value must be in the range μ(x) ∈ [0, 1] (see <see cref="FuzzyNumber(double)" /> for reference).
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="FuzzyNumber" /> value.</returns>
    public static implicit operator FuzzyNumber(double x) => Of(x);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="FuzzyNumber" /> to a <see cref="double" /> value.
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="FuzzyNumber" /> value.</returns>
    public static implicit operator double(FuzzyNumber x) => x.Value;

    /// <inheritdoc />
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

    private static void RangeCheck(double value)
    {
        if (value is < 0.0 or > 1.0)
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");
    }
}