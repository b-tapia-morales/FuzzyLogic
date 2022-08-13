using FuzzyLogic.Condition;
using FuzzyLogic.MembershipFunctions;
using FuzzyLogic.MembershipFunctions.Real;
using static FuzzyLogic.Condition.LiteralToken;

namespace FuzzyLogic.Linguistics;

public class LinguisticVariable
{
    private static readonly Dictionary<string, IRealFunction> Functions = new();

    public string Name { get; }
    public List<IRealFunction> LinguisticValues { get; } = new();

    public LinguisticVariable(string name)
    {
        Name = name;
    }

    public bool ContainsLinguisticValue(string name) => Functions.ContainsKey(name);

    public IRealFunction? RetrieveLinguisticValue(string name) =>
        Functions.TryGetValue(name, out var function) ? function : null;

    public void AddTrapezoidFunction(string name, double a, double b, double c, double d)
    {
        var trapezoidalFunction = (IRealFunction) MembershipFunctionFactory.CreateTrapezoidalFunction(name, a, b, c, d);
        if (!Functions.TryAdd(name, trapezoidalFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(trapezoidalFunction);
    }


    public void AddTriangularFunction(string name, double a, double b, double c)
    {
        var triangularFunction = (IRealFunction) MembershipFunctionFactory.CreateTriangularFunction(name, a, b, c);
        if (!Functions.TryAdd(name, triangularFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(triangularFunction);
    }

    public void AddRectangularFunction(string name, double a, double b)
    {
        var rectangularFunction = (IRealFunction) MembershipFunctionFactory.CreateRectangularFunction(name, a, b);
        if (!Functions.TryAdd(name, rectangularFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(rectangularFunction);
    }

    public void AddGaussianFunction(string name, double m, double o)
    {
        var gaussianFunction = (IRealFunction) MembershipFunctionFactory.CreateGaussianFunction(name, m, o);
        if (!Functions.TryAdd(name, gaussianFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(gaussianFunction);
    }

    public void AddSigmoidFunction(string name, double a, double c)
    {
        var sigmoidFunction = (IRealFunction) MembershipFunctionFactory.CreateSigmoidFunction(name, a, c);
        if (!Functions.TryAdd(name, sigmoidFunction))
        {
            throw new InvalidOperationException();
        }

        LinguisticValues.Add(sigmoidFunction);
    }

    public override string ToString() => Name;

    public FuzzyCondition Is(string linguisticValue) => Is(this, linguisticValue);

    public FuzzyCondition IsNot(string linguisticValue) => IsNot(this, linguisticValue);

    public static FuzzyCondition Is(LinguisticVariable linguisticVariable, string linguisticValue)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition(Affirmation, linguisticVariable, function);
    }

    public static FuzzyCondition IsNot(LinguisticVariable linguisticVariable, string linguisticValue)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition(Negation, linguisticVariable, function);
    }
}