using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : BaseGaussianFunction<double>, IRealFunction
{
    public GaussianFunction(string name, double m, double o) : base(name, m, o)
    {
    }

    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => double.PositiveInfinity;
}