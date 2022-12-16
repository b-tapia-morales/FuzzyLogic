using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : BaseTriangularFunction<double>, IRealFunction
{
    public TriangularFunction(string name, double a, double b, double c) : base(name, a, b, c)
    {
    }

    double ITrigonometricalFunction.CalculateArea(double errorMargin)
    {
        var s = (A + B + C) / 2;
        return Math.Sqrt(s * (s - A) * (s - B) * (s - C));
    }
    
    double ITrigonometricalFunction.CalculateArea(FuzzyNumber y, double errorMargin)
    {
        var (x1, x2) = LambdaCutInterval(y);
        var a = Math.Abs(x1 - x2);
        var b = Math.Abs(A - C);
        return 0.5 * Math.Sqrt(a * b) * y.Value;
    }
}