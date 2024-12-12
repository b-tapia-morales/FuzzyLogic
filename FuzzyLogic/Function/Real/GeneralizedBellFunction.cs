using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class GeneralizedBellFunction : AsymptoteFunction
{
    private double? _leftMost;
    private double? _rightMost;

    public GeneralizedBellFunction(string name, double a, double b, double c, double uMax = 1) : base(name, uMax)
    {
        CheckAValue(a);
        CheckValues(a, b, c);
        A = a;
        B = b;
        C = Inflection = c;
    }

    public override double Inflection { get; }

    public double A { get; }
    public double B { get; }
    public double C { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => true;

    public override bool IsPrototypical() => false;

    public override double? PeakLeft() => C;

    public override double? PeakRight() => C;

    public override double? CoreLeft() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? C : null;

    public override double? CoreRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? C : null;

    public override double? AlphaCutLeft(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(alpha.Value - UMax) <= FuzzyNumber.Epsilon)
            return C;
        return C - A * Pow((1 - alpha.Value) / alpha.Value, 1 / (2 * B));
    }

    public override double? AlphaCutRight(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(alpha.Value - UMax) <= FuzzyNumber.Epsilon)
            return C;
        return C + A * Pow((1 - alpha.Value) / alpha.Value, 1 / (2 * B));
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) =>
        x => lambda.Value * (1 / (1 + Pow(Abs((x - C) / A), 2 * B)));

    public override IMembershipFunction DeepCopy() => new GeneralizedBellFunction(Name, A, B, C, UMax);

    public override IMembershipFunction DeepCopyRenamed(string name) => new GeneralizedBellFunction(name, A, B, C, UMax);

    public override bool IsMonotonicallyIncreasing() => false;

    public override bool IsMonotonicallyDecreasing() => false;

    public override bool IsUnimodal() => true;

    public override double ApproxSupportLeft() => _leftMost ??= AlphaCutLeft(FuzzyNumber.Epsilon)!.Value;

    public override double ApproxSupportRight() => _rightMost ??= AlphaCutRight(FuzzyNumber.Epsilon)!.Value;

    public override double? ApproxCoreLeft() => CoreLeft();

    public override double? ApproxCoreRight() => CoreRight();

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Generalized Bell - Sides: (a: {A}, b: {B}, c: {C}) - μMax: {UMax}";

    private static void CheckAValue(double a)
    {
        if (Abs(a) < IMembershipFunction.DeltaX)
            throw new ArgumentException("The value for «a» cannot be equal to 0");
    }

    private static void CheckValues(double a, double b, double c)
    {
        if (b < a || Abs(b - a) <= IMembershipFunction.DeltaX || c < b || Abs(c - b) <= IMembershipFunction.DeltaX)
            throw new ArgumentException("a < b < c");
    }
}