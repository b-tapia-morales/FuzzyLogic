using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : BaseGaussianFunction<double>, IRealFunction
{
    public GaussianFunction(string name, double m, double o) : base(name, m, o)
    {
        M = m;
        O = o;
    }

    protected override double M { get; }
    protected override double O { get; }
    
    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => double.PositiveInfinity;
}