namespace FuzzyLogic.MembershipFunctions;

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
}