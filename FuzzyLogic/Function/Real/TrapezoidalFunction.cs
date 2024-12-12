using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using static System.Math;

// ReSharper disable MemberCanBePrivate.Global

namespace FuzzyLogic.Function.Real;

public class TrapezoidalFunction : MembershipFunction
{
    private readonly bool _isSymmetric;

    public TrapezoidalFunction(string name, double a, double b, double c, double d, double uMax = 1) : base(name, uMax)
    {
        CheckIfTriangle(b, c);
        CheckIfRectangular(a, b, c, d);
        CheckEdges(a, b, c, d);
        A = a;
        B = b;
        C = c;
        D = d;
        _isSymmetric = Abs(
            TrigonometricUtils.Distance((A, 0), (B, UMax)) -
            TrigonometricUtils.Distance((C, UMax), (D, 0))
        ) < FuzzyNumber.Epsilon;
    }

    public double A { get; }
    public double B { get; }
    public double C { get; }
    public double D { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override bool IsSymmetric() => _isSymmetric;

    public override bool IsPrototypical() => false;

    public override double? PeakLeft() => B;

    public override double? PeakRight() => C;

    public override double SupportLeft() => A;

    public override double SupportRight() => D;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? C : null;

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
        return D - alpha.Value * (D - C);
    }

    public override Func<double, double> LarsenProduct(FuzzyNumber lambda) => x =>
    {
        if (x > A && x < B)
            return lambda.Value * ((x - A) / (B - A));
        if (x >= B && x <= C)
            return lambda.Value;
        if (x > C && x < D)
            return lambda.Value * ((D - x) / (D - C));
        return 0;
    };

    public override IMembershipFunction DeepCopy() => new TrapezoidalFunction(Name, A, B, C, D, UMax);

    public override IMembershipFunction DeepCopyRenamed(string name) => new TrapezoidalFunction(name, A, B, C, D, UMax);

    public override string ToString() => $"Linguistic term: {Name} - Membership Function: Trapezoidal - Sides: (a: {A}, b: {B}, c: {C}, d: {D}) - μMax: {UMax}";

    private static void CheckIfTriangle(double b, double c)
    {
        if (Abs(b - c) <= IMembershipFunction.DeltaX)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: B ≠ C (Values provides were: {b}, {c}).
                 The resulting shape is a Triangle, not a Trapezoidal.
                 If you wish to use a triangular function, create an instance of the {nameof(TriangularFunction)} class instead."");
                 """);
    }

    private static void CheckIfRectangular(double a, double b, double c, double d)
    {
        if (Abs(b - a) < IMembershipFunction.DeltaX && Abs(d - c) < IMembershipFunction.DeltaX)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: A ≠ B ∨ C ≠ D (Values provides were: {a}, {b}, {c}, {d})
                 The resulting shape is either a Rectangle or a Square, not a Trapezoidal.
                 """);
    }

    private static void CheckEdges(double a, double b, double c, double d)
    {

        if (a > b || b > c || c > d)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: A <= B < C <= D (Values provides were: {a}, {b}, {c}, {d})
                 The resulting shape is not a Trapezoid.
                 """);

    }
}