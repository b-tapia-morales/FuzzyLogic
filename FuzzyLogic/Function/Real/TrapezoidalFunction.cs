using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;

namespace FuzzyLogic.Function.Real;

public class TrapezoidalFunction : BaseTrapezoidalFunction<double>, IRealFunction, IClosedShape
{
    public TrapezoidalFunction(string name, double a, double b, double c, double d, double h) :
        base(name, a, b, c, d, h)
    {
    }

    public TrapezoidalFunction(string name, double a, double b, double c, double d) : base(name, a, b, c, d)
    {
    }

    public double CalculateArea(double errorMargin = IClosedShape.DefaultErrorMargin) =>
        TrigonometricUtils.TrapezoidArea(Math.Abs(B - C), Math.Abs(A - D), H);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedShape.DefaultErrorMargin)
    {
        if (y == 0) throw new ArgumentException("Can't calculate the area of the zero-function");
        if (y == 1) return CalculateArea(errorMargin);
        var (x1, x2) = LambdaCutInterval(y);
        return TrigonometricUtils.TrapezoidArea(Math.Abs(x1 - x2), Math.Abs(A - D), y.Value);
    }

    public (double X, double Y) CalculateCentroid(double errorMargin = IClosedShape.DefaultErrorMargin)
    {
        var a = Math.Abs(C - D);
        var b = Math.Abs(A - D);
        return ((1 / 2.0), (1 / 3.0) * (2 * a + b) / (a + b));
    }

    public (double X, double Y) CalculateCentroid(FuzzyNumber y,
        double errorMargin = IClosedShape.DefaultErrorMargin)
    {
        if (y == 0) throw new ArgumentException("Can't calculate the centroid of the zero-function");
        if (y == 1) return CalculateCentroid(errorMargin);
        var (x1, x2) = LambdaCutInterval(y);
        var a = Math.Abs(x1 - x2);
        var b = Math.Abs(A - D);
        return ((1 / 2.0), (1 / 3.0) * (2 * a + b) / (a + b));
    }
}