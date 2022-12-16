using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : BaseTriangularFunction<double>, IRealFunction, IClosedSurface
{
    public TriangularFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public double CalculateArea(double errorMargin = IClosedSurface.DefaultErrorMargin)
    {
        var s = (A + B + C) / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedSurface.DefaultErrorMargin)
    {
        var (x1, x2) = LambdaCutInterval(y);
        var a = Math.Abs(x1 - x2);
        var b = Math.Abs(A - C);
        return 0.5 * Math.Sqrt(a * b) * y.Value;
    }
}