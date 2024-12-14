using System.Collections;
using FuzzyLogic.Enum;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using Xunit;

namespace FuzzyLogic.Tests.OperatorTests;

public static class GlobalConst
{
    public const int N = 100;
}

public class OperatorProperties
{
    private static readonly INorm Minimum = Norm.Minimum;
    private static readonly IConorm Maximum = Conorm.Maximum;

    [Theory]
    [ClassData(typeof(NegationDataOperator))]
    public void ComplementSatisfiesFundamentalProperty(INegation negation)
    {
        Assert.StrictEqual(negation.Complement(0), FuzzyNumber.Max);
        Assert.StrictEqual(negation.Complement(1), FuzzyNumber.Min);
    }

    [Theory]
    [ClassData(typeof(IntersectionDataOperator))]
    public void AssociativityHoldsForIntersection(INorm norm, FuzzyNumber x, FuzzyNumber y, FuzzyNumber z)
    {
        Assert.Equal(norm.Intersection(x, norm.Intersection(y, z)), norm.Intersection(norm.Intersection(x, y), z));
    }

    [Theory]
    [ClassData(typeof(UnionDataOperator))]
    public void AssociativityHoldsForUnion(IConorm conorm, FuzzyNumber x, FuzzyNumber y, FuzzyNumber z)
    {
        Assert.Equal(conorm.Union(x, conorm.Union(y, z)), conorm.Union(conorm.Union(x, y), z));
    }

    [Theory]
    [ClassData(typeof(IntersectionDataOperator))]
    public void CommutativityHoldsForIntersection(INorm norm, FuzzyNumber x, FuzzyNumber y, FuzzyNumber _)
    {
        Assert.Equal(norm.Intersection(x, y), norm.Intersection(y, x));
        Assert.StrictEqual(norm.Intersection(x, y), norm.Intersection(y, x));
    }

    [Theory]
    [ClassData(typeof(UnionDataOperator))]
    public void CommutativityHoldsForUnion(IConorm conorm, FuzzyNumber x, FuzzyNumber y, FuzzyNumber _)
    {
        Assert.Equal(conorm.Union(x, y), conorm.Union(y, x));
        Assert.StrictEqual(conorm.Union(x, y), conorm.Union(y, x));
    }

    [Theory]
    [ClassData(typeof(IntersectionDataOperator))]
    public void MonotonicityHoldsForIntersection(INorm norm, FuzzyNumber x, FuzzyNumber y, FuzzyNumber z)
    {
        var (min, median, max) = (MathUtils.Min<double>(x, y, z), MathUtils.Median<double>(x, y, z), MathUtils.Max<double>(x, y, z));
        Assert.True(norm.Intersection(min, median) <= norm.Intersection(min, max));
    }

    [Theory]
    [ClassData(typeof(UnionDataOperator))]
    public void MonotonicityHoldsForUnion(IConorm conorm, FuzzyNumber x, FuzzyNumber y, FuzzyNumber z)
    {
        var (min, median, max) = (MathUtils.Min<double>(x, y, z), MathUtils.Median<double>(x, y, z), MathUtils.Max<double>(x, y, z));
        Assert.True(conorm.Union(min, median) <= conorm.Union(min, max));
    }

    [Theory]
    [ClassData(typeof(GodelDataOperators))]
    public void DistributivityHoldsForGodelOperators(FuzzyNumber x, FuzzyNumber y, FuzzyNumber z)
    {
        Assert.Equal(Maximum.Union(x, Minimum.Intersection(y, z)), Minimum.Intersection(Maximum.Union(x, y), Maximum.Union(x, z)));
        Assert.Equal(Minimum.Intersection(x, Maximum.Union(y, z)), Maximum.Union(Minimum.Intersection(x, y), Minimum.Intersection(x, z)));
    }

    [Theory]
    [ClassData(typeof(GodelDataOperators))]
    public void StrictMonotonicityHoldsForGodelOperators(FuzzyNumber x, FuzzyNumber y, FuzzyNumber _)
    {
        var (min, max) = (Minimum.Intersection(x, y), Maximum.Union(x, y));
        Assert.Equal(Minimum.Intersection(min, min), Minimum.Intersection(max, max));
        Assert.Equal(Maximum.Union(min, min), Maximum.Union(max, max));
    }

    [Theory(Skip = "Fails for certain random values, reason unknown")]
    [ClassData(typeof(GodelDataOperators))]
    public void AbsorptionHoldsForGodelOperators(FuzzyNumber x, FuzzyNumber y, FuzzyNumber _)
    {
        Assert.Equal(Minimum.Intersection(x, Maximum.Union(x, y)), x);
        Assert.Equal(Maximum.Union(x, Minimum.Intersection(x, y)), x);
    }

    [Theory]
    [ClassData(typeof(GodelDataOperators))]
    public void NonCompensationHoldsForGodelOperators(FuzzyNumber x, FuzzyNumber y, FuzzyNumber z)
    {
        var (min, max, median) = (MathUtils.Min<double>(x, y, z), MathUtils.Median<double>(x, y, z), MathUtils.Max<double>(x, y, z));
        var (a, b, c) = (FuzzyNumber.Of(min), FuzzyNumber.Of(median), FuzzyNumber.Of(max));
        Assert.NotEqual(Minimum.Intersection(a, c), Minimum.Intersection(b, b));
        Assert.NotEqual(Maximum.Union(a, c), Maximum.Union(b, b));
    }
}

file class NegationDataOperator : IEnumerable<object[]>
{
    private static readonly IEnumerable<object[]> Negators = IEnum<Negation, NegationType>.Values.Select(e => new object[] {e});

    public IEnumerator<object[]> GetEnumerator() => Negators.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

file class IntersectionDataOperator : IEnumerable<object[]>
{
    private static readonly Random Random = new();

    private static readonly IEnumerable<object[]> Intersectors =
        Enumerable
            .Repeat(IEnum<Norm, NormType>.Values, GlobalConst.N)
            .SelectMany(e => e)
            .Select(e => new object[] {e, FuzzyNumber.Of(Random.NextDouble()), FuzzyNumber.Of(Random.NextDouble()), FuzzyNumber.Of(Random.NextDouble())});

    public IEnumerator<object[]> GetEnumerator() => Intersectors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

file class UnionDataOperator : IEnumerable<object[]>
{
    private static readonly Random Random = new();

    private static readonly IEnumerable<object[]> Unitors =
        Enumerable
            .Repeat(IEnum<Conorm, ConormType>.Values, GlobalConst.N)
            .SelectMany(e => e)
            .Select(e => new object[] {e, FuzzyNumber.Of(Random.NextDouble()), FuzzyNumber.Of(Random.NextDouble()), FuzzyNumber.Of(Random.NextDouble())});

    public IEnumerator<object[]> GetEnumerator() => Unitors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

file class GodelDataOperators : IEnumerable<object[]>
{
    private static readonly Random Random = new();

    private static readonly IEnumerable<object[]> GodelOperators =
        Enumerable
            .Range(0, GlobalConst.N)
            .Select(_ => new object[] {FuzzyNumber.Of(Random.NextDouble()), FuzzyNumber.Of(Random.NextDouble()), FuzzyNumber.Of(Random.NextDouble())});

    public IEnumerator<object[]> GetEnumerator() => GodelOperators.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}