using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Base;

public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    public string Name { get; }
    public T H { get; protected init; }

    protected BaseMembershipFunction(string name)
    {
        Name = name;
    }

    public abstract bool IsOpenLeft();

    public abstract bool IsOpenRight();

    public abstract bool IsSymmetric();

    public abstract T LeftSupportEndpoint();

    public abstract T RightSupportEndpoint();

    public abstract Func<T, double> AsFunction();

    public abstract Func<T, double> HeightFunction(FuzzyNumber y);

    public abstract (double X1, double X2) LambdaCutInterval(FuzzyNumber y);
}