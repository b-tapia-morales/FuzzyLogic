namespace FuzzyLogic.Rule;

public class PriorityComparer : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {
        if (x == null && y == null) return +0;
        if (x == null) return -1;
        if (y == null) return +1;
        var a = (int) x.Priority;
        var b = (int) y.Priority;
        return a.CompareTo(b);
    }
}

public class ShortestPremiseComparer : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {
        if (x == null && y == null) return +0;
        if (x == null) return -1;
        if (y == null) return +1;
        var a = 1 + x.Connectives.Count;
        var b = 1 + y.Connectives.Count;
        return -(a.CompareTo(b));
    }
}

public class LargestPremiseComparer : IComparer<IRule>
{
    public int Compare(IRule? x, IRule? y)
    {
        if (x == null && y == null) return +0;
        if (x == null) return -1;
        if (y == null) return +1;
        var a = 1 + x.Connectives.Count;
        var b = 1 + y.Connectives.Count;
        return a.CompareTo(b);
    }
}

public enum Comparer
{
    Priority = 1,
    ShortestPremise = 2,
    LargestPremise = 3
}