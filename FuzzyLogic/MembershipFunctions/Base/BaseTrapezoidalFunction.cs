namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T>
    where T : unmanaged, IConvertible
{
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

    public override (double? X1, double? X2) LambdaCutInterval(FuzzyNumber y) => y == 1
        ? (B.ToDouble(null), C.ToDouble(null))
        : (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    public virtual T? LowerBoundary() => A;

    public virtual T? UpperBoundary() => D;

    public virtual (T? X0, T? X1) CoreInterval() => (B, C);

    public virtual (T? X0, T? X1) LeftSupportInterval() => (LowerBoundary(), CoreInterval().X0);

    public virtual (T? X0, T? X1) RightSupportInterval() => (CoreInterval().X1, UpperBoundary());

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        y.Value * (B.ToDouble(null) - A.ToDouble(null)) + A.ToDouble(null);

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        D.ToDouble(null) - y.Value * (D.ToDouble(null) - C.ToDouble(null));
}