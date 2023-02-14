using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Utils;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseTriangularFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private bool? _isSymmetric;

    protected BaseTriangularFunction(string name, T a, T b, T c, double h = 1) : base(name, h)
    {
        CheckEdges(a, b, c);
        CheckSides(a, b, c);
        A = a;
        B = b;
        C = c;
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }

    public static Func<T, double> AsFunction(double a, double b, double c, double h = 1) => t =>
    {
        var x = t.ToDouble(null);
        if (x > a && x < b)
            return h * ((x - a) / (b - a));
        if (Abs(x - b) < 1E-5)
            return h;
        if (x > b && x < c)
            return h * ((c - x) / (c - b));
        return 0;
    };

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => _isSymmetric ??=
        Abs(
            TrigonometricUtils.Distance((A.ToDouble(null), 0), (B.ToDouble(null), H)) -
            TrigonometricUtils.Distance((B.ToDouble(null), H), (C.ToDouble(null), 0))
        ) < ITrapezoidalFunction<T>.DistanceTolerance;

    public override bool IsSingleton() => Abs(H - 1) < 1E-5;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), H);

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), Min(H, y));

    public override T SupportLeftEndpoint() => A;

    public override T SupportRightEndpoint() => C;

    public virtual (T X0, T X1)? CoreInterval() => (A, C);
    
    private static void CheckEdges(T a, T b, T c)
    {
        if (a > b || b > c)
            throw new ArgumentException(
                $"""
                    The following condition has been violated: a ≤ b ≤ c (Values provides were: {a}, {b}, {c})
                    The resulting shape is not a Triangle.
                    """);
    }
    
    private static void CheckSides(T a, T b, T c)
    {
        if (a == b && b == c)
            throw new ArgumentException(
                $"""
                    The following condition has been violated: a ≠ b ∨ b ≠ c (Values provides were: {a}, {b}, {c})
                    The resulting membership function is the Singleton function.
                    """);
    }
}