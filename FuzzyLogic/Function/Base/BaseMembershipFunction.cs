using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Base;

public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    public string Name { get; }
    public T H { get; protected init; }

    protected BaseMembershipFunction(string name) => Name = name;

    public abstract bool IsOpenLeft();

    public abstract bool IsOpenRight();

    public abstract bool IsSymmetric();

    public abstract bool IsSingleton();

    public abstract T LeftSupportEndpoint();

    public abstract T RightSupportEndpoint();

    public abstract T MaxHeightLeftEndpoint();

    public abstract T MaxHeightRightEndpoint();

    public abstract Func<T, double> AsFunction();

    public abstract Func<T, double> HeightFunction<TNumber>(TNumber y) where TNumber : struct, IFuzzyNumber<TNumber>;
}