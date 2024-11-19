using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;
using static FuzzyLogic.Function.Interface.IMembershipFunction<double>;

namespace FuzzyLogic.Function.Real;

public class GeneralizedBellFunction : MembershipFunction, IAsymptoteFunction<double>
{
    private double? _leftMost;
    private double? _rightMost;

    public double Inflection { get; }

    protected GeneralizedBellFunction(string name, double a, double b, double c, double uMax = 1) : base(name, uMax)
    {
        CheckAValue(a);
        A = a;
        B = b;
        C = Inflection = c;
    }

    protected double A { get; }
    protected double B { get; }
    protected double C { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => true;

    public override bool IsSingleton() => false;

    public override double SupportLeft() => double.NegativeInfinity;

    public override double SupportRight() => double.PositiveInfinity;

    public override double? CoreLeft() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? C : null;

    public override double? CoreRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon ? C : null;

    public override double? AlphaCutLeft(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return C;
        return C - A * Pow((1 - cut.Value) / cut.Value, 1 / (2 * B));
    }

    public override double? AlphaCutRight(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return C;
        return C + A * Pow((1 - cut.Value) / cut.Value, 1 / (2 * B));
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) =>
        x => lambda.Value * (1 / (1 + Pow(Abs((x - C) / A), 2 * B)));

    public bool IsMonotonicallyIncreasing() => false;

    public bool IsMonotonicallyDecreasing() => false;

    public bool IsUnimodal() => true;

    public double ApproxSupportLeft() => _leftMost ??= AlphaCutLeft(FuzzyNumber.Epsilon)!.Value;

    public double ApproxSupportRight() => _rightMost ??= AlphaCutRight(FuzzyNumber.Epsilon)!.Value;

    public double? ApproxCoreLeft() => CoreLeft();

    public double? ApproxCoreRight() => CoreRight();

    private static void CheckAValue(double a)
    {
        if (Abs(a) < DeltaX)
            throw new ArgumentException("The value for «a» cannot be equal to 0");
    }
}