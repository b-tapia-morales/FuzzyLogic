namespace FuzzyLogic.Rule;

public class HighestPriority : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
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

public class ShortestPremise : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
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

public class LargestPremise : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
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

public class MostKnownFacts(IDictionary<string, double> facts) : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {

        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        if (ReferenceEquals(x, y))
            return 0;
        var a = KnownFactsCount(x);
        var b = KnownFactsCount(y);
        return a.CompareTo(b);

        int KnownFactsCount(IRule rule) =>
            (rule.Conditional != null && rule.Conditional.IsApplicable(facts) ? 1 : 0) +
            rule.Connectives.Count(e => e.IsApplicable(facts));
    }
}

public enum ComparingMethod
{
    HighestPriority,
    ShortestPremise,
    LargestPremise,
    MostKnownFacts
}