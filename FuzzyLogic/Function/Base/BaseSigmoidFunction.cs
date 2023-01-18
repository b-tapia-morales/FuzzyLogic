using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseSigmoidFunction<T> : BaseMembershipFunction<T>, IAsymptoteFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private T? _minCrispValue;
    private T? _maxCrispValue;

    protected BaseSigmoidFunction(string name, T a, T c, T h) : base(name)
    {
        A = a;
        C = c;
        H = h;
    }

    protected BaseSigmoidFunction(string name, T a, T c) : this(name, a, c, T.One)
    {
    }

    protected T A { get; }
    protected T C { get; }

    public static Func<T, double> AsFunction(double a, double c, double h) => t =>
    {
        var x = t.ToDouble(null);
        return h * (1 / (1 + Exp(a * (c - x))));
    };

    public static Func<T, double> AsFunction(double a, double c) => AsFunction(a, c, 1);

    public T ApproximateLowerBoundary() =>
        _minCrispValue ??= (T) Convert.ChangeType(LambdaCut(IAsymptoteFunction<T>.MinLambdaCut), typeof(T));

    public T ApproximateUpperBoundary() =>
        _maxCrispValue ??= (T) Convert.ChangeType(LambdaCut(IAsymptoteFunction<T>.MaxLambdaCut), typeof(T));

    public override bool IsOpenLeft() => A.ToDouble(null) < 0;

    public override bool IsOpenRight() => A.ToDouble(null) > 0;

    public override bool IsSymmetric() => false;

    bool IMembershipFunction<T>.IsNormal() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), C.ToDouble(null), H.ToDouble(null));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => A.ToDouble(null) < 0
        ? (double.NegativeInfinity, LambdaCut(y))
        : (LambdaCut(y), double.PositiveInfinity);

    private double LambdaCut(FuzzyNumber y) => C.ToDouble(null) + Log(y / (1 - y)) / A.ToDouble(null);
}