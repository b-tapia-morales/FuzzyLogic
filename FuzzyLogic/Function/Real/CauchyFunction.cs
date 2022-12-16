using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class CauchyFunction: BaseCauchyFunction<double>, IRealFunction, IClosedSurface
{
    public CauchyFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => double.PositiveInfinity;
    
    public double CalculateArea(double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        IClosedSurface.CalculateArea(this, errorMargin);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        IClosedSurface.CalculateArea(this, y, errorMargin);
}