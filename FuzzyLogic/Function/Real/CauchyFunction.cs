using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class CauchyFunction : BaseCauchyFunction<double>, IRealFunction, IClosedShape
{
    public CauchyFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public override double LeftSupportEndpoint() => double.NegativeInfinity;

    public override double RightSupportEndpoint() => double.PositiveInfinity;

    public double CalculateArea(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        IClosedShape.CalculateArea(this, errorMargin);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedShape.DefaultErrorMargin) =>
        IClosedShape.CalculateArea(this, y, errorMargin);

    public (double X, double Y) CalculateCentroid(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        (C, IClosedShape.CentroidYCoordinate(this));

    public (double X, double Y) CalculateCentroid(FuzzyNumber y,
        double errorMargin = IClosedShape.DefaultErrorMargin) =>
        (C, IClosedShape.CentroidYCoordinate(this, y, errorMargin));
}