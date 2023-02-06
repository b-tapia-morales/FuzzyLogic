using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Utils;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseTriangularFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private bool? _isSymmetric;

    protected BaseTriangularFunction(string name, T a, T b, T c, T h) : base(name)
    {
        A = a;
        B = b;
        C = c;
        H = h;
    }

    protected BaseTriangularFunction(string name, T a, T b, T c) : this(name, a, b, c, T.One)
    {
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }

    public static Func<T, double> AsFunction(double a, double b, double c, double h) => t =>
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

    public static Func<T, double> AsFunction(double a, double b, double c) => AsFunction(a, b, c, 1);


    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => _isSymmetric ??=
        Abs(
            TrigonometricUtils.Distance((A.ToDouble(null), 0), (B.ToDouble(null), H.ToDouble(null))) -
            TrigonometricUtils.Distance((B.ToDouble(null), H.ToDouble(null)), (C.ToDouble(null), 0))
        ) < ITrapezoidalFunction<T>.DistanceTolerance;

    public override bool IsSingleton() => H == T.One;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), H.ToDouble(null));

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), Min(H.ToDouble(null), y.Value));

    public override T LeftSupportEndpoint() => A;

    public override T RightSupportEndpoint() => C;

    public virtual (T X0, T X1)? CoreInterval() => (A, C);
}