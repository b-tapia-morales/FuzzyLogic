using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;

namespace FuzzyLogic.Function.Base;

public abstract class BaseTriangularFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private bool? _isSymmetric;

    protected BaseTriangularFunction(string name, T a, T b, T c) : base(name)
    {
        A = a;
        B = b;
        C = c;
    }

    protected T A { get; }
    protected T B { get; }
    protected T C { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsNormal() => true;

    public override bool IsSymmetric() => _isSymmetric ??=
        Math.Abs(
            TrigonometricUtils.Distance((A.ToDouble(null), 0), (B.ToDouble(null), 1)) -
            TrigonometricUtils.Distance((B.ToDouble(null), 1), (C.ToDouble(null), 0))
        ) < ITrapezoidalFunction<T>.DistanceTolerance;

    public override Func<T, double> SimpleFunction() => x =>
    {
        if (x > A && x < B) return (x.ToDouble(null) - A.ToDouble(null)) / (B.ToDouble(null) - A.ToDouble(null));
        if (x == B) return 1.0;
        if (x > B && x < C) return (C.ToDouble(null) - x.ToDouble(null)) / (C.ToDouble(null) - B.ToDouble(null));
        return 0.0;
    };

    public override T LowerBoundary() => A;

    public override T UpperBoundary() => C;

    public virtual (T? X0, T? X1) CoreInterval() => (A, C);

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => y == 1
        ? (B.ToDouble(null), B.ToDouble(null))
        : (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        y.Value * (B.ToDouble(null) - A.ToDouble(null)) + A.ToDouble(null);

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        C.ToDouble(null) - y.Value * (C.ToDouble(null) - B.ToDouble(null));
}