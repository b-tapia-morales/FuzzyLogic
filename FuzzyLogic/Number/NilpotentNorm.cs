using FuzzyLogic.Number.Enums;
using static System.Math;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.NilpotentNorm>;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

public readonly record struct NilpotentNorm : IFuzzyNumber<NilpotentNorm>
{
    private NilpotentNorm(double value) => Value = value;

    public double Value { get; }

    public static NilpotentNorm Of(double value)
    {
        if (Abs(1 - value) < Tolerance) value = 1;
        if (Abs(value) < Tolerance) value = 0;
        RangeCheck(value);
        return new NilpotentNorm(value);
    }

    public static NilpotentNorm operator &(NilpotentNorm x, NilpotentNorm y) =>
        TriangularNorm<NilpotentNorm>.FromToken(NormToken.NilpotentMinimum)(x, y);

    public static NilpotentNorm operator |(NilpotentNorm x, NilpotentNorm y) =>
        TriangularConorm<NilpotentNorm>.FromToken(ConormToken.NilpotentMaximum)(x, y);

    public static NilpotentNorm Then(NilpotentNorm x, NilpotentNorm y) =>
        Residuum<NilpotentNorm>.FromToken(ResiduumToken.KleeneDienes)(x, y);

    static bool IFuzzyNumber<NilpotentNorm>.operator ==(NilpotentNorm x, NilpotentNorm y) =>
        Abs(x.Value - y.Value) < Tolerance;

    static bool IFuzzyNumber<NilpotentNorm>.operator !=(NilpotentNorm x, NilpotentNorm y) =>
        Abs(x.Value - y.Value) >= Tolerance;
}