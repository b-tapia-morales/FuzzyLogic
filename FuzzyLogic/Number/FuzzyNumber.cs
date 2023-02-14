using static System.Math;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.FuzzyNumber>;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

public readonly record struct FuzzyNumber : IFuzzyNumber<FuzzyNumber>
{
    private FuzzyNumber(double value) => Value = value;

    public double Value { get; }

    public static FuzzyNumber Of(double value)
    {
        if (Abs(1 - value) < Tolerance) value = 1;
        if (Abs(value) < Tolerance) value = 0;
        RangeCheck(value);
        return new FuzzyNumber(value);
    }

    static bool IFuzzyNumber<FuzzyNumber>.operator ==(FuzzyNumber x, FuzzyNumber y) =>
        Abs(x.Value - y.Value) < Tolerance;

    static bool IFuzzyNumber<FuzzyNumber>.operator !=(FuzzyNumber x, FuzzyNumber y) =>
        Abs(x.Value - y.Value) >= Tolerance;
}