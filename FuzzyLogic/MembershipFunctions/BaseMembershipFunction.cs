namespace FuzzyLogic.MembershipFunctions;

public abstract class BaseMembershipFunction<T>: IMembershipFunction<T> where T: unmanaged, IConvertible
{
    public string Name { get; }

    protected BaseMembershipFunction(string name)
    {
        Name = name;
    }

    public abstract FuzzyNumber MembershipDegree(T t);
}