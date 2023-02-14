using System.Numerics;
using FuzzyLogic.Function.Interface;
using static System.Math;

namespace FuzzyLogic.Function.Base;

public abstract class BaseRightTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseRightTrapezoidalFunction(string name, T a, T b, double h = 1) : base(name, h)
    {
        CheckValues(a, b);
        A = a;
        B = b;
    }

    protected T A { get; }
    protected T B { get; }

    public static Func<T, double> AsFunction(double a, double b, double h = 1) => t =>
    {
        var x = t.ToDouble(null);
        if (x > a && x < b)
            return h * ((x - a) / (b - a));
        if (x >= b)
            return h;
        return 0;
    };

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => false;
    
    public override bool IsSingleton() => false;

    public override Func<T, double> AsFunction() =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), H);

    public override Func<T, double> HeightFunction<TNumber>(TNumber y) =>
        AsFunction(A.ToDouble(null), B.ToDouble(null), Min(H, y));

    public abstract (T X0, T X1)? CoreInterval();

    private static void CheckValues(T a, T b)
    {
        if (a >= b)
            throw new ArgumentException(
                $"The following condition has been violated: a < b (Values provides were: {a}, {b})");
    }
}