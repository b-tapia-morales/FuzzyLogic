using MathNet.Numerics.Integration;

namespace FuzzyLogic.Function.Interface;

public interface IClosedShape : IMembershipFunction
{
    const double ErrorMargin = 1e-4;

    double CalculateArea(double errorMargin = ErrorMargin)
    {
        var (x0, x1) = FiniteSupportBoundary();
        return Integrate(PureFunction(), x0, x1, errorMargin);
    }

    double CentroidXCoordinate(double errorMargin = ErrorMargin)
    {
        var (x0, x1) = FiniteSupportBoundary();
        var area = CalculateArea(errorMargin);
        return (1 / area) * Integrate(Integral, x0, x1, errorMargin);
        double Integral(double x) => PureFunction()(x);
    }

    double CentroidYCoordinate(double errorMargin = ErrorMargin)
    {
        var (x0, x1) = FiniteSupportBoundary();
        var area = CalculateArea(errorMargin);
        return (1 / (2 * area)) * Integrate(Integral, x0, x1, errorMargin);
        double Integral(double x) => PureFunction()(x) * PureFunction()(x);
    }

    (double X, double Y) CalculateCentroid(double errorMargin = ErrorMargin) =>
        (CentroidXCoordinate(errorMargin), CentroidYCoordinate(errorMargin));

    static double Integrate(Func<double, double> function, double x0, double x1,
        double errorMargin = ErrorMargin) =>
        NewtonCotesTrapeziumRule.IntegrateAdaptive(function, x0, x1, errorMargin);
}