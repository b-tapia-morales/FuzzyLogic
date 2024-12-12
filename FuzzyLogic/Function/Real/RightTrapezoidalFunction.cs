using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class RightTrapezoidalFunction : MembershipFunction
{
    protected RightTrapezoidalFunction(string name, double a, double b, double uMax = 1) : base(name, uMax)
    {
        CheckValues(a, b);
        A = a;
        B = b;
    }

    public double A { get; }
    public double B { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override bool IsSymmetric() => false;

    public override bool IsPrototypical() => false;

    public override double? PeakLeft() => B;

    public override double? PeakRight() => double.PositiveInfinity;

    public override double SupportLeft() => A;

    public override double SupportRight() => double.PositiveInfinity;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? double.PositiveInfinity : null;

    public override double? AlphaCutLeft(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(UMax - alpha.Value) <= FuzzyNumber.Epsilon)
            return A;
        return A + alpha.Value * (B - A);
    }

    public override double? AlphaCutRight(FuzzyNumber alpha) =>
        alpha.Value > UMax ? null : double.PositiveInfinity;

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) => x =>
    {
        if (x > A && x < B)
            return lambda.Value * ((x - A) / (B - A));
        if (x >= B)
            return lambda.Value;
        return 0;
    };

    public override IMembershipFunction DeepCopy() => new RightTrapezoidalFunction(Name, A, B, UMax);

    public override IMembershipFunction DeepCopyRenamed(string name) => new RightTrapezoidalFunction(name, A, B, UMax);

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Open Right Trapezoidal - Sides: (a: {A}, b: {B}) - μMax: {UMax}";

    private static void CheckValues(double a, double b)
    {
        if (a >= b)
            throw new ArgumentException(
                $"The following condition has been violated: a < b (Values provides were: {a}, {b})");
    }
}