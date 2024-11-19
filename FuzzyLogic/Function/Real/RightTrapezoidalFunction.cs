using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class RightTrapezoidalFunction : MembershipFunction
{
    protected RightTrapezoidalFunction(string name, double a, double b, double uMax = 1) : base(name, uMax)
    {
        CheckValues(a, b);
        A = a;
        B = b;
    }

    protected double A { get; }
    protected double B { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override bool IsSymmetric() => false;

    public override bool IsSingleton() => false;

    public override double SupportLeft() => A;

    public override double SupportRight() => double.PositiveInfinity;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? double.PositiveInfinity : null;

    public override double? AlphaCutLeft(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(UMax - cut.Value) <= FuzzyNumber.Epsilon)
            return A;
        return A + cut.Value * (B - A);
    }

    public override double? AlphaCutRight(FuzzyNumber cut) =>
        cut.Value > UMax ? null : double.PositiveInfinity;

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) => x =>
    {
        if (x > A && x < B)
            return lambda.Value * ((x - A) / (B - A));
        if (x >= B)
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