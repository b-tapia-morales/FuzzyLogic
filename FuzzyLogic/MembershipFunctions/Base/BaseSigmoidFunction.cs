namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseSigmoidFunction<T> : BaseMembershipFunction<T> where T : unmanaged, IConvertible
{
    protected BaseSigmoidFunction(string name, T a, T c) : base(name)
    {
        A = a;
        C = c;
    }

    protected virtual T A { get; }
    protected virtual T C { get; }

    public override FuzzyNumber MembershipDegree(T x) =>
        1.0 / (1.0 + Math.Exp(-A.ToDouble(null) * (x.ToDouble(null) - C.ToDouble(null))));

    public override double? LeftSidedAlphaCut(FuzzyNumber y) => (A.ToDouble(null), y.Value) switch
    {
        (_, 0.5) => 0.5,
        (> 0, 0.0) or (< 0, 1.0) => double.NegativeInfinity,
        (> 0, > 0.5) or (< 0, < 0.5) => null,
        _ => AlphaCut(y)
    };

    public override double? RightSidedAlphaCut(FuzzyNumber y) => (A.ToDouble(null), y.Value) switch
    {
        (_, 0.5) => 0.5,
        (> 0, 1.0) or (< 0, 0.0) => double.PositiveInfinity,
        (> 0, < 0.5) or (< 0, > 0.5) => null,
        _ => AlphaCut(y)
    };

    private double AlphaCut(double y) => C.ToDouble(null) + Math.Log(y / (1 - y)) / A.ToDouble(null);
};