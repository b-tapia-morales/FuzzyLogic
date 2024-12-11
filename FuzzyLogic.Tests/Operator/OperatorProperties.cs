using System.Collections;
using FuzzyLogic.Enum;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Number;
using Xunit;

namespace FuzzyLogic.Tests.Operator;

public class OperatorProperties
{
    [Theory]
    [ClassData(typeof(NegationDataOperator))]
    public void ComplementSatisfiesFundamentalProperty(INegation negation)
    {
        Assert.StrictEqual(negation.Complement(0), FuzzyNumber.Max);
        Assert.StrictEqual(negation.Complement(1), FuzzyNumber.Min);
    }

    [Theory]
    [ClassData(typeof(IntersectionDataOperator))]
    public void AssociativityHoldsForIntersection(INorm norm, IEnumerable<(double A, double B, double C)> values)
    {
        AssociativityHoldsForIntersectionHelper(norm, values);
    }

    [Theory]
    [ClassData(typeof(UnionDataOperator))]
    public void AssociativityHoldsForUnion(IConorm conorm, IEnumerable<(double A, double B, double C)> values)
    {
        AssociativityHoldsForUnionHelper(conorm, values);
    }

    private static void AssociativityHoldsForIntersectionHelper(INorm norm, IEnumerable<(double A, double B, double C)> values)
    {
        foreach (var value in values)
        {
            var (x, y, z) = (FuzzyNumber.Of(value.A), FuzzyNumber.Of(value.B), FuzzyNumber.Of(value.C));
            Assert.Equal(norm.Intersection(x, norm.Intersection(y, z)), norm.Intersection(norm.Intersection(x, y), z));
        }
    }

    private static void AssociativityHoldsForUnionHelper(IConorm conorm, IEnumerable<(double A, double B, double C)> values)
    {
        foreach (var value in values)
        {
            var (x, y, z) = (FuzzyNumber.Of(value.A), FuzzyNumber.Of(value.B), FuzzyNumber.Of(value.C));
            Assert.Equal(conorm.Union(x, conorm.Union(y, z)), conorm.Union(conorm.Union(x, y), z));
        }
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
        IEnum<Norm, NormType>.Values.Select(e => new object[] {e, Enumerable.Range(0, 100).Select(_ => (A: Random.NextDouble(), B: Random.NextDouble(), C: Random.NextDouble()))});

    public IEnumerator<object[]> GetEnumerator() => Intersectors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

file class UnionDataOperator : IEnumerable<object[]>
{
    private static readonly Random Random = new();

    private static readonly IEnumerable<object[]> Unitors =
        IEnum<Conorm, ConormType>.Values.Select(e => new object[] {e, Enumerable.Range(0, 100).Select(_ => (A: Random.NextDouble(), B: Random.NextDouble(), C: Random.NextDouble()))});

    public IEnumerator<object[]> GetEnumerator() => Unitors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}