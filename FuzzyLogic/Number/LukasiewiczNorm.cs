using FuzzyLogic.Number.Enums;
using static System.Math;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.LukasiewiczNorm>;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

public readonly record struct LukasiewiczNorm : IFuzzyNumber<LukasiewiczNorm>
{
    private LukasiewiczNorm(double value) => Value = value;

    public double Value { get; }

    public static LukasiewiczNorm Of(double value)
    {
        if (Abs(1 - value) < Tolerance) value = 1;
        if (Abs(value) < Tolerance) value = 0;
        RangeCheck(value);
        return new LukasiewiczNorm(value);
    }

    public static LukasiewiczNorm operator &(LukasiewiczNorm x, LukasiewiczNorm y) =>
        TriangularNorm<LukasiewiczNorm>.FromToken(NormToken.Lukasiewicz)(x, y);

    public static LukasiewiczNorm operator |(LukasiewiczNorm x, LukasiewiczNorm y) =>
        TriangularConorm<LukasiewiczNorm>.FromToken(ConormToken.Lukasiewicz)(x, y);

    public static LukasiewiczNorm Then(LukasiewiczNorm x, LukasiewiczNorm y) =>
        Residuum<LukasiewiczNorm>.FromToken(ResiduumToken.Lukasiewicz)(x, y);

    static bool IFuzzyNumber<LukasiewiczNorm>.operator ==(LukasiewiczNorm x, LukasiewiczNorm y) =>
        Abs(x.Value - y.Value) < Tolerance;

    static bool IFuzzyNumber<LukasiewiczNorm>.operator !=(LukasiewiczNorm x, LukasiewiczNorm y) =>
        Abs(x.Value - y.Value) >= Tolerance;
}