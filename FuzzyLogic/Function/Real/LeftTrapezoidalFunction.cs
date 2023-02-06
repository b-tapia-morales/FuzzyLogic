using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Real;

public class LeftTrapezoidalFunction : BaseLeftTrapezoidalFunction<double>, IMembershipFunction<double>
{
    public LeftTrapezoidalFunction(string name, double a, double b, double h) : base(name, a, b, h)
    {
    }

    public LeftTrapezoidalFunction(string name, double a, double b) : base(name, a, b)
    {
    }

    public override double LeftSupportEndpoint() => double.NegativeInfinity;

    public override double RightSupportEndpoint() => B;

    public override double MaxHeightLeftEndpoint() => double.NegativeInfinity;

    public override double MaxHeightRightEndpoint() => B;
}