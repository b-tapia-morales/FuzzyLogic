using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

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

    public T ApproximateLowerBoundary() =>
        _minCrispValue ??= (T) Convert.ChangeType(LambdaCut(IAsymptoteFunction<T>.MinLambdaCut), typeof(T));

    public T ApproximateUpperBoundary() =>
        _maxCrispValue ??= (T) Convert.ChangeType(LambdaCut(IAsymptoteFunction<T>.MaxLambdaCut), typeof(T));

    public override bool IsOpenLeft() => A.ToDouble(null) < 0;

    public override bool IsOpenRight() => A.ToDouble(null) > 0;

    public override bool IsSymmetric() => false;

    bool IMembershipFunction<T>.IsNormal() => false;

    public override Func<T, double> SimpleFunction() =>
        x => 1.0 / (1.0 + Math.Exp(-A.ToDouble(null) * (x.ToDouble(null) - C.ToDouble(null))));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => A.ToDouble(null) < 0
        ? (double.NegativeInfinity, LambdaCut(y))
        : (LambdaCut(y), double.PositiveInfinity);

    private double LambdaCut(FuzzyNumber y) => C.ToDouble(null) + Math.Log(y / (1 - y)) / A.ToDouble(null);
}