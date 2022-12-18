using System.Globalization;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.ProductNumber>;

namespace FuzzyLogic.Number;

public readonly record struct ProductNumber : IFuzzyNumber<ProductNumber>, IComparable<ProductNumber>
{
    private static readonly ProductNumber Min = Of(0);
    private static readonly ProductNumber Max = Of(1);

    private ProductNumber(double value)
    {
        if (Math.Abs(0 - value) < Tolerance) Value = 0;
        if (Math.Abs(1 - value) < Tolerance) Value = 1;
        RangeCheck(value);
        Value = value;
    }

    public double Value { get; }

    public static ProductNumber Of(double value) => new(value);

    public static bool TryCreate(double number, out ProductNumber fuzzyNumber)
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

    public static ProductNumber MinValue() => Min;

    public static ProductNumber MaxValue() => Max;

    public static ProductNumber operator !(ProductNumber x) => (1 - x.Value);

    public static ProductNumber operator &(ProductNumber a, ProductNumber b) => (a.Value * b.Value);

    public static ProductNumber operator |(ProductNumber a, ProductNumber b) => (a.Value + b.Value - a.Value * b.Value);

    public static ProductNumber Implication(ProductNumber a, ProductNumber b) =>
        a.Value <= b.Value ? 1 : (b.Value / a.Value);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="double" /> value to a <see cref="ProductNumber" />.
    ///     Note that this value must be in the range μ(x) ∈ [0, 1].
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="ProductNumber" /> value.</returns>
    public static implicit operator ProductNumber(double x) => new(x);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="ProductNumber" /> to a <see cref="double" /> value.
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="ProductNumber" /> value.</returns>
    public static implicit operator double(ProductNumber x) => x.Value;

    public int CompareTo(ProductNumber other) => Value.CompareTo(other.Value);

    /// <inheritdoc />
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

    private static void RangeCheck(double value)
    {
        if (value is < 0.0 or > 1.0)
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");
    }
}