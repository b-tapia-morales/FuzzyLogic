using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : BaseTriangularFunction<double>, IRealFunction, IClosedSurface
{
    public TriangularFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public double CalculateArea(double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        TrigonometricUtils.CalculateTriangleArea(A, C, 1);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedSurface.DefaultErrorMargin)
    {
        if (y == 0) throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y == 1) return CalculateArea(errorMargin);
        var (x1, x2) = LambdaCutInterval(y);
        return TrigonometricUtils.CalculateTrapezoidArea(A, x1, x2, C, y);
    }

    public (double X0, double X1) CalculateCentroid(double errorMargin = IClosedSurface.DefaultErrorMargin) =>
        ((A + B + C) / 3.0, 1 / 3.0);

    public (double X0, double X1) CalculateCentroid(FuzzyNumber y,
        double errorMargin = IClosedSurface.DefaultErrorMargin)
    {
        if (y == 0) throw new ArgumentException("Can't calculate the centroid of the zero-function");
        if (y == 1) return CalculateCentroid(errorMargin);
        var (x1, x2) = LambdaCutInterval(y);
        var a = Math.Abs(x1 - x2);
        var b = Math.Abs(A - C);
        return ((1 / 2.0), (1 / 3.0) * (2 * a + b) / (a + b));
    }
}