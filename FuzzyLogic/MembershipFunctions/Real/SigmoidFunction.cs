namespace FuzzyLogic.MembershipFunctions.Real;

public class SigmoidFunction : BaseMembershipFunction<double>
{
    public SigmoidFunction(string name, double a, double c) : base(name)
    {
        A = a;
        C = c;
    }

    public double A { get; }
    public double C { get; }

    public override FuzzyNumber MembershipDegree(double x) => 1 / (1 + Math.Pow(Math.E, -A * (x - C)));
}