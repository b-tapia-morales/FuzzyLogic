namespace FuzzyLogic.MembershipFunction;

public sealed class TriangleFunction : TrapezoidFunction
{
    public TriangleFunction(string name, double a, double b, double c) : base(name, a, b, b, c)
    {
    }
}