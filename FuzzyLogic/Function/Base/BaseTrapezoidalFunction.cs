using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Utils;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private bool? _isSymmetric;

    protected BaseTrapezoidalFunction(string name, T a, T b, T c, T d, double h = 1) : base(name, h)
    {
        CheckEdges(a, b, c, d);
        CheckSides(a, b, c, d);
        A = a;
        B = b;
        C = c;
        D = d;
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }
    protected T D { get; }

    public static Func<T, double> AsFunction(double a, double b, double c, double d, double h = 1) => t =>
    {
        var x = t.ToDouble(null);
        if (x > a && x < b)
            return h * ((x - a) / (b - a));
        if (x >= b && x <= c)
            return h;
        if (x > c && x < d)
            return h * ((d - x) / (d - c));
        return 0;
    };

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => _isSymmetric ??=
        Abs(
            TrigonometricUtils.Distance((A.ToDouble(null), 0), (B.ToDouble(null), H)) -
            TrigonometricUtils.Distance((C.ToDouble(null), H), (D.ToDouble(null), 0))
        ) < ITrapezoidalFunction<T>.DistanceTolerance;

    public override bool IsSingleton() => false;

    public override T SupportLeftEndpoint() => A;

    public override T SupportRightEndpoint() => D;

    public (T X0, T X1)? CoreInterval() => (B, C);

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), D.ToDouble(null), H);

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), D.ToDouble(null), Min(H, y));

    private static void CheckEdges(T a, T b, T c, T d)
    {
        if (a > b || b >= c || c > d)
            throw new ArgumentException(
                $"""
                    The following condition has been violated: a ≤ b < c ≤ d (Values provides were: {a}, {b}, {c}, {d})
                    The resulting shape is not a Trapezoid.
                    """);
    }

    private static void CheckSides(T a, T b, T c, T d)
    {
        if (a == b && c == d)
            throw new ArgumentException(
                $"""
                    The following condition has been violated: a ≠ b ∨ c ≠ d (Values provides were: {a}, {b}, {c}, {d})
                    The resulting shape is either a Rectangle or a Square
                    """);
    }
}