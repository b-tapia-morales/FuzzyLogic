using MathNet.Numerics.Integration;

namespace FuzzyLogic.Function.Interface;

public interface IClosedShape : IMembershipFunction<double>
{
    const double DefaultErrorMargin = 1e-5;

    double CalculateArea(double errorMargin = DefaultErrorMargin) => CalculateArea(this, errorMargin);

    double CentroidXCoordinate(double errorMargin = DefaultErrorMargin) => CentroidXCoordinate(this, errorMargin);

    double CentroidYCoordinate(double errorMargin = DefaultErrorMargin) => CentroidYCoordinate(this, errorMargin);

    (double X, double Y) CalculateCentroid(double errorMargin = DefaultErrorMargin) =>
        (CentroidXCoordinate(errorMargin), CentroidYCoordinate(errorMargin));

    static double Integrate(Func<double, double> function, double x0, double x1,
        double errorMargin = DefaultErrorMargin) =>
        NewtonCotesTrapeziumRule.IntegrateAdaptive(function, x0, x1, errorMargin);

    static double CalculateArea(IMembershipFunction<double> mf, double errorMargin = DefaultErrorMargin)
    {
        var (x1, x2) = mf.ClosedInterval();
        return Integrate(mf.AsFunction(), x1, x2, errorMargin);
    }

    static double CentroidXCoordinate(IMembershipFunction<double> mf, double errorMargin = DefaultErrorMargin)
    {
        double Integral(double x) => x * mf.AsFunction().Invoke(x);
        var (x1, x2) = mf.ClosedInterval();
        var area = CalculateArea(mf, errorMargin);
        return (1 / area) * Integrate(Integral, x1, x2, errorMargin);
    }

    static double CentroidYCoordinate(IMembershipFunction<double> mf, double errorMargin = DefaultErrorMargin)
    {
        double Integral(double x) => mf.AsFunction().Invoke(x) * mf.AsFunction().Invoke(x);
        var (x1, x2) = mf.ClosedInterval();
        var area = CalculateArea(mf, errorMargin);
        return (1 / (2.0 * area)) * Integrate(Integral, x1, x2, errorMargin);
    }
}