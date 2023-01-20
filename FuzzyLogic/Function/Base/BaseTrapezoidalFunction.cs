using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;

namespace FuzzyLogic.Function.Base;

public abstract class BaseTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private bool? _isSymmetric;

    protected BaseTrapezoidalFunction(string name, T a, T b, T c, T d, T h) : base(name)
    {
        A = a;
        B = b;
        C = c;
        D = d;
        H = h;
    }

    protected BaseTrapezoidalFunction(string name, T a, T b, T c, T d) : this(name, a, b, c, d, T.One)
    {
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }
    protected T D { get; }

    public static Func<T, double> AsFunction(double a, double b, double c, double d, double h) => t =>
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

    public static Func<T, double> AsFunction(double a, double b, double c, double d) => AsFunction(a, b, c, d, 1);

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => _isSymmetric ??=
        Math.Abs(
            TrigonometricUtils.Distance((A.ToDouble(null), 0), (B.ToDouble(null), H.ToDouble(null))) -
            TrigonometricUtils.Distance((C.ToDouble(null), H.ToDouble(null)), (D.ToDouble(null), 0))
        ) < ITrapezoidalFunction<T>.DistanceTolerance;

    public override T LeftSupportEndpoint() => A;

    public override T RightSupportEndpoint() => D;

    public (T? X0, T? X1) CoreInterval() => (B, C);

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), D.ToDouble(null), H.ToDouble(null));
    
    public override Func<T, double> HeightFunction(FuzzyNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), D.ToDouble(null), y.Value);

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => y == 1
        ? (B.ToDouble(null), C.ToDouble(null))
        : (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        y.Value * (B.ToDouble(null) - A.ToDouble(null)) + A.ToDouble(null);

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        D.ToDouble(null) - y.Value * (D.ToDouble(null) - C.ToDouble(null));
}