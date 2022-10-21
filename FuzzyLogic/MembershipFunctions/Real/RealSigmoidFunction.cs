using FuzzyLogic.MembershipFunctions.Base;

namespace FuzzyLogic.MembershipFunctions.Real;

public class RealSigmoidFunction : BaseSigmoidFunction<double>, IRealFunction
{
    public RealSigmoidFunction(string name, double a, double c) : base(name, a, c)
    {
        A = a;
        C = c;
    }

    protected override double A { get; }
    protected override double C { get; }
    
    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => double.PositiveInfinity;
}