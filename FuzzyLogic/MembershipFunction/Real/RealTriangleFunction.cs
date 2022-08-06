namespace FuzzyLogic.MembershipFunction.Real;

public sealed class RealTriangleFunction : RealTrapezoidFunction
{
    public RealTriangleFunction(string name, double a, double b, double c) : base(name, a, b, b, c)
    {
    }
}