using System.Numerics;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Base;

public abstract class BaseRightTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseRightTrapezoidalFunction(string name, T a, T b) : base(name)
    {
        A = a;
        B = b;
    }

    protected virtual T A { get; }
    protected virtual T B { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsNormal() => true;

    public override bool IsSymmetric() => false;

    public override Func<T, double> SimpleFunction() => x =>
    {
        if (x > A && x < B) return (x.ToDouble(null) - A.ToDouble(null)) / (B.ToDouble(null) - A.ToDouble(null));
        if (x >= B) return 1.0;
        return 0.0;
    };

    public abstract (T? X0, T? X1) CoreInterval();
}