using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class GaussianFunction : BaseGaussianFunction<double>, IRealFunction, IClosedShape
{
    public GaussianFunction(string name, double m, double o) : base(name, m, o)
    {
    }

    public override double LeftSupportEndpoint() => double.NegativeInfinity;

    public override double RightSupportEndpoint() => double.PositiveInfinity;

    public double CalculateArea(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        IClosedShape.CalculateArea(this, errorMargin);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedShape.DefaultErrorMargin) =>
        IClosedShape.CalculateArea(this, y, errorMargin);

    public (double X, double Y) CalculateCentroid(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        (M, 0);

    public (double X, double Y) CalculateCentroid(FuzzyNumber y,
        double errorMargin = IClosedShape.DefaultErrorMargin) =>
        (M, IClosedShape.CentroidYCoordinate(this, y, errorMargin));
}