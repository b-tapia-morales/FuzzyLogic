namespace FuzzyLogic.MembershipFunction.Integer;

public sealed class IntTriangleFunction : IntTrapezoidFunction
{
    public IntTriangleFunction(string name, int a, int b, int c) : base(name, a, b, b, c)
    {
    }
}