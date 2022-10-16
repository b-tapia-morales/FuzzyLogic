using System.Numerics;
using FuzzyLogic.Number;

namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    public string Name { get; }

    protected BaseMembershipFunction(string name)
    {
        Name = name;
    }

    public abstract Func<T, double> SimpleFunction();

    public abstract (double X1, double X2) LambdaCutInterval(FuzzyNumber y);
}