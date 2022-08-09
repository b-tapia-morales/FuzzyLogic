namespace FuzzyLogic.MembershipFunctions.Real;

public sealed class RealRectangularFunction : RealTrapezoidalFunction
{
    public RealRectangularFunction(string name, double a, double b) : base(name, a, a, b, b)
    {
    }
}