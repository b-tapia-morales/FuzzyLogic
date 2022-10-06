using FuzzyLogic.Condition;
using FuzzyLogic.MembershipFunctions;
using FuzzyLogic.MembershipFunctions.Real;
using static FuzzyLogic.Condition.LiteralToken;

namespace FuzzyLogic.Linguistics;

public class LinguisticVariable : IVariable
{
    private readonly Dictionary<string, IRealFunction> _functions = new(StringComparer.InvariantCultureIgnoreCase);

    public string Name { get; }
    public ICollection<IRealFunction> LinguisticValues { get; } = new List<IRealFunction>();

    public LinguisticVariable(string name) => Name = name;

    public bool ContainsLinguisticValue(string name) => _functions.ContainsKey(name);

    public IRealFunction? RetrieveLinguisticValue(string name) =>
        _functions.TryGetValue(name, out var function) ? function : null;

    public void AddAll(ICollection<IRealFunction> membershipFunctions)
    {
        if (membershipFunctions.Any(e => _functions.ContainsKey(e.Name)))
            throw new InvalidOperationException();

        foreach (var function in membershipFunctions)
        {
            _functions.Add(function.Name, function);
            LinguisticValues.Add(function);
        }
    }

    public void AddTrapezoidFunction(string name, double a, double b, double c, double d)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateTrapezoidalFunction(name, a, b, c, d);
        AddFunction(name, function);
    }

    public void AddTriangularFunction(string name, double a, double b, double c)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateTriangularFunction(name, a, b, c);
        AddFunction(name, function);
    }

    public void AddRectangularFunction(string name, double a, double b)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateRectangularFunction(name, a, b);
        AddFunction(name, function);
    }

    public void AddGaussianFunction(string name, double m, double o)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateGaussianFunction(name, m, o);
        AddFunction(name, function);
    }

    public void AddCauchyFunction(string name, double a, double b, double c)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateCauchyFunction(name, a, b, c);
        AddFunction(name, function);
    }

    public void AddSigmoidFunction(string name, double a, double c)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateSigmoidFunction(name, a, c);
        AddFunction(name, function);
    }

    public ICondition Is(string linguisticValue, HedgeToken token = HedgeToken.None) =>
        Is(this, linguisticValue, token);

    public ICondition IsNot(string linguisticValue, HedgeToken token = HedgeToken.None) =>
        IsNot(this, linguisticValue, token);

    public override string ToString() => Name;

    private void AddFunction(string name, IRealFunction function)
    {
        if (!_functions.TryAdd(name, function))
            throw new InvalidOperationException();

        LinguisticValues.Add(function);
    }

    private static ICondition Is(LinguisticVariable linguisticVariable, string linguisticValue,
        HedgeToken token = HedgeToken.None)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition(linguisticVariable, Affirmation, token, function);
    }

    private static ICondition IsNot(LinguisticVariable linguisticVariable, string linguisticValue,
        HedgeToken token = HedgeToken.None)
    {
        ArgumentNullException.ThrowIfNull(linguisticVariable);
        var function = linguisticVariable.RetrieveLinguisticValue(linguisticValue);
        if (function == null)
        {
            throw new KeyNotFoundException(
                $"{nameof(linguisticVariable)} does not contain a Membership Function associated to the Linguistic Value provided: {linguisticValue}");
        }

        return new FuzzyCondition(linguisticVariable, Negation, token, function);
    }
}