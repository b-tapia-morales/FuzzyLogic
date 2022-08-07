namespace FuzzyLogic.MembershipFunctions;

public interface ITrapezoidalFunction<T> : IMembershipFunction<T> where T : unmanaged, IConvertible
{
    T LowerBoundary();

    T UpperBoundary();

    (T X0, T X1) CoreBoundaries();

    ((T X0, T X1) Lower, (T X0, T X1) Upper) SupportBoundaries();
}