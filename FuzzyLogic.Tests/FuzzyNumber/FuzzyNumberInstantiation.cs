using System.Diagnostics.CodeAnalysis;
using FuzzyLogic.Number;
using Xunit;

namespace FuzzyLogic.Tests.FuzzyNumber;

[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
public class FuzzyNumberInstantiation
{
    private static readonly Random Random = new();

    private static void InstantiationThrowsExceptionOnValueOutOfRangeHelper<T>() where T : struct, IFuzzyNumber<T>
    {
        Assert.Throws<ArgumentException>(() => T.Of(double.MinValue));
        Assert.Throws<ArgumentException>(() => T.Of(-1e-5));
        Assert.Throws<ArgumentException>(() => T.Of(1 + 1e-5));
        Assert.Throws<ArgumentException>(() => T.Of(double.MaxValue));
    }

    private static void TryCreateFailsOnValueOutOfRangeHelper<T>() where T : struct, IFuzzyNumber<T>
    {
        Assert.False(T.TryCreate(double.MinValue, out _));
        Assert.False(T.TryCreate(-1e-5, out _));
        Assert.False(T.TryCreate(1 + 1e-5, out _));
        Assert.False(T.TryCreate(double.MaxValue, out _));
    }

    private static void NegationOperatorYieldsExpectedValuesHelper<T>() where T : struct, IFuzzyNumber<T>
    {
        Assert.Equal(T.MinValue(), !T.MaxValue(), IFuzzyNumber<Number.FuzzyNumber>.Tolerance);
        Assert.StrictEqual(T.MinValue(), !T.MaxValue());
        Assert.True(T.MinValue() == !T.MaxValue());
        Assert.Equal(T.MaxValue(), !T.MinValue(), IFuzzyNumber<Number.FuzzyNumber>.Tolerance);
        Assert.StrictEqual(T.MaxValue(), !T.MinValue());
        Assert.True(T.MaxValue() == !T.MinValue());
    }

    private static void NegationOperatorIsEquivalentHelper<T>(double x) where T : struct, IFuzzyNumber<T>
    {
        Assert.Equal(1 - x, !T.Of(x), IFuzzyNumber<Number.FuzzyNumber>.Tolerance);
        Assert.True(T.Of(1 - x) == !T.Of(x));
    }

    private static void ValuesYieldEqualityForDifferenceUnderToleranceHelper<T>(double first, double second)
        where T : struct, IFuzzyNumber<T>
    {
        Assert.Equal(T.Of(first), T.Of(second), IFuzzyNumber<Number.FuzzyNumber>.Tolerance);
        Assert.True(T.Of(first) == T.Of(second));
    }

    [Fact]
    public void InstantiationThrowsExceptionOnValueOutOfRange()
    {
        InstantiationThrowsExceptionOnValueOutOfRangeHelper<Number.FuzzyNumber>();
        InstantiationThrowsExceptionOnValueOutOfRangeHelper<LukasiewiczNorm>();
        InstantiationThrowsExceptionOnValueOutOfRangeHelper<ProductNorm>();
        InstantiationThrowsExceptionOnValueOutOfRangeHelper<NilpotentNorm>();
    }

    [Fact]
    public void TryCreateFailsOnValueOutOfRange()
    {
        TryCreateFailsOnValueOutOfRangeHelper<Number.FuzzyNumber>();
        TryCreateFailsOnValueOutOfRangeHelper<LukasiewiczNorm>();
        TryCreateFailsOnValueOutOfRangeHelper<ProductNorm>();
        TryCreateFailsOnValueOutOfRangeHelper<NilpotentNorm>();
    }

    [Fact]
    public void NegationOperatorYieldsExpectedValues()
    {
        NegationOperatorYieldsExpectedValuesHelper<Number.FuzzyNumber>();
        NegationOperatorYieldsExpectedValuesHelper<LukasiewiczNorm>();
        NegationOperatorYieldsExpectedValuesHelper<ProductNorm>();
        NegationOperatorYieldsExpectedValuesHelper<NilpotentNorm>();
    }

    [Theory]
    [MemberData(nameof(GenerateRandomValues), parameters: 100)]
    public void NegationOperatorIsEquivalent(double x)
    {
        NegationOperatorIsEquivalentHelper<Number.FuzzyNumber>(x);
        NegationOperatorIsEquivalentHelper<LukasiewiczNorm>(x);
        NegationOperatorIsEquivalentHelper<ProductNorm>(x);
        NegationOperatorIsEquivalentHelper<NilpotentNorm>(x);
    }

    [Theory]
    [MemberData(nameof(GenerateRandomValuesIncludingTolerance), parameters: 100)]
    public void ValuesYieldEqualityForDifferenceUnderTolerance(double first, double second)
    {
        ValuesYieldEqualityForDifferenceUnderToleranceHelper<Number.FuzzyNumber>(first, second);
        ValuesYieldEqualityForDifferenceUnderToleranceHelper<LukasiewiczNorm>(first, second);
        ValuesYieldEqualityForDifferenceUnderToleranceHelper<ProductNorm>(first, second);
        ValuesYieldEqualityForDifferenceUnderToleranceHelper<NilpotentNorm>(first, second);
    }

    [Theory]
    [InlineData(1e-1 + IFuzzyNumber<Number.FuzzyNumber>.Tolerance, 1e-1 + IFuzzyNumber<Number.FuzzyNumber>.Tolerance + 1e-15)]
    [InlineData(0 - IFuzzyNumber<Number.FuzzyNumber>.Tolerance * 1e-1, 0)]
    [InlineData(0 + IFuzzyNumber<Number.FuzzyNumber>.Tolerance * 1e-1, 0)]
    [InlineData(1 - IFuzzyNumber<Number.FuzzyNumber>.Tolerance * 1e-1, 1)]
    [InlineData(1 + IFuzzyNumber<Number.FuzzyNumber>.Tolerance * 1e-1, 1)]
    public void EdgeCasesYieldEqualityForDifferenceUnderTolerance(double first, double second) =>
        ValuesYieldEqualityForDifferenceUnderTolerance(first, second);

    public static IEnumerable<object[]> GenerateRandomValues(int n)
    {
        for (var i = 1; i <= n; i++)
        {
            yield return new object[] {Random.NextDouble()};
        }
    }

    public static IEnumerable<object[]> GenerateRandomValuesIncludingTolerance(int n)
    {
        for (var i = 1; i <= n; i++)
        {
            var x = Random.NextDouble();
            yield return new object[] {x, x - Random.Next(1, 9) * IFuzzyNumber<Number.FuzzyNumber>.Tolerance * 1e-1};
        }
    }
}