using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : BaseGaussianFunction<double>, IRealFunction, IClosedSurface
{
    public GaussianFunction(string name, double m, double o) : base(name, m, o)
    {
    }

    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => double.PositiveInfinity;

    public double CalculateArea(double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        IClosedSurface.CalculateArea(this, errorMargin);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        IClosedSurface.CalculateArea(this, y, errorMargin);
}