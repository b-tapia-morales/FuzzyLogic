using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : BaseTriangularFunction<double>, IRealFunction, IClosedSurface
{
    public TriangularFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    public double CalculateArea(double errorMargin = IClosedSurface.DefaultErrorMargin) => 0.5 * Math.Abs(A - C);

    public double CalculateArea(FuzzyNumber y, double errorMargin = IClosedSurface.DefaultErrorMargin)
    {
        var (x1, x2) = LambdaCutInterval(y);
        var a = Math.Abs(x1 - x2);
        var b = Math.Abs(A - C);
        return 0.5 * (a + b) * y.Value;
    }
}