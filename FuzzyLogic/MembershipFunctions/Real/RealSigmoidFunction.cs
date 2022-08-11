namespace FuzzyLogic.MembershipFunctions.Real;

public class RealSigmoidFunction : BaseSigmoidFunction<double>
{
    public RealSigmoidFunction(string name, double a, double c) : base(name, a, c)
    {
        A = a;
        C = c;
    }

    protected override double A { get; }
    protected override double C { get; }
}