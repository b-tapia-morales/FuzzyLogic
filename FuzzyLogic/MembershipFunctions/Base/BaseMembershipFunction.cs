namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseMembershipFunction<T> : IMembershipFunction<T> where T : unmanaged, IConvertible
{
    public string Name { get; }

    protected BaseMembershipFunction(string name)
    {
        Name = name;
    }

    public abstract FuzzyNumber MembershipDegree(T x);

    public abstract (double? X1, double? X2) LambdaCutInterval(FuzzyNumber y);
}