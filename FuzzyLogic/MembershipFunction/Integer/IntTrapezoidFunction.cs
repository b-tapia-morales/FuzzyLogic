namespace FuzzyLogic.MembershipFunction.Integer;

public class IntTrapezoidFunction : ITrapezoidalFunction<int>
{
    protected IntTrapezoidFunction(string name, int a, int b, int c, int d)
    {
        Name = name;
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public string Name { get; }
    public int A { get; }
    public int B { get; }
    public int C { get; }
    public int D { get; }

    public int LowerBoundary() => A;

    public int UpperBoundary() => B;

    public (int X0, int X1) CoreBoundaries() => (B, C);

    public ((int X0, int X1) Lower, (int X0, int X1) Upper) SupportBoundaries() => ((A, B), (C, D));

    public FuzzyNumber MembershipDegree(int x)
    {
        if (x <= A) return 0.0;
        if (x >= A && x <= B) return (x - A) / (B - A);
        if (x >= B && x <= C) return 1.0;
        if (x >= C && x <= D) return (D - x) / (D - C);
        return 0.0;
    }
}