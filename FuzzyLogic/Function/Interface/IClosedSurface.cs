using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using MathNet.Numerics.Integration;

namespace FuzzyLogic.Function.Interface;

public interface IClosedSurface
{
    const double DefaultErrorMargin = 1e-5;

    double CalculateArea(double errorMargin = DefaultErrorMargin);

    double CalculateArea(FuzzyNumber y, double errorMargin = DefaultErrorMargin);

    (double X0, double X1) CalculateCentroid(double errorMargin = DefaultErrorMargin);

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
        if (y == 0) throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y == 1) return CalculateArea(function, errorMargin);
        var (x1, x2) = function.ClosedInterval();
        var (l1, l2) = function.LambdaCutInterval(y);
        var rectangleArea = Math.Abs(l1 - l2) * y.Value;
        var leftArea = Integrate(function.SimpleFunction(), x1, l1, errorMargin);
        var rightArea = Integrate(function.SimpleFunction(), l2, x2, errorMargin);
        return leftArea + rectangleArea + rightArea;
    }

    static (double X1, double X2) CalculateCentroid(IRealFunction function, double errorMargin = DefaultErrorMargin) =>
        (CentroidXCoordinate(function, errorMargin), CentroidXCoordinate(function, errorMargin));

    static double CentroidXCoordinate(IRealFunction function, double errorMargin = DefaultErrorMargin)
    {
        double Integral(double x) => x * function.SimpleFunction().Invoke(x);
        var (x1, x2) = function.ClosedInterval();
        var area = CalculateArea(function, errorMargin);
        return (1 / (2.0 * area)) * Integrate(Integral, x1, x2, errorMargin);
    }

    static double CentroidXCoordinate(IRealFunction function, FuzzyNumber y, double errorMargin = DefaultErrorMargin)
    {
        if (y == 0) throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y == 1) return CentroidXCoordinate(function, errorMargin);
        var lambdaCutFunction = function.LambdaCutFunction(y);
        double Integral(double x) => x * lambdaCutFunction.Invoke(x);
        var (x1, x2) = function.LambdaCutInterval(y);
        var area = CalculateArea(function, errorMargin);
        return (1 / (2.0 * area)) * Integrate(Integral, x1, x2, errorMargin);
    }

    static double CentroidYCoordinate(IRealFunction function, double errorMargin = DefaultErrorMargin)
    {
        double Integral(double x) => function.SimpleFunction().Invoke(x) * function.SimpleFunction().Invoke(x);
        var (x1, x2) = function.ClosedInterval();
        var area = CalculateArea(function, errorMargin);
        return (1 / (2.0 * area)) * Integrate(Integral, x1, x2, errorMargin);
    }

    static double CentroidYCoordinate(IRealFunction function, FuzzyNumber y, double errorMargin = DefaultErrorMargin)
    {
        var lambdaCutFunction = function.LambdaCutFunction(y);
        double Integral(double x) => lambdaCutFunction.Invoke(x) * lambdaCutFunction.Invoke(x);
        var (x1, x2) = function.ClosedInterval();
        var area = CalculateArea(function, errorMargin);
        return (1 / (2.0 * area)) * Integrate(Integral, x1, x2, errorMargin);
    }
}