using System.Numerics;
using FuzzyLogic.Number;

namespace FuzzyLogic.MembershipFunctions.Base;

public class BaseCauchyFunction<T> : BaseMembershipFunction<T>, IMembershipFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseCauchyFunction(string name, T a, T b, T c) : base(name)
    {
        A = a;
        B = b;
        C = c;
    }

    protected virtual T A { get; }
    protected virtual T B { get; }
    protected virtual T C { get; }

    public override Func<T, double> SimpleFunction() => x =>
        1 / (1 + Math.Pow(Math.Abs((x.ToDouble(null) - C.ToDouble(null)) / A.ToDouble(null)), 2 * B.ToDouble(null)));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) =>
        (LeftSidedAlphaCut(y), RightSidedAlphaCut(y));

    // c - a ((1 - α) / α) ^ (1 / 2b)
    private double LeftSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) - A.ToDouble(null) * Math.Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));

    // c + a ((1 - α) / α) ^ (1 / 2b)
    private double RightSidedAlphaCut(FuzzyNumber y) =>
        C.ToDouble(null) + A.ToDouble(null) * Math.Pow((1 - y.Value) / y.Value, 1 / (2 * B.ToDouble(null)));
}