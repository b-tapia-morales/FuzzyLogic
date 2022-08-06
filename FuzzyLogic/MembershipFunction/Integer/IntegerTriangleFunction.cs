namespace FuzzyLogic.MembershipFunction.Integer;

public sealed class IntegerTriangleFunction : IntegerTrapezoidFunction
{
    public IntegerTriangleFunction(string name, int a, int b, int c) : base(name, a, b, b, c)
    {
    }
}