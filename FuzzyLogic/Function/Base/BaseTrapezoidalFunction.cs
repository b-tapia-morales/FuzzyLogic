using FuzzyLogic.Number;
using FuzzyLogic.Utils;
using static System.Math;
using static FuzzyLogic.Function.Interface.IMembershipFunction<double>;

namespace FuzzyLogic.Function.Base;

public class BaseTrapezoidalFunction : BaseMembershipFunction
{
    private readonly bool _isSymmetric;

    protected BaseTrapezoidalFunction(string name, double a, double b, double c, double d, double uMax = 1) : base(name, uMax)
    {
        CheckEdges(a, b, C, D);
        CheckSides(a, b, C, D);
        A = a;
        B = b;
        C = c;
        D = d;
        _isSymmetric = Abs(
            TrigonometricUtils.Distance((A, 0), (B, UMax)) -
            TrigonometricUtils.Distance((C, UMax), (D, 0))
        ) < FuzzyNumber.Epsilon;
    }

    protected double A { get; }
    protected double B { get; }
    protected double C { get; }
    protected double D { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => Abs(1 - UMax) <= FuzzyNumber.Epsilon;

    public override bool IsSymmetric() => _isSymmetric;

    public override bool IsSingleton() => false;

    public override double SupportLeft() => A;

    public override double SupportRight() => D;

    public override double? CoreLeft() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? B : null;

    public override double? CoreRight() =>
        Abs(1 - UMax) <= FuzzyNumber.Epsilon ? C : null;

    public override double? AlphaCutLeft(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return B;
        return A + cut.Value * (B - A);
    }

    public override double? AlphaCutRight(FuzzyNumber cut)
    {
        if (cut.Value > UMax)
            return null;
        if (Abs(cut.Value - UMax) <= FuzzyNumber.Epsilon)
            return B;
        return D - cut.Value * (D - C);
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

    private static void CheckEdges(double a, double b, double c, double d)
    {
        if (a > b || b >= c || c > d)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: a ≤ b < C ≤ D (Values provides were: {a}, {b}, {c}, {d})
                 The resulting shape is not a Trapezoid.
                 """);
    }

    private static void CheckSides(double a, double b, double c, double d)
    {
        if (Abs(a - b) < DeltaX && Abs(c - d) < DeltaX)
            throw new ArgumentException(
                $"""
                 The following condition has been violated: a ≠ b ∨ C ≠ D (Values provides were: {a}, {b}, {c}, {d})
                 The resulting shape is either a Rectangle or a Square
                 """);
    }
}