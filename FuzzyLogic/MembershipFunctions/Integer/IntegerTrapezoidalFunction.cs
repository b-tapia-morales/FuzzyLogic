using FuzzyLogic.MembershipFunctions.Base;

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

    public override FuzzyNumber MembershipDegree(int x)
    {
        if (x <= A) return 0.0;
        if (x >= A && x <= B) return (double) (x - A) / (B - A);
        if (x >= B && x <= C) return 1.0;
        if (x >= C && x <= D) return (double) (D - x) / (D - C);
        return 0.0;
    }
}