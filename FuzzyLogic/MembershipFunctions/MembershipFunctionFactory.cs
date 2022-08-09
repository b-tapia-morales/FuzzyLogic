using FuzzyLogic.MembershipFunctions.Integer;
using FuzzyLogic.MembershipFunctions.Real;
using ArgumentException = System.ArgumentException;

namespace FuzzyLogic.MembershipFunctions;

public static class MembershipFunctionFactory
{
    public static IMembershipFunction<T> CreateTrapezoidalFunction<T>(string name, T a, T b, T c, T d)
        where T : unmanaged, IConvertible
    {
        return (a, b, c, d) switch
        {
            (int x, int y, int z, int w) =>
                new IntegerTrapezoidalFunction(name, x, y, z, w) as IMembershipFunction<T>,
            (double x, double y, double z, double w) =>
                new RealTrapezoidalFunction(name, x, y, z, w) as IMembershipFunction<T>,
            _ => throw new ArgumentException("Type must be either int or double")
        } ?? throw new InvalidOperationException();
    }

    public static IMembershipFunction<T> CreateTriangularFunction<T>(string name, T a, T b, T c)
        where T : unmanaged, IConvertible
    {
        return (a, b, c) switch
        {
            (int x, int y, int z) =>
                new IntegerTriangularFunction(name, x, y, z) as IMembershipFunction<T>,
            (double x, double y, double z) =>
                new RealTriangularFunction(name, x, y, z) as IMembershipFunction<T>,
            _ => throw new ArgumentException("Type must be either int or double")
        } ?? throw new InvalidOperationException();
    }

    public static IMembershipFunction<T> CreateRectangularFunction<T>(string name, T a, T b)
        where T : unmanaged, IConvertible
    {
        return (a, b) switch
        {
            (int x, int y)
                => new IntegerRectangularFunction(name, x, y) as IMembershipFunction<T>,
            (double x, double y)
                => new RealRectangularFunction(name, x, y) as IMembershipFunction<T>,
            _ => throw new ArgumentException("Type must be either int or double")
        } ?? throw new InvalidOperationException();
    }
}