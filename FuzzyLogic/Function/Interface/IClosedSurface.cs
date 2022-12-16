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

    static double CalculateArea(IRealFunction function, double errorMargin = DefaultErrorMargin)
    {
        var (x1, x2) = function.ClosedInterval();
        return Integrate(function.SimpleFunction(), x1, x2, errorMargin);
    }

    static double CalculateArea(IRealFunction function, FuzzyNumber y, double errorMargin = DefaultErrorMargin)
    {
        if (y == 0) return 0.0;
        if (y == 1) return CalculateArea(function, errorMargin);
        var (x1, x2) = function.ClosedInterval();
        var (l1, l2) = function.LambdaCutInterval(y);
        var trapezoidArea = Math.Abs(l1 - l2) * y.Value;
        var leftArea = Integrate(function.SimpleFunction(), x1, l1, errorMargin);
        var rightArea = Integrate(function.SimpleFunction(), l2, x2, errorMargin);
        return leftArea + trapezoidArea + rightArea;
    }
}