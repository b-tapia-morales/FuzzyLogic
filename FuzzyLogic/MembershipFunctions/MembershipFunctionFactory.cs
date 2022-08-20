using FuzzyLogic.MembershipFunctions.Base;
using FuzzyLogic.MembershipFunctions.Integer;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.MembershipFunctions;

public static class MembershipFunctionFactory
{
    public static IMembershipFunction<T> CreateTrapezoidalFunction<T>(string name, T a, T b, T c, T d)
        where T : unmanaged, IConvertible => (a, b, c, d) switch
    {
        (int x, int y, int z, int w) => 
            (IMembershipFunction<T>) new IntegerTrapezoidalFunction(name, x, y, z, w),
        (double x, double y, double z, double w) =>
            (IMembershipFunction<T>) new RealTrapezoidalFunction(name, x, y, z, w),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };

    public static IMembershipFunction<T> CreateTriangularFunction<T>(string name, T a, T b, T c)
        where T : unmanaged, IConvertible => (a, b, c) switch
    {
        (int x, int y, int z) => (IMembershipFunction<T>) new IntegerTriangularFunction(name, x, y, z),
        (double x, double y, double z) => (IMembershipFunction<T>) new RealTriangularFunction(name, x, y, z),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };

    public static IMembershipFunction<T> CreateRectangularFunction<T>(string name, T a, T b)
        where T : unmanaged, IConvertible => (a, b) switch
    {
        (int x, int y) => (IMembershipFunction<T>) new IntegerRectangularFunction(name, x, y),
        (double x, double y) => (IMembershipFunction<T>) new RealRectangularFunction(name, x, y),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };

    public static IMembershipFunction<T> CreateGaussianFunction<T>(string name, T m, T o)
        where T : unmanaged, IConvertible => (m, o) switch
    {
        (int x, int y) => (IMembershipFunction<T>) new IntegerGaussianFunction(name, x, y),
        (double x, double y) => (IMembershipFunction<T>) new RealGaussianFunction(name, x, y),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };
    
    public static IMembershipFunction<T> CreateCauchyFunction<T>(string name, T a, T b, T c)
        where T : unmanaged, IConvertible => (a, b, c) switch
    {
        (int x, int y, int z) => (IMembershipFunction<T>) new IntegerCauchyFunction(name, x, y, z),
        (double x, double y, double z) => (IMembershipFunction<T>) new RealCauchyFunction(name, x, y, z),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };

    public static IMembershipFunction<T> CreateSigmoidFunction<T>(string name, T a, T c)
        where T : unmanaged, IConvertible => (a, c) switch
    {
        (int x, int y) => (IMembershipFunction<T>) new IntegerSigmoidFunction(name, x, y),
        (double x, double y) => (IMembershipFunction<T>) new RealSigmoidFunction(name, x, y),
        _ => throw new InvalidOperationException("Type must be either int or double")
    };
}