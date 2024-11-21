using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using static System.Math;

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : MembershipFunction
{
    private readonly bool _isSymmetric;

    internal protected TriangularFunction(string name, double a, double b, double c, double uMax = 1) : base(name, uMax)
    {
        CheckEdges(a, b, c);
        CheckSides(a, b, c);
        A = a;
        B = b;
        C = c;
        _isSymmetric = Abs(
            TrigonometricUtils.Distance((A, 0), (B, UMax)) -
            TrigonometricUtils.Distance((B, UMax), (C, 0))
        ) < IMembershipFunction.DeltaX;
    }

    protected double A { get; }
    protected double B { get; }
    protected double C { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => _isSymmetric;

    public override bool IsSingleton() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override double SupportLeft() => A;

    public override double SupportRight() => C;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? AlphaCutLeft(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return C;
        return A + cut.Value * (B - A);
    }

    public override double? AlphaCutRight(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return C;
        return C - cut.Value * (C - B);
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) => x =>
    {
        if (x > A && x < B)
            return lambda.Value * ((x - A) / (B - A));
        if (Abs(x - B) < IMembershipFunction.DeltaX)
            return lambda.Value;
        if (x > B && x < C)
            return lambda.Value * ((C - x) / (C - B));
        return 0;
    };

    private static void CheckEdges(double a, double b, double c)
    {
        if (a > b || b > c)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: a ≤ b ≤ c (Values provides were: {a}, {b}, {c})
                 The resulting shape is not a Triangle.
                 """);
    }

    private static void CheckSides(double a, double b, double c)
    {
        if (Abs(a - b) < IMembershipFunction.DeltaX && Abs(b - c) < IMembershipFunction.DeltaX)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: a ≠ b ∨ b ≠ c (Values provides were: {a}, {b}, {c})
                 The resulting membership function is the Singleton function.
                 """);
    }
}