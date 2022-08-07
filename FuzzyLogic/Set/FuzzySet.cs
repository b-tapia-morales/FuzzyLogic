using FuzzyLogic.MembershipFunctions;

namespace FuzzyLogic.Set;

public class FuzzySet<T> where T : unmanaged, IConvertible
{
    public string Name { get; }
    public List<IMembershipFunction<T>> MembershipFunctions { get; } = new();

    public FuzzySet(string name) => Name = name;

    public void AddTrapezoidFunction(string name, T a, T b, T c, T d) =>
        MembershipFunctions.Add(MembershipFunctionFactory.CreateTrapezoidalFunction(name, a, b, c, d));

    public void AddTriangularFunction(string name, T a, T b, T c) =>
        MembershipFunctions.Add(MembershipFunctionFactory.CreateTriangularFunction(name, a, b, c));

    public void AddRectangularFunction(string name, T a, T b) =>
        MembershipFunctions.Add(MembershipFunctionFactory.CreateRectangularFunction(name, a, b));
}