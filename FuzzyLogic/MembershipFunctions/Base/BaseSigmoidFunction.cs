using System.Numerics;
using FuzzyLogic.Number;

namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseSigmoidFunction<T> : BaseMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseSigmoidFunction(string name, T a, T c) : base(name)
    {
        A = a;
        C = c;
    }

    protected virtual T A { get; }
    protected virtual T C { get; }

    public override Func<T, double> SimpleFunction() =>
        x => 1.0 / (1.0 + Math.Exp(-A.ToDouble(null) * (x.ToDouble(null) - C.ToDouble(null))));

    public override (double X1, double X2) LambdaCutInterval(FuzzyNumber y) => A.ToDouble(null) < 0
        ? (double.NegativeInfinity, LambdaCut(y))
        : (LambdaCut(y), double.PositiveInfinity);

    private double LambdaCut(FuzzyNumber y) => C.ToDouble(null) + Math.Log(y / (1 - y)) / A.ToDouble(null);
}