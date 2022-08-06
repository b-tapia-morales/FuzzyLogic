namespace FuzzyLogic.MembershipFunction.Integer;

public sealed class IntRectangularFunction : IntTrapezoidFunction
{
    public IntRectangularFunction(string name, int a, int b) : base(name, a, a, b, b)
    {
    }
}