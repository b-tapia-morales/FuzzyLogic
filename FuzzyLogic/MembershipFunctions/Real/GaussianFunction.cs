namespace FuzzyLogic.MembershipFunctions.Real;

public class GaussianFunction : BaseMembershipFunction<double>
{
    public GaussianFunction(string name, double m, double o): base(name)
    {
        M = m;
        O = o;
    }

    public double M { get; }
    public double O { get; }

    public double? LowerBoundary() => M - 3 * O;

    public double? UpperBoundary() => M + 3 * O;
    
    public override FuzzyNumber MembershipDegree(double x) => Math.Min(1.0, Math.Pow(Math.E, -0.5 * Math.Pow((x - M) / O, 2)));
}