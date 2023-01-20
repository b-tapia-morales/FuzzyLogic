using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseGaussianFunction<T> : BaseMembershipFunction<T>, IAsymptoteFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private T? _minCrispValue;
    private T? _maxCrispValue;

    protected BaseGaussianFunction(string name, T m, T o, T h) : base(name)
    {
        M = m;
        O = o;
        H = h;
    }

    protected BaseGaussianFunction(string name, T m, T o) : this(name, m, o, T.One)
    {
    }

    protected T M { get; }
    protected T O { get; }

    public static Func<T, double> AsFunction(double m, double o, double h) => t =>
    {
        var x = t.ToDouble(null);
        return h * Exp(-(1 / 2.0) * Pow((x - m) / o, 2));
    };

    public static Func<T, double> AsFunction(double m, double o) => AsFunction(m, o, 1);

    public override bool IsOpenLeft() => true;

    public override bool IsOpenRight() => true;

    public override bool IsSymmetric() => true;

    public override Func<T, double> AsFunction() =>
        AsFunction(M.ToDouble(null), O.ToDouble(null), H.ToDouble(null));

    public override Func<T, double> HeightFunction(FuzzyNumber y) =>
        AsFunction(M.ToDouble(null), O.ToDouble(null), y.Value);

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) =>
        (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    public T ApproximateLowerBoundary() =>
        _minCrispValue ??= (T) Convert.ChangeType(M.ToDouble(null) - 3 * O.ToDouble(null), typeof(T));

    public T ApproximateUpperBoundary() =>
        _maxCrispValue ??= (T) Convert.ChangeType(M.ToDouble(null) + 3 * O.ToDouble(null), typeof(T));

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        M.ToDouble(null) - O.ToDouble(null) * Sqrt(2 * Log(1 / y.Value));

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        M.ToDouble(null) + O.ToDouble(null) * Sqrt(2 * Log(1 / y.Value));
}