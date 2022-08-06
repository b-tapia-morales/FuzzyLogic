namespace FuzzyLogic.MembershipFunction.Real;

public class RealTrapezoidFunction : ITrapezoidalFunction<double>
{
    protected RealTrapezoidFunction(string name, double a, double b, double c, double d)
    {
        Name = name;
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public string Name { get; }
    public double A { get; }
    public double B { get; }
    public double C { get; }
    public double D { get; }

    public double LowerBoundary() => A;

    public double UpperBoundary() => B;

    public (double X0, double X1) CoreBoundaries() => (B, C);

    public ((double X0, double X1) Lower, (double X0, double X1) Upper) SupportBoundaries() => ((A, B), (C, D));

    public FuzzyNumber MembershipDegree(double x)
    {
        if (x <= A) return 0.0;
        if (x >= A && x <= B) return (x - A) / (B - A);
        if (x >= B && x <= C) return 1.0;
        if (x >= C && x <= D) return (D - x) / (D - C);
        return 0.0;
    }
}