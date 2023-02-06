using FuzzyLogic.Number;

namespace FuzzyLogic.Rule;

public class HighestPriority<T> : IComparer<IRule<T>> where T : struct, IFuzzyNumber<T>
{
    public int Compare(IRule<T>? x, IRule<T>? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        if (ReferenceEquals(x, y))
            return 0;
        var a = (int) x.Priority;
        var b = (int) y.Priority;
        return a.CompareTo(b);
    }
}

public class ShortestPremise<T> : IComparer<IRule<T>> where T : struct, IFuzzyNumber<T>
{
    public int Compare(IRule<T>? x, IRule<T>? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        if (ReferenceEquals(x, y))
            return 0;
        var a = x.PremiseLength();
        var b = y.PremiseLength();
        return -(a.CompareTo(b));
    }
}

public class LargestPremise<T> : IComparer<IRule<T>> where T : struct, IFuzzyNumber<T>
{
    public int Compare(IRule<T>? x, IRule<T>? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        if (ReferenceEquals(x, y))
            return 0;
        var a = x.PremiseLength();
        var b = y.PremiseLength();
        return a.CompareTo(b);
    }
}

public class MostKnownFacts<T> : IComparer<IRule<T>> where T : struct, IFuzzyNumber<T>
{
    private readonly IDictionary<string, double> _facts;

    public MostKnownFacts(IDictionary<string, double> facts) => _facts = facts;

    public int Compare(IRule<T>? x, IRule<T>? y)
    {
        int KnownFactsCount(IRule<T> rule) =>
            (rule.Antecedent != null && rule.Antecedent.IsApplicable(_facts) ? 1 : 0) +
            rule.Connectives.Count(e => e.IsApplicable(_facts));

        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        if (ReferenceEquals(x, y))
            return 0;
        var a = KnownFactsCount(x);
        var b = KnownFactsCount(y);
        return a.CompareTo(b);
    }
}

public enum ComparingMethod
{
    HighestPriority = 1,
    ShortestPremise = 2,
    LargestPremise = 3,
    MostKnownFacts = 4
}