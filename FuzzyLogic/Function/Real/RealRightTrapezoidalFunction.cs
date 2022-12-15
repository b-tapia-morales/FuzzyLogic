using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Real;

public class RealRightTrapezoidalFunction : BaseLeftTrapezoidalFunction<double>, IRealFunction
{
    public RealRightTrapezoidalFunction(string name, double a, double b) : base(name, a, b)
    {
    }

    public override double LowerBoundary() => A;

    public override double UpperBoundary() => double.PositiveInfinity;

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => throw new NotImplementedException();
}