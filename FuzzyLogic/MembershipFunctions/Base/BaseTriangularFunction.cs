using System.Numerics;

namespace FuzzyLogic.MembershipFunctions.Base;

public abstract class BaseTriangularFunction<T> : BaseTrapezoidalFunction<T>
    where T : unmanaged, INumber<T>, IConvertible
{
    protected BaseTriangularFunction(string name, T a, T b, T c) : base(name, a, b, b, c)
    {
    }
}