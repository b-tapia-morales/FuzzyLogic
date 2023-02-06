using static System.Math;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.LukasiewiczNumber>;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

public readonly record struct LukasiewiczNumber : IFuzzyNumber<LukasiewiczNumber>
{
    private static readonly LukasiewiczNumber Min = Of(0);
    private static readonly LukasiewiczNumber Max = Of(1);

    private LukasiewiczNumber(double value)
    {
        if (Abs(0 - value) < Tolerance) Value = 0;
        if (Abs(1 - value) < Tolerance) Value = 1;
        RangeCheck(value);
        Value = value;
    }

    public double Value { get; }

    public static LukasiewiczNumber Of(double value) => new(value);

    public static bool TryCreate(double number, out LukasiewiczNumber fuzzyNumber)
    {
        try
        {
            fuzzyNumber = Of(number);
            return true;
        }
        catch (ArgumentException)
        {
            fuzzyNumber = number < 0 ? Max(0, number) : Min(1, number);
            return false;
        }
    }

    public static LukasiewiczNumber MinValue() => Min;

    public static LukasiewiczNumber MaxValue() => Max;

    public static LukasiewiczNumber operator !(LukasiewiczNumber x) => 1 - x.Value;

    public static LukasiewiczNumber operator &(LukasiewiczNumber x, LukasiewiczNumber y) =>
        Max(0, x.Value + y.Value - 1);

    public static LukasiewiczNumber operator |(LukasiewiczNumber x, LukasiewiczNumber y) =>
        Min(1, x.Value + y.Value);

    public static LukasiewiczNumber Implication(LukasiewiczNumber x, LukasiewiczNumber y) =>
        Min(1, 1 - x.Value + y.Value);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="double" /> value to a <see cref="LukasiewiczNumber" />.
    ///     Note that this value must be in the range μ(x) ∈ [0, 1].
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="LukasiewiczNumber" /> value.</returns>
    public static implicit operator LukasiewiczNumber(double x) => new(x);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="LukasiewiczNumber" /> to a <see cref="double" /> value.
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="LukasiewiczNumber" /> value.</returns>
    public static implicit operator double(LukasiewiczNumber x) => x.Value;

    public static implicit operator FuzzyNumber(LukasiewiczNumber x) => FuzzyNumber.Of(x.Value);
}