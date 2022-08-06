namespace FuzzyLogic.MembershipFunction.Real;

public sealed class RealRectangularFunction : RealTrapezoidFunction
{
    public RealRectangularFunction(string name, double a, double b) : base(name, a, a, b, b)
    {
    }
}