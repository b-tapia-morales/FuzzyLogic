using System.Numerics;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseCauchyFunction<T> : BaseMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
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

    public override bool IsSingleton() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), H.ToDouble(null));

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), Min(H.ToDouble(null), y.Value));
}