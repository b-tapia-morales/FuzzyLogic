using FuzzyLogic.Number;
using MathNet.Numerics.Integration;

namespace FuzzyLogic.Function.Interface;

public interface IClosedSurface
{
    const double DefaultErrorMargin = 1e-5;

    double CalculateArea(double errorMargin = DefaultErrorMargin);

    double CalculateArea(FuzzyNumber y, double errorMargin = DefaultErrorMargin);
    
    static double Integrate(Func<double, double> function, double x0, double x1,
        double errorMargin = DefaultErrorMargin) =>
        NewtonCotesTrapeziumRule.IntegrateAdaptive(function, x0, x1, errorMargin);
    
    static double DefaultCalculateArea(double errorMargin = DefaultErrorMargin)
    {
        return 0.0;
    }
}