namespace FuzzyLogic.MembershipFunctions;

public interface IMembershipFunction<T> where T : unmanaged, IConvertible
{
    FuzzyNumber MembershipDegree(T t);

    (T t, FuzzyNumber Y) ToPoint(T t) => (t, MembershipDegree(t));
}