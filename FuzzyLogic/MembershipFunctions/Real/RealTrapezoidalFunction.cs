using FuzzyLogic.MembershipFunctions.Base;

namespace FuzzyLogic.MembershipFunctions.Real;

public class RealTrapezoidalFunction : BaseTrapezoidalFunction<double>, IRealFunction
{
    public RealTrapezoidalFunction(string name, double a, double b, double c, double d) : base(name, a, b, c, d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public new double A { get; }
    public new double B { get; }
    public new double C { get; }
    public new double D { get; }

    /// <inheritdoc/>
    public override FuzzyNumber MembershipDegree(double x)
    {
        if (x <= A) return 0.0;
        if (x >= A && x <= B) return (x - A) / (B - A);
        if (x >= B && x <= C) return 1.0;
        if (x >= C && x <= D) return (D - x) / (D - C);
        return 0.0;
    }
}