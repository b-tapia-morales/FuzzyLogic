using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Utils;

namespace FuzzyLogic.Function.Base;

public abstract class BaseTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    private bool? _isSymmetric;

    protected BaseTrapezoidalFunction(string name, T a, T b, T c, T d) : base(name)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    protected virtual T A { get; }
    protected virtual T B { get; }
    protected virtual T C { get; }
    protected virtual T D { get; }

    public override bool IsOpenLeft() => false;

    public override bool IsOpenRight() => false;

    public override bool IsNormal() => true;

    public override bool IsSymmetric() => _isSymmetric ??=
        Math.Abs(
            MathUtils.Distance(A.ToDouble(null), B.ToDouble(null), 0, 1) -
            MathUtils.Distance(C.ToDouble(null), D.ToDouble(null), 1, 0)) < ITrapezoidalFunction<T>.DistanceTolerance;

    public override T LowerBoundary() => A;

    public override T UpperBoundary() => D;

    public (T? X0, T? X1) CoreInterval() => (B, C);

    public override Func<T, double> SimpleFunction() => x =>
    {
        if (x > A && x < B) return (x.ToDouble(null) - A.ToDouble(null)) / (B.ToDouble(null) - A.ToDouble(null));
        if (x >= B && x <= C) return 1.0;
        if (x > C && x < D) return (D.ToDouble(null) - x.ToDouble(null)) / (D.ToDouble(null) - C.ToDouble(null));
        return 0.0;
    };

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => y == 1
        ? (B.ToDouble(null), C.ToDouble(null))
        : (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        y.Value * (B.ToDouble(null) - A.ToDouble(null)) + A.ToDouble(null);

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        D.ToDouble(null) - y.Value * (D.ToDouble(null) - C.ToDouble(null));
}