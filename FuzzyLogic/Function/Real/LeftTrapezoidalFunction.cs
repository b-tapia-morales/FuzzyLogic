using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class LeftTrapezoidalFunction : MembershipFunction
{
    internal protected LeftTrapezoidalFunction(string name, double a, double b, double uMax = 1) : base(name, uMax)
    {
        CheckValues(a, b);
        A = a;
        B = b;
    }

    public double A { get; }
    public double B { get; }

    public override bool IsOpenLeft() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => false;

    public override bool IsPrototypical() => false;

    public override double? PeakLeft() => double.NegativeInfinity;

    public override double? PeakRight() => A;

    public override double SupportLeft() => double.NegativeInfinity;

    public override double SupportRight() => B;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? double.NegativeInfinity : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? A : null;

    public override double? AlphaCutLeft(FuzzyNumber alpha) =>
        alpha.Value > UMax ? null : double.NegativeInfinity;

    public override double? AlphaCutRight(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(UMax - alpha.Value) <= FuzzyNumber.Epsilon)
            return A;
        return B - alpha.Value * (B - A);
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

    public override IMembershipFunction DeepCopy() => new LeftTrapezoidalFunction(Name, A, B, UMax);

    public override IMembershipFunction DeepCopyRenamed(string name) => new LeftTrapezoidalFunction(name, A, B, UMax);

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Open Left Trapezoidal - Sides: (a: {A}, b: {B}) - μMax: {UMax}";

    private static void CheckValues(double a, double b)
    {
        if (a >= b)
            throw new ArgumentException(
                $"The following condition has been violated: a < b (Values provides were: {a}, {b})");
    }
}