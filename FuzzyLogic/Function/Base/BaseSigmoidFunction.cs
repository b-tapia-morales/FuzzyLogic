using System.Numerics;
using FuzzyLogic.Function.Interface;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseSigmoidFunction<T> : BaseMembershipFunction<T>, IMembershipFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
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

    public override bool IsOpenLeft() => A.ToDouble(null) < 0;

    public override bool IsOpenRight() => A.ToDouble(null) > 0;

    public override bool IsSymmetric() => false;
    
    public override bool IsSingleton() => false;

    public bool IsNormal() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), C.ToDouble(null), H.ToDouble(null));

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), C.ToDouble(null), Min(H.ToDouble(null), y.Value));
}