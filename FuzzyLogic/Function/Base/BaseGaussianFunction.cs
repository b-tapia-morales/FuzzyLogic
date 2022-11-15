using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Base;

public abstract class BaseGaussianFunction<T> : BaseMembershipFunction<T>, IAsymptoteFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private T? _minCrispValue;
    private T? _maxCrispValue;

    protected BaseGaussianFunction(string name, T m, T o) : base(name)
    {
        M = m;
        O = o;
    }

    protected virtual T M { get; }
    protected virtual T O { get; }

    public override bool IsOpenLeft() => true;

    public override bool IsOpenRight() => true;

    public override bool IsSymmetric() => true;

    public override bool IsNormal() => true;

    public override Func<T, double> SimpleFunction() =>
        x => Math.Exp(-0.5 * Math.Pow((x.ToDouble(null) - M.ToDouble(null)) / O.ToDouble(null), 2));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) =>
        (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    public T ApproximateLowerBoundary() =>
        _minCrispValue ??= (T) Convert.ChangeType(M.ToDouble(null) - 3 * O.ToDouble(null), typeof(T));

    public T ApproximateUpperBoundary() =>
        _maxCrispValue ??= (T) Convert.ChangeType(M.ToDouble(null) + 3 * O.ToDouble(null), typeof(T));

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        M.ToDouble(null) - O.ToDouble(null) * Math.Sqrt(2 * Math.Log(1 / y.Value));

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        M.ToDouble(null) + O.ToDouble(null) * Math.Sqrt(2 * Math.Log(1 / y.Value));
}