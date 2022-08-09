namespace FuzzyLogic.MembershipFunctions.Integer;

public class IntegerTrapezoidalFunction : BaseTrapezoidalFunction<int>
{
    public IntegerTrapezoidalFunction(string name, int a, int b, int c, int d) : base(name, a, b, c, d)
    {
        Name = name;
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public new string Name { get; }
    public new int A { get; }
    public new int B { get; }
    public new int C { get; }
    public new int D { get; }

    public override FuzzyNumber MembershipDegree(int x)
    {
        if (x <= A) return 0.0;
        if (x >= A && x <= B) return (double) (x - A) / (B - A);
        if (x >= B && x <= C) return 1.0;
        if (x >= C && x <= D) return (double) (D - x) / (D - C);
        return 0.0;
    }
}