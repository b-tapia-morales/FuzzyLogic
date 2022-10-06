namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseGaussianFunction<T> : BaseMembershipFunction<T>, IMembershipFunction<T>
    where T : unmanaged, IConvertible
{
    protected BaseGaussianFunction(string name, T m, T o) : base(name)
    {
        M = m;
        O = o;
    }

    protected virtual T M { get; }
    protected virtual T O { get; }

    public T? LowerBoundary() => (M, O) switch
    {
        (int m, int o) => (T) Convert.ChangeType(m - 3 * o, typeof(int)),
        (double m, double o) => (T) Convert.ChangeType(m - 3 * o, typeof(double)),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };

    public T? UpperBoundary() => (M, O) switch
    {
        (int m, int o) => (T) Convert.ChangeType(m + 3 * o, typeof(int)),
        (double m, double o) => (T) Convert.ChangeType(m + 3 * o, typeof(double)),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };

    public override FuzzyNumber MembershipDegree(T x) =>
        Math.Abs(x.ToDouble(null) - M.ToDouble(null)) < FuzzyNumber.Tolerance
            ? 1.0
            : Math.Exp(-0.5 * Math.Pow((x.ToDouble(null) - M.ToDouble(null)) / O.ToDouble(null), 2));

    public override (double? X1, double? X2) LambdaCutInterval(FuzzyNumber y) => (LeftSidedLambdaCut(y), RightSidedLambdaCut(y));

    private double LeftSidedLambdaCut(FuzzyNumber y) =>
        M.ToDouble(null) - O.ToDouble(null) * Math.Sqrt(2 * Math.Log(1 / y.Value));

    private double RightSidedLambdaCut(FuzzyNumber y) =>
        M.ToDouble(null) + O.ToDouble(null) * Math.Sqrt(2 * Math.Log(1 / y.Value));
}