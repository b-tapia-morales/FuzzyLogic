using System.Numerics;
using FuzzyLogic.Function.Interface;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseSigmoidFunction<T> : BaseMembershipFunction<T>, IMembershipFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseSigmoidFunction(string name, T a, T c, double h = 1) : base(name, h)
    {
        CheckAValue(a);
        A = a;
        C = c;
    }

    protected T A { get; }
    protected T C { get; }

    public static Func<T, double> AsFunction(double a, double c, double h = 1) => t =>
    {
        var x = t.ToDouble(null);
        return h * (1 / (1 + Exp(a * (c - x))));
    };

    public override bool IsOpenLeft() => A.ToDouble(null) < 0;

    public override bool IsOpenRight() => A.ToDouble(null) > 0;

    public override bool IsSymmetric() => false;

    public override bool IsSingleton() => false;

    public bool IsNormal() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), C.ToDouble(null), H);

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), C.ToDouble(null), Min(H, y));

    private static void CheckAValue(T a)
    {
        if (a == T.Zero)
            throw new ArgumentException("The value for «a» cannot be equal to 0");
    }
}