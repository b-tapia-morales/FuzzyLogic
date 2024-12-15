using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class TriangularFunction : MembershipFunction
{
    private readonly bool _isSymmetric;

    public TriangularFunction(string name, double a, double b, double c, double uMax = 1) : base(name, uMax)
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

    public double A { get; }
    public double B { get; }
    public double C { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => _isSymmetric;

    public override bool IsPrototypical() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override double? PeakLeft() => B;

    public override double? PeakRight() => B;

    public override double SupportLeft() => A;

    public override double SupportRight() => C;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? AlphaCutLeft(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(alpha.Value - UMax) <= FuzzyNumber.Epsilon)
            return B;
        return A + alpha.Value * (B - A);
    }

    public override double? AlphaCutRight(FuzzyNumber alpha)
    {
        if (alpha.Value > UMax)
            return null;
        if (Abs(alpha.Value - UMax) <= FuzzyNumber.Epsilon)
            return B;
        return C - alpha.Value * (C - B);
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

    public override IMembershipFunction DeepCopy() => new TriangularFunction(Name, A, B, C, UMax);

    public override IMembershipFunction DeepCopyRenamed(string name) => new TriangularFunction(name, A, B, C, UMax);

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Triangular - Sides: (a: {A}, b: {B}, c: {C}) - μMax: {UMax}";

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