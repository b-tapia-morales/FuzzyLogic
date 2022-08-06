namespace FuzzyLogic.MembershipFunction;

public sealed class RectangularFunction : TrapezoidFunction
{
    public RectangularFunction(string name, double a, double b) : base(name, a, a, b, b)
    {
    }
}