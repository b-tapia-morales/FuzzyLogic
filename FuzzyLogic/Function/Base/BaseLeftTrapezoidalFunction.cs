using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public class BaseLeftTrapezoidalFunction : BaseMembershipFunction
{
    protected BaseLeftTrapezoidalFunction(string name, double a, double b, double uMax = 1) : base(name, uMax)
    {
        CheckValues(a, b);
        A = a;
        B = b;
    }

    protected double A { get; }
    protected double B { get; }

    public override bool IsOpenLeft() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => false;

    public override bool IsSingleton() => false;

    public override double SupportLeft() => double.NegativeInfinity;

    public override double SupportRight() => B;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? double.NegativeInfinity : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? A : null;

    public override double? AlphaCutLeft(FuzzyNumber cut) =>
        cut.Value > UMax ? null : double.NegativeInfinity;

    public override double? AlphaCutRight(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(UMax - cut.Value) <= FuzzyNumber.Epsilon)
            return A;
        return B - cut.Value * (B - A);
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) =>
        x =>
        {
            if (x > A && x < B)
                return lambda.Value * ((B - x) / (B - A));
            if (x <= A)
                return lambda.Value;
            return 0;
        };

    private static void CheckValues(double a, double b)
    {
        if (a >= b)
            throw new ArgumentException(
                $"The following condition has been violated: a < b (Values provides were: {a}, {b})");
    }
}