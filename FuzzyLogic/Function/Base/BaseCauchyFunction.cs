using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Base;

public abstract class BaseCauchyFunction<T> : BaseMembershipFunction<T>, IAsymptoteFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private T? _minCrispValue;
    private T? _maxCrispValue;

    protected BaseCauchyFunction(string name, T a, T b, T c) : base(name)
    {
        A = a;
        B = b;
        C = c;
    }

    protected virtual T A { get; }
    protected virtual T B { get; }
    protected virtual T C { get; }

    public override bool IsOpenLeft() => true;

    public override bool IsOpenRight() => true;

    public override bool IsSymmetric() => true;

    public override bool IsNormal() => true;

    public override Func<T, double> SimpleFunction() => x =>
        1 / (1 + Math.Pow(Math.Abs((x.ToDouble(null) - C.ToDouble(null)) / A.ToDouble(null)), 2 * B.ToDouble(null)));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) =>
        (LeftSidedAlphaCut(y), RightSidedAlphaCut(y));

    public T ApproximateLowerBoundary() =>
        _minCrispValue ??= (T) Convert.ChangeType(LeftSidedAlphaCut(IAsymptoteFunction<T>.MinLambdaCut), typeof(T));

    public T ApproximateUpperBoundary() =>
        _maxCrispValue ??= (T) Convert.ChangeType(RightSidedAlphaCut(IAsymptoteFunction<T>.MinLambdaCut), typeof(T));

    // c - a ((1 - α) / α) ^ (1 / 2b)
    private double LeftSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) - A.ToDouble(null) * Math.Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));

    // c + a ((1 - α) / α) ^ (1 / 2b)
    private double RightSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) + A.ToDouble(null) * Math.Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));
}