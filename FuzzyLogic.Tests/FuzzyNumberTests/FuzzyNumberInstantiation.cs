using Xunit;

namespace FuzzyLogic.Tests.FuzzyNumberTests;

public class FuzzyNumberInstantiation
{
    private static readonly Random Random = new();

    [Fact]
    public void InstantiationThrowsExceptionOnValueOutOfRangeHelper()
    {
        Assert.Throws<ArgumentException>(() => Number.FuzzyNumber.Of(Number.FuzzyNumber.Epsilon * -1e1));
        Assert.Throws<ArgumentException>(() => Number.FuzzyNumber.Of(1 + Number.FuzzyNumber.Epsilon * 1e1));
        Assert.Throws<ArgumentException>(() => Number.FuzzyNumber.Of(double.MaxValue));
    }

    [Fact]
    public void TryCreateFailsOnValueOutOfRangeHelper()
    {
        Assert.False(Number.FuzzyNumber.TryCreate(Number.FuzzyNumber.Epsilon * -1e1, out _));
        Assert.False(Number.FuzzyNumber.TryCreate(1 + Number.FuzzyNumber.Epsilon * 1e1, out _));
        Assert.False(Number.FuzzyNumber.TryCreate(double.MaxValue, out _));
    }

    [Fact]
    public void NegationOperatorYieldsExpectedValuesHelper()
    {
        Assert.Equal(Number.FuzzyNumber.Min, !Number.FuzzyNumber.Max, Number.FuzzyNumber.Epsilon);
        Assert.StrictEqual(Number.FuzzyNumber.Min, !Number.FuzzyNumber.Max);
        Assert.True(Number.FuzzyNumber.Min == !Number.FuzzyNumber.Max);
        Assert.Equal(Number.FuzzyNumber.Max, !Number.FuzzyNumber.Min, Number.FuzzyNumber.Epsilon);
        Assert.StrictEqual(Number.FuzzyNumber.Max, !Number.FuzzyNumber.Min);
        Assert.True(Number.FuzzyNumber.Max == !Number.FuzzyNumber.Min);
    }

    [Theory]
    [MemberData(nameof(GenerateRandomValues), parameters: 100)]
    public void NegationOperatorIsEquivalent(double x) =>
        NegationOperatorIsEquivalentHelper(x);

    [Theory]
    [MemberData(nameof(GenerateRandomValuesIncludingTolerance), parameters: 100)]
    public void ValuesYieldEqualityForDifferenceUnderTolerance(double first, double second) =>
        ValuesYieldEqualityForDifferenceUnderToleranceHelper(first, second);

    [Theory]
    [InlineData(1e-1 + Number.FuzzyNumber.Epsilon, 1e-1 + Number.FuzzyNumber.Epsilon + 1e-15)]
    [InlineData(0 - Number.FuzzyNumber.Epsilon * 1e-1, 0)]
    [InlineData(0 + Number.FuzzyNumber.Epsilon * 1e-1, 0)]
    [InlineData(1 - Number.FuzzyNumber.Epsilon * 1e-1, 1)]
    [InlineData(1 + Number.FuzzyNumber.Epsilon * 1e-1, 1)]
    public void EdgeCasesYieldEqualityForDifferenceUnderTolerance(double first, double second) =>
        ValuesYieldEqualityForDifferenceUnderTolerance(first, second);

    private static void NegationOperatorIsEquivalentHelper(double x)
    {
        Assert.Equal(1 - x, !Number.FuzzyNumber.Of(x), Number.FuzzyNumber.Epsilon);
        Assert.True(Number.FuzzyNumber.Of(1 - x) == !Number.FuzzyNumber.Of(x));
    }

    private static void ValuesYieldEqualityForDifferenceUnderToleranceHelper(double a, double b)
    {
        Assert.Equal(Number.FuzzyNumber.Of(a), Number.FuzzyNumber.Of(b), Number.FuzzyNumber.Epsilon);
        Assert.True(Number.FuzzyNumber.Of(a) == Number.FuzzyNumber.Of(b));
    }

    public static TheoryData<double> GenerateRandomValues(int n)
    {
        var data = new TheoryData<double>();
        for (var i = 1; i <= n; i++)
        {
            data.Add(Random.NextDouble());
        }

        return data;
    }

    public static IEnumerable<object[]> GenerateRandomValuesIncludingTolerance(int n)
    {
        for (var i = 1; i <= n; i++)
        {
            var x = Random.NextDouble();
            yield return [x, x - Random.Next(1, 9) * Number.FuzzyNumber.Epsilon * 1e-1];
        }
    }
}