namespace FuzzyLogic.MembershipFunction;

public interface IMembershipFunction<T>
{
    FuzzyNumber MembershipDegree(T t);

    (T t, FuzzyNumber Y) ToPoint(T t) => (t, MembershipDegree(t));
}