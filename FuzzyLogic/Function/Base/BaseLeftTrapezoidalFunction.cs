using System.Numerics;
using FuzzyLogic.Function.Interface;

namespace FuzzyLogic.Function.Base;

public abstract class BaseLeftTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseLeftTrapezoidalFunction(string name, T a, T b, T h) : base(name)
    {
        A = a;
        B = b;
        H = h;
    }

    protected BaseLeftTrapezoidalFunction(string name, T a, T b) : this(name, a, b, T.One)
    {
    }

    protected T A { get; }
    protected T B { get; }

    public override bool IsOpenLeft() => true;

    public override bool IsOpenRight() => false;

    public override bool IsSymmetric() => false;

    public override Func<T, double> SimpleFunction() => x =>
    {
        if (x > A && x < B)
            return H.ToDouble(null) * ((B.ToDouble(null) - x.ToDouble(null)) / (B.ToDouble(null) - A.ToDouble(null)));
        if (x <= A)
            return H.ToDouble(null);
        return 0;
    };

    public (T? X0, T? X1) CoreInterval() => (LeftSupportEndpoint(), RightSupportEndpoint());
}