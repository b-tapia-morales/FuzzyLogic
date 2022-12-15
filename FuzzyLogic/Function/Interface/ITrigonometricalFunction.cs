using FuzzyLogic.Number;
using MathNet.Numerics.Integration;

namespace FuzzyLogic.Function.Interface;

public interface ITrigonometricalFunction : IMembershipFunction<double>
{
    const double DefaultErrorMargin = 1e-5;

    static double Integrate(Func<double, double> function, double x0, double x1,
        double errorMargin = DefaultErrorMargin) =>
        NewtonCotesTrapeziumRule.IntegrateAdaptive(function, x0, x1, errorMargin);

    double CalculateArea(double errorMargin = DefaultErrorMargin)
    {
        var (x0, x1) = ClosedInterval();
        return Integrate(SimpleFunction(), x0, x1, errorMargin);
    }

    double CalculateArea(FuzzyNumber y, double errorMargin = DefaultErrorMargin)
    {
        if (y == 0) return 0.0;
        if (y == 1) return CalculateArea();
        var (x0, x1) = ClosedInterval();
        return Integrate(LambdaCutFunction(y), x0, x1, errorMargin);
    }

    double CalculateCentroid(double errorMargin = DefaultErrorMargin)
    {
        var (x0, x1) = ClosedInterval();
        var area = CalculateArea();
        return Integrate(x => x * MembershipDegree(x) / area, x0, x1, errorMargin);
    }

    double CalculateCentroid(FuzzyNumber y, double errorMargin = DefaultErrorMargin)
    {
        if (y == 0) return 0.0;
        if (y == 1) return CalculateCentroid();
        var (x0, x1) = ClosedInterval();
        var area = CalculateArea(y);
        return Integrate(x => x * LambdaCutFunction(y).Invoke(x) / area, x0, x1, errorMargin);
    }
}