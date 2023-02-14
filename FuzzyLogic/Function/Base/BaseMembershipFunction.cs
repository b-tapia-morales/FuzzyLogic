using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Base;

public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    public string Name { get; }
    public double H { get; }

    protected BaseMembershipFunction(string name, double h)
    {
        CheckName(name);
        CheckHeight(h);
        Name = name.Trim();
        H = h;
    }

    public abstract bool IsOpenLeft();

    public abstract bool IsOpenRight();

    public abstract bool IsSymmetric();

    public abstract bool IsSingleton();

    public abstract T SupportLeftEndpoint();

    public abstract T SupportRightEndpoint();

    public abstract T MaxHeightLeftEndpoint();

    public abstract T MaxHeightRightEndpoint();

    public abstract Func<T, double> AsFunction();

    public abstract Func<T, double> HeightFunction<TNumber>(TNumber y) where TNumber : struct, IFuzzyNumber<TNumber>;

    private static void CheckHeight(double h)
    {
        if (h is <= 0 or > 1)
            throw new ArgumentException(
                $"The height “h” of the function must be in the range [0, 1] (Provided value was: {h}");
    }

    private static void CheckName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "The string corresponding to the name of the function cannot be null or contain only whitespaces");
    }
}