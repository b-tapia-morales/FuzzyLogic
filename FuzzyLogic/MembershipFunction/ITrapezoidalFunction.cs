namespace FuzzyLogic.MembershipFunction;

public interface ITrapezoidalFunction<T> : IMembershipFunction<T>
{
    T LowerBoundary();

    T UpperBoundary();

    (T X0, T X1) CoreBoundaries();

    ((T X0, T X1) Lower, (T X0, T X1) Upper) SupportBoundaries();
}