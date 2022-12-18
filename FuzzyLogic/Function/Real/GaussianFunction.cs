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

    public (double X, double Y) CalculateCentroid(double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        (M, 0);

    public (double X, double Y) CalculateCentroid(FuzzyNumber y,
        double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        (M, IClosedSurface.CentroidYCoordinate(this, y, errorMargin));
}