using FuzzyLogic.Condition;
using FuzzyLogic.MembershipFunctions;
using FuzzyLogic.MembershipFunctions.Base;
using static FuzzyLogic.Condition.LiteralToken;

namespace FuzzyLogic.Linguistics;

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

    public void AddGaussianFunction(string name, T m, T o)
    {
        var gaussianFunction = MembershipFunctionFactory.CreateGaussianFunction(name, m, o);
        if (!Functions.TryAdd(name, gaussianFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(gaussianFunction);
    }

    public void AddSigmoidFunction(string name, T a, T c)
    {
        var sigmoidFunction = MembershipFunctionFactory.CreateSigmoidFunction(name, a, c);
        if (!Functions.TryAdd(name, sigmoidFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(sigmoidFunction);
    }

    public override string ToString() => Name;

    public FuzzyCondition<T> Is(string linguisticValue) => Is(this, linguisticValue);

    public FuzzyCondition<T> IsNot(string linguisticValue) => IsNot(this, linguisticValue);

    public static FuzzyCondition<T> Is(LinguisticVariable<T> linguisticVariable, string linguisticValue)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition<T>(Affirmation, linguisticVariable, function);
    }

    public static FuzzyCondition<T> IsNot(LinguisticVariable<T> linguisticVariable, string linguisticValue)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition<T>(Negation, linguisticVariable, function);
    }
}