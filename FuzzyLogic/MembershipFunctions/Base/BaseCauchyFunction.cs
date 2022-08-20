namespace FuzzyLogic.MembershipFunctions.Base;

public class BaseCauchyFunction<T> : BaseMembershipFunction<T>, IMembershipFunction<T> where T : unmanaged, IConvertible
{
    protected BaseCauchyFunction(string name, T a, T b, T c) : base(name)
    {
        CheckValues();
        A = a;
        B = b;
        C = c;
    }

    protected virtual T A { get; }
    protected virtual T B { get; }
    protected virtual T C { get; }

    public override FuzzyNumber MembershipDegree(T x) =>
        1 / (1 + Math.Pow(Math.Abs((x.ToDouble(null) - C.ToDouble(null)) / A.ToDouble(null)), 2 * B.ToDouble(null)));

    // c - a ((1 - α) / α) ^ (1 / 2b)
    public override double? LeftSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) - A.ToDouble(null) * Math.Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));

    // c + a ((1 - α) / α) ^ (1 / 2b)
    public override double? RightSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) + A.ToDouble(null) * Math.Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));

    private void CheckValues()
    {
        if (A.ToDouble(null) <= 0)
            throw new ArgumentException($"The value for A must be greater than zero (Value provided was: {A}).");
    }
}