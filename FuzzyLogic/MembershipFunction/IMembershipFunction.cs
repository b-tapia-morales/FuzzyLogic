namespace FuzzyLogic.MembershipFunction;

public interface IMembershipFunction<T>
{
    DataType DataType();

    FuzzyNumber MembershipDegree(T t);

    (T t, FuzzyNumber Y) ToPoint(T t) => (t, MembershipDegree(t));
}