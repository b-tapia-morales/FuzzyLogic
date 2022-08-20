using FuzzyLogic.MembershipFunctions.Base;

namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerCauchyFunction : BaseCauchyFunction<int>, IIntegerFunction
{
    public IntegerCauchyFunction(string name, int a, int b, int c) : base(name, a, b, c)
    {
        A = a;
        B = b;
        C = c;
    }

    protected override int A { get; }
    protected override int B { get; }
    protected override int C { get; }
}