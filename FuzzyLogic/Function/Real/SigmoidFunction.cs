using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class SigmoidFunction : AsymptoteFunction
{
    private double? _supportLeft;
    private double? _supportRight;
    private double? _coreLeft;
    private double? _coreRight;

    public override double Inflection { get; }

    public SigmoidFunction(string name, double a, double c, double uMax = 1) : base(name, uMax)
    {
        CheckAValue(A);
        A = a;
        C = Inflection = c;
    }

    public double A { get; }
    public double C { get; }

    public override bool IsOpenLeft() => A < 0;

    public override bool IsOpenRight() => A > 0;

    public override bool IsSymmetric() => false;

    public override bool IsSingleton() => false;

    public override double? PeakLeft() => null;

    public override double? PeakRight() => null;

    public override double? CoreLeft() => null;

    public override double? CoreRight() => null;

    public override double? AlphaCutLeft(FuzzyNumber cut)
    {
        if (cut.Value > UMax || Abs(UMax - cut.Value) <= FuzzyNumber.Epsilon || Abs(cut.Value) <= FuzzyNumber.Epsilon)
            return null;
        return IsMonotonicallyDecreasing() ? double.NegativeInfinity : C - (1 / A) * Log(1 / cut.Value - 1);
    }

    public override double? AlphaCutRight(FuzzyNumber cut)
    {
        if (cut.Value > UMax || Abs(UMax - cut.Value) <= FuzzyNumber.Epsilon || Abs(cut.Value) <= FuzzyNumber.Epsilon)
            return null;
        return IsMonotonicallyIncreasing() ? double.PositiveInfinity : C + (1 / A) * Log(1 / cut.Value - 1);
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) =>
        x => lambda.Value * (1 / (1 + Exp(-A * (x - C))));

    public override bool IsMonotonicallyIncreasing() => A > 0;

    public override bool IsMonotonicallyDecreasing() => A < 0;

    public override bool IsUnimodal() => false;

    public override double ApproxSupportLeft() =>
        _supportLeft ??= AlphaCutLeft(IsMonotonicallyIncreasing() ? FuzzyNumber.Epsilon : Abs(UMax - FuzzyNumber.Epsilon))!.Value;

    public override double ApproxSupportRight() =>
        _supportRight ??= AlphaCutRight(IsMonotonicallyDecreasing() ? FuzzyNumber.Epsilon : Abs(UMax - FuzzyNumber.Epsilon))!.Value;

    public override double? ApproxCoreLeft() =>
        _coreLeft ??= AlphaCutLeft(IsMonotonicallyDecreasing() ? FuzzyNumber.Epsilon : Abs(UMax - FuzzyNumber.Epsilon))!.Value;

    public override double? ApproxCoreRight() =>
        _coreRight ??= AlphaCutRight(IsMonotonicallyIncreasing() ? FuzzyNumber.Epsilon : Abs(UMax - FuzzyNumber.Epsilon))!.Value;

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Sigmoidal - Sides: (A: {A}, C: {C}) - μMax: {UMax}";

    private static void CheckAValue(double a)
    {
        if (Abs(a) < IMembershipFunction.DeltaX)
            throw new ArgumentException("The value for «A» cannot be equal to 0");
    }
}