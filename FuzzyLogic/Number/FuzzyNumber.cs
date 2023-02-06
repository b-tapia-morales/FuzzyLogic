using static System.Math;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.FuzzyNumber>;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

public readonly record struct FuzzyNumber : IFuzzyNumber<FuzzyNumber>
{
    private static readonly FuzzyNumber Min = Of(0);
    private static readonly FuzzyNumber Max = Of(1);

    private FuzzyNumber(double value)
    {
        if (Abs(0 - value) < Tolerance) Value = 0;
        if (Abs(1 - value) < Tolerance) Value = 1;
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
            fuzzyNumber = number < 0 ? Max(0, number) : Min(1, number);
            return false;
        }
    }

    public static FuzzyNumber MinValue() => Min;

    public static FuzzyNumber MaxValue() => Max;

    public static FuzzyNumber operator !(FuzzyNumber x) => 1 - x.Value;

    public static FuzzyNumber operator &(FuzzyNumber x, FuzzyNumber y) => Min(x.Value, y.Value);

    public static FuzzyNumber operator |(FuzzyNumber x, FuzzyNumber y) => Max(x.Value, y.Value);

    public static FuzzyNumber Implication(FuzzyNumber x, FuzzyNumber y) =>
        Max(1 - x.Value, Min(x.Value, y.Value));

    public static implicit operator FuzzyNumber(double x) => Of(x);

    public static implicit operator double(FuzzyNumber x) => x.Value;

    static implicit IFuzzyNumber<FuzzyNumber>.operator FuzzyNumber(FuzzyNumber x) => x;
}