namespace FuzzyLogic.Rule;

public class PriorityComparer : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        var a = (int) x.Priority;
        var b = (int) y.Priority;
        return a.CompareTo(b);
    }
}

public class ShortestPremiseComparer : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        var a = 1 + x.Connectives.Count;
        var b = 1 + y.Connectives.Count;
        return -(a.CompareTo(b));
    }
}

public class LargestPremiseComparer : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {
        if (x == null || y == null)
            throw new ArgumentException("Rule references cannot be null");
        var a = 1 + x.Connectives.Count;
        var b = 1 + y.Connectives.Count;
        return a.CompareTo(b);
    }
}

public enum ComparingMethod
{
    Priority = 1,
    ShortestPremise = 2,
    LargestPremise = 3
}