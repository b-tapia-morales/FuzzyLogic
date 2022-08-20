using FuzzyLogic.Condition;
using FuzzyLogic.MembershipFunctions;
using FuzzyLogic.MembershipFunctions.Real;
using static FuzzyLogic.Condition.LiteralToken;

namespace FuzzyLogic.Linguistics;

public class LinguisticVariable : IVariable
{
    private readonly Dictionary<string, IRealFunction> _functions = new(StringComparer.InvariantCultureIgnoreCase);

    public string Name { get; }
    public List<IRealFunction> LinguisticValues { get; } = new();

    public LinguisticVariable(string name) => Name = name;

    public bool ContainsLinguisticValue(string name) => _functions.ContainsKey(name);

    public IRealFunction? RetrieveLinguisticValue(string name) =>
        _functions.TryGetValue(name, out var function) ? function : null;

    public void AddTrapezoidFunction(string name, double a, double b, double c, double d)
    {
        var trapezoidalFunction = (IRealFunction) MembershipFunctionFactory.CreateTrapezoidalFunction(name, a, b, c, d);
        if (!_functions.TryAdd(name, trapezoidalFunction))
            throw new InvalidOperationException();

        LinguisticValues.Add(trapezoidalFunction);
    }

    public void AddTriangularFunction(string name, double a, double b, double c)
    {
        var triangularFunction = (IRealFunction) MembershipFunctionFactory.CreateTriangularFunction(name, a, b, c);
        if (!_functions.TryAdd(name, triangularFunction))
            throw new InvalidOperationException();

        LinguisticValues.Add(triangularFunction);
    }

    public void AddRectangularFunction(string name, double a, double b)
    {
        var rectangularFunction = (IRealFunction) MembershipFunctionFactory.CreateRectangularFunction(name, a, b);
        if (!_functions.TryAdd(name, rectangularFunction))
            throw new InvalidOperationException();

        LinguisticValues.Add(rectangularFunction);
    }

    public void AddGaussianFunction(string name, double m, double o)
    {
        var gaussianFunction = (IRealFunction) MembershipFunctionFactory.CreateGaussianFunction(name, m, o);
        if (!_functions.TryAdd(name, gaussianFunction))
            throw new InvalidOperationException();

        LinguisticValues.Add(gaussianFunction);
    }
    
    public void AddCauchyFunction(string name, double a, double b, double c)
    {
        var cauchyFunction = (IRealFunction) MembershipFunctionFactory.CreateCauchyFunction(name, a, b, c);
        if (!_functions.TryAdd(name, cauchyFunction))
            throw new InvalidOperationException();

        LinguisticValues.Add(cauchyFunction);
    }

    public void AddSigmoidFunction(string name, double a, double c)
    {
        var sigmoidFunction = (IRealFunction) MembershipFunctionFactory.CreateSigmoidFunction(name, a, c);
        if (!_functions.TryAdd(name, sigmoidFunction))
            throw new InvalidOperationException();

        LinguisticValues.Add(sigmoidFunction);
    }

    public ICondition Is(string linguisticValue) => Is(this, linguisticValue);

    public ICondition IsNot(string linguisticValue) => IsNot(this, linguisticValue);

    public override string ToString() => Name;

    public static ICondition Is(LinguisticVariable linguisticVariable, string linguisticValue)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition(linguisticVariable, Affirmation, function);
    }

    public static ICondition IsNot(LinguisticVariable linguisticVariable, string linguisticValue)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition(linguisticVariable, Negation, function);
    }
}