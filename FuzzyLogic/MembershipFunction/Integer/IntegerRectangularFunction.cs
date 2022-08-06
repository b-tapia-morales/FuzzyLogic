namespace FuzzyLogic.MembershipFunction.Integer;

public sealed class IntegerRectangularFunction : IntegerTrapezoidFunction
{
    public IntegerRectangularFunction(string name, int a, int b) : base(name, a, a, b, b)
    {
    }
}