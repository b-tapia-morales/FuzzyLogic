using FuzzyLogic.MembershipFunctions;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Linguistics;

public class LinguisticVariable : IVariable
{
    public string Name { get; }
    public IDictionary<string, IRealFunction> LinguisticEntries { get; }

    public LinguisticVariable(string name)
    {
        Name = name;
        LinguisticEntries = new Dictionary<string, IRealFunction>(StringComparer.InvariantCultureIgnoreCase);
    }

    public IVariable AddAll(IDictionary<string, IRealFunction> linguisticEntries) => AddAll(this, linguisticEntries);

    public IVariable AddTrapezoidFunction(string name, double a, double b, double c, double d) =>
        AddTrapezoidFunction(this, name, a, b, c, d);

    public IVariable AddTriangularFunction(string name, double a, double b, double c) =>
        AddTriangularFunction(this, name, a, b, c);

    public IVariable AddGaussianFunction(string name, double m, double o) => AddGaussianFunction(this, name, m, o);

    public IVariable AddCauchyFunction(string name, double a, double b, double c) =>
        AddCauchyFunction(this, name, a, b, c);

    public IVariable AddSigmoidFunction(string name, double a, double c) => AddSigmoidFunction(this, name, a, c);

    public IVariable AddFunction(string name, IRealFunction function) => AddFunction(this, name, function);

    public bool ContainsLinguisticEntry(string name) => LinguisticEntries.ContainsKey(name);

    public IRealFunction? RetrieveLinguisticEntry(string name) =>
        LinguisticEntries.TryGetValue(name, out var function) ? function : null;

    public static IVariable Create(string name) => new LinguisticVariable(name);

    private static IVariable AddAll(IVariable variable, IDictionary<string, IRealFunction> linguisticEntries)
    {
        if (linguisticEntries.Keys.Any(variable.ContainsLinguisticEntry))
            throw new InvalidOperationException();

        foreach (var entry in linguisticEntries)
            variable.LinguisticEntries.Add(entry.Key, entry.Value);

        return variable;
    }

    private static IVariable AddTrapezoidFunction(IVariable variable, string name, double a, double b, double c,
        double d)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateTrapezoidalFunction(name, a, b, c, d);
        return variable.AddFunction(name, function);
    }

    private static IVariable AddTriangularFunction(IVariable variable, string name, double a, double b, double c)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateTriangularFunction(name, a, b, c);
        return variable.AddFunction(name, function);
    }

    private static IVariable AddGaussianFunction(IVariable variable, string name, double m, double o)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateGaussianFunction(name, m, o);
        return variable.AddFunction(name, function);
    }

    private static IVariable AddCauchyFunction(IVariable variable, string name, double a, double b, double c)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateCauchyFunction(name, a, b, c);
        return variable.AddFunction(name, function);
    }

    private static IVariable AddSigmoidFunction(IVariable variable, string name, double a, double c)
    {
        var function = (IRealFunction) MembershipFunctionFactory.CreateSigmoidFunction(name, a, c);
        return variable.AddFunction(name, function);
    }

    private static IVariable AddFunction(IVariable variable, string name, IRealFunction function)
    {
        if (!variable.LinguisticEntries.TryAdd(name, function))
            throw new InvalidOperationException();
        return variable;
    }

    public override string ToString() => Name;
}