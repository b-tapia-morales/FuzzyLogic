using FuzzyLogic.Function.Interface;
using FuzzyLogic.Function.Real;

namespace FuzzyLogic.Variable;

public class LinguisticVariable : IVariable
{
    public double LowerBoundary { get; }
    public double UpperBoundary { get; }
    public bool HasClosedInterval { get; }
    public string Name { get; }
    public IDictionary<string, IRealFunction> LinguisticEntries { get; }

    private LinguisticVariable(string name)
    {
        LowerBoundary = double.MinValue;
        UpperBoundary = double.MaxValue;
        HasClosedInterval = false;
        Name = name;
        LinguisticEntries = new Dictionary<string, IRealFunction>(StringComparer.InvariantCultureIgnoreCase);
    }

    private LinguisticVariable(string name, double lowerBoundary, double upperBoundary)
    {
        LowerBoundary = lowerBoundary;
        UpperBoundary = upperBoundary;
        HasClosedInterval = true;
        Name = name;
        LinguisticEntries = new Dictionary<string, IRealFunction>(StringComparer.InvariantCultureIgnoreCase);
    }

    public static IVariable Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyVariableException();
        return new LinguisticVariable(name);
    }

    public static IVariable Create(string name, double lowerBoundary, double upperBoundary)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyVariableException();
        return new LinguisticVariable(name, lowerBoundary, upperBoundary);
    }

    public IVariable AddAll(IDictionary<string, IRealFunction> linguisticEntries) => AddAll(this, linguisticEntries);

    public IVariable AddTrapezoidFunction(string name, double a, double b, double c, double d) =>
        AddTrapezoidFunction(this, name, a, b, c, d);

    public IVariable AddLeftTrapezoidFunction(string name, double a, double b) =>
        AddLeftTrapezoidFunction(this, name, a, b);

    public IVariable AddRightTrapezoidFunction(string name, double a, double b) =>
        AddRightTrapezoidFunction(this, name, a, b);

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

    private static IVariable AddAll(IVariable variable, IDictionary<string, IRealFunction> linguisticEntries)
    {
        if (linguisticEntries == null)
            throw new ArgumentNullException(nameof(linguisticEntries));
        if (linguisticEntries.Keys.Any(string.IsNullOrWhiteSpace))
            throw new EmptyEntryException();
        var duplicatedEntry = linguisticEntries.Keys.FirstOrDefault(variable.ContainsLinguisticEntry);
        if (duplicatedEntry != null)
            throw new DuplicatedEntryException(variable.Name, duplicatedEntry);

        foreach (var entry in linguisticEntries)
            variable.LinguisticEntries.Add(entry.Key, entry.Value);

        return variable;
    }

    private static IVariable AddTrapezoidFunction(IVariable variable, string name, double a, double b, double c,
        double d) =>
        variable.AddFunction(name, new TrapezoidalFunction(name, a, b, c, d));

    private static IVariable AddLeftTrapezoidFunction(IVariable variable, string name, double a, double b) =>
        variable.AddFunction(name, new LeftTrapezoidalFunction(name, a, b));

    private static IVariable AddRightTrapezoidFunction(IVariable variable, string name, double a, double b) =>
        variable.AddFunction(name, new LeftTrapezoidalFunction(name, a, b));

    private static IVariable AddTriangularFunction(IVariable variable, string name, double a, double b, double c) =>
        variable.AddFunction(name, new TriangularFunction(name, a, b, c));

    private static IVariable AddGaussianFunction(IVariable variable, string name, double m, double o) =>
        variable.AddFunction(name, new GaussianFunction(name, m, o));

    private static IVariable AddCauchyFunction(IVariable variable, string name, double a, double b, double c) =>
        variable.AddFunction(name, new CauchyFunction(name, a, b, c));

    private static IVariable AddSigmoidFunction(IVariable variable, string name, double a, double c) =>
        variable.AddFunction(name, new SigmoidFunction(name, a, c));

    private static IVariable AddFunction(IVariable variable, string name, IRealFunction function)
    {
        CheckLinguisticEntry(variable, name);
        CheckRange(variable, function);
        variable.LinguisticEntries[name] = function;
        return variable;
    }

    private static void CheckLinguisticEntry(IVariable variable, string name)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new EmptyEntryException();
        if (variable.ContainsLinguisticEntry(name)) 
            throw new DuplicatedEntryException(variable.Name, name);
    }

    public static void CheckRange(IVariable variable, IRealFunction function)
    {
        var (lower, upper) = function is IAsymptoteFunction<double> asymptote
            ? asymptote.ApproximateBoundaryInterval()
            : function.BoundaryInterval();
        if (upper <= variable.LowerBoundary)
            throw new VariableRangeException(variable.Name,
                (variable.LowerBoundary, variable.UpperBoundary), function.Name, (lower, upper), function.GetType());
    }

    public override string ToString() => Name;
}