using Xunit;
using static FuzzyLogic.FuzzyNumber;

namespace FuzzyLogic.Tests.FuzzyNumberTest;

public class FuzzyNumberTest
{
    private static readonly Random Random = new();
    
    [Fact]
    public void InstantiationThrowsExceptionOnValueOutOfRange()
    {
        Assert.Throws<ArgumentException>(() => Of(double.MinValue));
        Assert.Throws<ArgumentException>(() => Of(-0.0001));
        Assert.Throws<ArgumentException>(() => Of(+1.0001));
        Assert.Throws<ArgumentException>(() => Of(double.MaxValue));
    }

    [Fact]
    public void TryCreateFailsOnValueOutOfRange()
    {
        Assert.False(TryCreate(double.MinValue, out _));
        Assert.False(TryCreate(-0.00001, out _));
        Assert.False(TryCreate(+1.00001, out _));
        Assert.False(TryCreate(double.MaxValue, out _));
    }

    [Fact]
    public void MinMaxOperatorsYieldExpectedValues()
    {
        Assert.Equal(Of(0), MinValue & MaxValue, Tolerance);
        Assert.Equal(Of(1), MinValue | MaxValue, Tolerance);
    }

    [Theory]
    [MemberData(nameof(GenerateRandomPairs), parameters: 100)]
    public void MinMaxOperatorsAreEquivalent(double x, double y)
    {
        Assert.Equal(Math.Min(x, y), Of(x) & Of(y), Tolerance);
        Assert.Equal(Math.Max(x, y), Of(x) | Of(y), Tolerance);
    }

    [Fact]
    public void NegationOperatorYieldsExpectedValues()
    {
        Assert.Equal(Of(0), !Of(1), Tolerance);
        Assert.True(Of(0) == !Of(1));
        Assert.Equal(Of(1), !Of(0), Tolerance);
        Assert.True(Of(1) == !Of(0));
    }

    [Theory]
    [MemberData(nameof(GenerateRandomValues), parameters: 100)]
    public void NegationOperatorIsEquivalent(double x)
    {
        Assert.Equal(1 - x, !Of(x), Tolerance);
        Assert.True(Of(1 - x) == !Of(x));
    }

    [Theory]
    [InlineData(1e-1 + Tolerance, 1e-1 + 9.9999999999 * Tolerance * 1e-1)]
    [InlineData(1e-1 + Tolerance, 1e-1 + 1.0000000001 * Tolerance * 1e-1)]
    [InlineData(0 - Tolerance * 1e-1, 0)]
    [InlineData(0 + Tolerance * 1e-1, 0)]
    [InlineData(1 - Tolerance * 1e-1, 1)]
    [InlineData(1 + Tolerance * 1e-1, 1)]
    public void EdgeCasesYieldEqualityForDifferenceUnderTolerance(double first, double second)
    {
        ValuesYieldEqualityForDifferenceUnderTolerance(first, second);
    }

    [Theory]
    [MemberData(nameof(GenerateRandomValuesIncludingTolerance), parameters: 100)]
    public void ValuesYieldEqualityForDifferenceUnderTolerance(double first, double second)
    {
        Assert.Equal(Of(first), Of(second), Tolerance);
        Assert.True(Of(first) == Of(second));
    }
    
    public static IEnumerable<object[]> GenerateRandomValues(int n)
    {
        for (var i = 1; i <= n; i++)
        {
            yield return new object[] {Random.NextDouble()};
        }
    }
    
    public static IEnumerable<object[]> GenerateRandomPairs(int n)
    {
        for (var i = 1; i <= n; i++)
        {
            yield return new object[] {Random.NextDouble(), Random.NextDouble()};
        }
    }
    
    public static IEnumerable<object[]> GenerateRandomValuesIncludingTolerance(int n)
    {
        for (var i = 1; i <= n; i++)
        {
            var x = Random.NextDouble();
            yield return new object[] {x, x - Random.Next(1, 10) * Tolerance * 1e-1};
        }
    }
}