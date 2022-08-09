namespace FuzzyLogic.MembershipFunctions.Integer;

public sealed class IntegerRectangularFunction : IntegerTrapezoidalFunction
{
    public IntegerRectangularFunction(string name, int a, int b) : base(name, a, a, b, b)
    {
    }
}