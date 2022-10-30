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

    public abstract bool IsOpenLeft();

    public abstract bool IsOpenRight();

    public abstract bool IsSymmetric();

    public abstract bool IsNormal();
    
    public abstract T LowerBoundary();

    public abstract T UpperBoundary();

    public abstract Func<T, double> SimpleFunction();

    public abstract (double X1, double X2) LambdaCutInterval(FuzzyNumber y);
}