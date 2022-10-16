using FuzzyLogic.MembershipFunctions.Base;
using FuzzyLogic.Number;

namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerTrapezoidalFunction : BaseTrapezoidalFunction<int>, IIntegerFunction
{
    public IntegerTrapezoidalFunction(string name, int a, int b, int c, int d) : base(name, a, b, c, d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    protected override int A { get; }
    protected override int B { get; }
    protected override int C { get; }
    protected override int D { get; }
}