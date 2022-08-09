using FuzzyLogic.MembershipFunctions;

namespace FuzzyLogic.Set;

public class LinguisticVariable<T> where T : unmanaged, IConvertible
{
    private static readonly Dictionary<string, IMembershipFunction<T>> Functions = new();

    public string Name { get; }
    public List<IMembershipFunction<T>> LinguisticValues { get; } = new();

    public LinguisticVariable(string name)
    {
        var type = typeof(T);
        if (type != typeof(int) && type != typeof(double))
        {
            throw new InvalidOperationException();
        }

        Name = name;
    }

    public bool ContainsLinguisticValue(string name) => Functions.ContainsKey(name);
    
    public IMembershipFunction<T>? RetrieveLinguisticValue(string name) =>
        Functions.TryGetValue(name, out var function) ? function : null;

    public void AddTrapezoidFunction(string name, T a, T b, T c, T d)
    {
        var trapezoidalFunction = MembershipFunctionFactory.CreateTrapezoidalFunction(name, a, b, c, d);
        if (!Functions.TryAdd(name, trapezoidalFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(trapezoidalFunction);
    }


    public void AddTriangularFunction(string name, T a, T b, T c)
    {
        var triangularFunction = MembershipFunctionFactory.CreateTriangularFunction(name, a, b, c);
        if (!Functions.TryAdd(name, triangularFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(triangularFunction);
    }

    public void AddRectangularFunction(string name, T a, T b)
    {
        var rectangularFunction = MembershipFunctionFactory.CreateRectangularFunction(name, a, b);
        if (!Functions.TryAdd(name, rectangularFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(rectangularFunction);
    }

    public LinguisticVariable<T> Is(string linguisticValue) => Is(this, linguisticValue);

    public LinguisticVariable<T> IsNot(string linguisticValue) => IsNot(this, linguisticValue);

    public static LinguisticVariable<T> Is(LinguisticVariable<T> linguisticVariable, string linguisticValue)
    {
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new InvalidOperationException();
        }

        return null;
    }

    public static LinguisticVariable<T> IsNot(LinguisticVariable<T> linguisticVariable, string linguisticValue)
    {
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new InvalidOperationException();
        }

        return null;
    }
}