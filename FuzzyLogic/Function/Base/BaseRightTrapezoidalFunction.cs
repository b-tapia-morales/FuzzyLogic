using System.Numerics;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Base;

public abstract class BaseRightTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseRightTrapezoidalFunction(string name, T a, T b, T h) : base(name)
    {
        A = a;
        B = b;
        H = h;
    }

    protected BaseRightTrapezoidalFunction(string name, T a, T b) : this(name, a, b, T.One)
    {
    }

    protected T A { get; }
    protected T B { get; }
    protected T H { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => false;

    public override Func<T, double> SimpleFunction() => x =>
    {
        if (x > A && x < B) 
            return H.ToDouble(null) * ((x.ToDouble(null) - A.ToDouble(null)) / (B.ToDouble(null) - A.ToDouble(null)));
        if (x >= B) 
            return H.ToDouble(null);
        return 0;
    };

    public abstract (T? X0, T? X1) CoreInterval();
}