using static System.Math;
using static FuzzyLogic.Number.IFuzzyNumber<FuzzyLogic.Number.ProductNorm>;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

public readonly record struct ProductNorm : IFuzzyNumber<ProductNorm>
{
    private ProductNorm(double value) => Value = value;

    public double Value { get; }

    public static ProductNorm Of(double value)
    {
        if (Abs(1 - value) < Tolerance) value = 1;
        if (Abs(value) < Tolerance) value = 0;
        RangeCheck(value);
        return new ProductNorm(value);
    }

    public static ProductNorm operator &(ProductNorm x, ProductNorm y) => 
        Of(x.Value * y.Value);

    public static ProductNorm operator |(ProductNorm x, ProductNorm y) => 
        Of(x.Value + y.Value - x.Value * y.Value);

    public static ProductNorm Then(ProductNorm x, ProductNorm y) => 
        Of(x.Value <= y.Value ? 1 : y.Value / x.Value);

    static bool IFuzzyNumber<ProductNorm>.operator ==(ProductNorm x, ProductNorm y) =>
        Abs(x.Value - y.Value) < Tolerance;

    static bool IFuzzyNumber<ProductNorm>.operator !=(ProductNorm x, ProductNorm y) =>
        Abs(x.Value - y.Value) >= Tolerance;
}