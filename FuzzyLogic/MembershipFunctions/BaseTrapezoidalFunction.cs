namespace FuzzyLogic.MembershipFunctions;

public abstract class BaseTrapezoidalFunction<T> : BaseMembershipFunction<T>, ITrapezoidalFunction<T> where T : unmanaged, IConvertible
{
    protected BaseTrapezoidalFunction(string name, T a, T b, T c, T d) : base(name)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public T A { get; }
    public T B { get; }
    public T C { get; }
    public T D { get; }

    public T LowerBoundary() => A;

    public T UpperBoundary() => D;

    public (T X0, T X1) CoreBoundaries() => (B, C);

    public ((T X0, T X1) Lower, (T X0, T X1) Upper) SupportBoundaries() => ((A, B), (C, D));
}