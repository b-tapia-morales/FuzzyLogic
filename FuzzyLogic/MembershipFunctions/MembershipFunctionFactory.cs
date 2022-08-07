using FuzzyLogic.MembershipFunctions.Integer;
using FuzzyLogic.MembershipFunctions.Real;
using ArgumentException = System.ArgumentException;

namespace FuzzyLogic.MembershipFunctions;

public static class MembershipFunctionFactory
{
    public static IMembershipFunction<T> CreateTrapezoidalFunction<T>(string name, T a, T b, T c, T d)
        where T : unmanaged, IConvertible
    {
        return Type.GetTypeCode(a.GetType()) switch
        {
            TypeCode.Int32 => new IntegerTrapezoidalFunction(name, a.ToInt32(null), b.ToInt32(null),
                c.ToInt32(null), d.ToInt32(null)) as IMembershipFunction<T>,
            TypeCode.Double => new RealTrapezoidalFunction(name, a.ToDouble(null), b.ToDouble(null),
                c.ToDouble(null), d.ToDouble(null)) as IMembershipFunction<T>,
            _ => throw new ArgumentException("Type must be either int or double")
        } ?? throw new InvalidOperationException();
    }

    public static IMembershipFunction<T> CreateTriangularFunction<T>(string name, T a, T b, T c)
        where T : unmanaged, IConvertible
    {
        return Type.GetTypeCode(a.GetType()) switch
        {
            TypeCode.Int32 => new IntegerTriangularFunction(name, a.ToInt32(null), b.ToInt32(null),
                c.ToInt32(null)) as IMembershipFunction<T>,
            TypeCode.Double => new RealTriangularFunction(name, a.ToDouble(null), b.ToDouble(null),
                c.ToDouble(null)) as IMembershipFunction<T>,
            _ => throw new ArgumentException("Type must be either int or double")
        } ?? throw new InvalidOperationException();
    }

    public static IMembershipFunction<T> CreateRectangularFunction<T>(string name, T a, T b)
        where T : unmanaged, IConvertible
    {
        return Type.GetTypeCode(a.GetType()) switch
        {
            TypeCode.Int32 => new IntegerRectangularFunction(name, a.ToInt32(null), b.ToInt32(null)) as
                IMembershipFunction<T>,
            TypeCode.Double => new RealRectangularFunction(name, a.ToDouble(null), b.ToDouble(null)) as
                IMembershipFunction<T>,
            _ => throw new ArgumentException("Type must be either int or double")
        } ?? throw new InvalidOperationException();
    }
}

public enum DataType
{
    Integer = 1,
    Double = 2
}