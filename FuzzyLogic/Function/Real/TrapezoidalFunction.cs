using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Real;

public class TrapezoidalFunction : BaseTrapezoidalFunction<double>, IRealFunction
{
    public TrapezoidalFunction(string name, double a, double b, double c, double d): base(name, a, b, c, d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    protected override double A { get; }
    protected override double B { get; }
    protected override double C { get; }
    protected override double D { get; }
}