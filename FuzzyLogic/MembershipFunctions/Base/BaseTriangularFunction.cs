using System.Numerics;
using FuzzyLogic.Utils;

namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseTriangularFunction<T> : BaseTrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseTriangularFunction(string name, T a, T b, T c) : base(name, a, b, b, c)
    {
    }

    public override bool IsSymmetric() =>
        Math.Abs(MathUtils.Distance(A.ToDouble(null), B.ToDouble(null), 0, 1) -
                 MathUtils.Distance(B.ToDouble(null), C.ToDouble(null), 1, 0)) < DistanceTolerance;

    public override Func<T, double> SimpleFunction() => x =>
    {
        if (x > A && x < B) return (x.ToDouble(null) - A.ToDouble(null)) / (B.ToDouble(null) - A.ToDouble(null));
        if (x == B) return 1.0;
        if (x > B && x < C) return (C.ToDouble(null) - x.ToDouble(null)) / (C.ToDouble(null) - B.ToDouble(null));
        return 0.0;
    };
}