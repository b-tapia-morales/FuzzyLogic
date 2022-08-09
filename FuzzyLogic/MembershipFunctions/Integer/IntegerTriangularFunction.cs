namespace FuzzyLogic.MembershipFunctions.Integer;

public sealed class IntegerTriangularFunction : IntegerTrapezoidalFunction
{
    public IntegerTriangularFunction(string name, int a, int b, int c) : base(name, a, b, b, c)
    {
    }
}