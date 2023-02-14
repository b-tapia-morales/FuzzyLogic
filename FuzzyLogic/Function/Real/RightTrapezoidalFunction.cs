using FuzzyLogic.Function.Base;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Real;

public class RightTrapezoidalFunction : BaseLeftTrapezoidalFunction<double>, IMembershipFunction<double>
{
    public RightTrapezoidalFunction(string name, double a, double b, double h) : base(name, a, b, h)
    {
    }

    public RightTrapezoidalFunction(string name, double a, double b) : base(name, a, b)
    {
    }

    public override double SupportLeftEndpoint() => A;

    public override double SupportRightEndpoint() => double.PositiveInfinity;

    public override double MaxHeightLeftEndpoint() => A;

    public override double MaxHeightRightEndpoint() => double.PositiveInfinity;
}