using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseCauchyFunction<T> : BaseMembershipFunction<T>, IAsymptoteFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private T? _minCrispValue;
    private T? _maxCrispValue;

    protected BaseCauchyFunction(string name, T a, T b, T c, T h) : base(name)
    {
        A = a;
        B = b;
        C = c;
        H = h;
    }

    protected BaseCauchyFunction(string name, T a, T b, T c) : this(name, a, b, c, T.One)
    {
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }

    public static Func<T, double> AsFunction(double a, double b, double c, double h) => t =>
    {
        var x = t.ToDouble(null);
        return h * (1 / (1 + Pow(Abs((x - c) / a), 2 * b)));
    };

    public static Func<T, double> AsFunction(double a, double b, double c) => AsFunction(a, b, c, 1);

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => true;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), H.ToDouble(null));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) =>
        (LeftSidedAlphaCut(y), RightSidedAlphaCut(y));

    public T ApproximateLowerBoundary() =>
        _minCrispValue ??= (T) Convert.ChangeType(LeftSidedAlphaCut(IAsymptoteFunction<T>.MinLambdaCut), typeof(T));

    public T ApproximateUpperBoundary() =>
        _maxCrispValue ??= (T) Convert.ChangeType(RightSidedAlphaCut(IAsymptoteFunction<T>.MinLambdaCut), typeof(T));

    // c - a ((1 - α) / α) ^ (1 / 2b)
    private double LeftSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) - A.ToDouble(null) * Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));

    // c + a ((1 - α) / α) ^ (1 / 2b)
    private double RightSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) + A.ToDouble(null) * Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));
}