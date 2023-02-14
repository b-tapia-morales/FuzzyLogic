using System.Numerics;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseBellShapedFunction<T> : BaseMembershipFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseBellShapedFunction(string name, T a, T b, T c, double h = 1) : base(name, h)
    {
        CheckAValue(a);
        CheckBValue(b);
        A = a;
        B = b;
        C = c;
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }

    public static Func<T, double> AsFunction(double a, double b, double c, double h = 1) => t =>
    {
        var x = t.ToDouble(null);
        return h * (1 / (1 + Pow(Abs((x - c) / a), 2 * b)));
    };

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => true;

    public override bool IsSingleton() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), H);

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), C.ToDouble(null), Min(H, y));

    private static void CheckAValue(T a)
    {
        if (a == T.Zero)
            throw new ArgumentException("The value for «a» cannot be equal to 0");
    }

    private static void CheckBValue(T b)
    {
        if (b < T.One)
            throw new ArgumentException("The value for «b» cannot be lesser than 1");
    }
}