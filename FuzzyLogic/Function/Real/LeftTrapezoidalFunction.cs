using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class LeftTrapezoidalFunction : BaseLeftTrapezoidalFunction<double>, IRealFunction
{
    public LeftTrapezoidalFunction(string name, double a, double b) : base(name, a, b)
    {
    }

    public override double LowerBoundary() => double.NegativeInfinity;

    public override double UpperBoundary() => B;

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => throw new NotImplementedException();
}