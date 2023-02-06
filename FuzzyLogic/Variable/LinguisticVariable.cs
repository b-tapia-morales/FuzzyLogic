using FuzzyLogic.Function.Interface;
using FuzzyLogic.Function.Real;

namespace FuzzyLogic.Variable;

public class LinguisticVariable : IVariable
{
    public double LowerBoundary { get; }
    public double UpperBoundary { get; }
    public bool IsClosed { get; }
    public string Name { get; }

    public IDictionary<string, IMembershipFunction<double>> LinguisticEntries { get; } =
        new Dictionary<string, IMembershipFunction<double>>(StringComparer.InvariantCultureIgnoreCase);

    private LinguisticVariable(string name, double lowerBoundary, double upperBoundary)
    {
        Name = name;
        LowerBoundary = double.IsNegativeInfinity(lowerBoundary) ? double.MinValue : lowerBoundary;
        UpperBoundary = double.IsPositiveInfinity(upperBoundary) ? double.MinValue : upperBoundary;
        IsClosed = lowerBoundary > double.MinValue && upperBoundary < double.MaxValue;
    }

    // ReSharper disable once IntroduceOptionalParameters.Local
    private LinguisticVariable(string name) : this(name, double.MinValue, double.MaxValue)
    {
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

    public IVariable AddAll(IDictionary<string, IMembershipFunction<double>> linguisticEntries) =>
        AddAll(this, linguisticEntries);

    public IVariable AddTrapezoidFunction(string name, double a, double b, double c, double d, double h = 1) =>
        AddTrapezoidFunction(this, name, a, b, c, d, h);

    public IVariable AddLeftTrapezoidFunction(string name, double a, double b, double h = 1) =>
        AddLeftTrapezoidFunction(this, name, a, b, h);

    public IVariable AddRightTrapezoidFunction(string name, double a, double b, double h = 1) =>
        AddRightTrapezoidFunction(this, name, a, b, h);

    public IVariable AddTriangularFunction(string name, double a, double b, double c, double h = 1) =>
        AddTriangularFunction(this, name, a, b, c, h);

    public IVariable AddGaussianFunction(string name, double m, double o, double h = 1) =>
        AddGaussianFunction(this, name, m, o, h);

    public IVariable AddCauchyFunction(string name, double a, double b, double c, double h = 1) =>
        AddCauchyFunction(this, name, a, b, c, h);

    public IVariable AddSigmoidFunction(string name, double a, double c, double h = 1) =>
        AddSigmoidFunction(this, name, a, c, h);

    public IVariable AddFunction(string name, IMembershipFunction<double> function) => AddFunction(this, name, function);

    public bool ContainsLinguisticEntry(string name) => LinguisticEntries.ContainsKey(name);

    public IMembershipFunction<double>? RetrieveLinguisticEntry(string name) =>
        LinguisticEntries.TryGetValue(name, out var function) ? function : null;

    private static IVariable AddAll(IVariable variable,
        IDictionary<string, IMembershipFunction<double>> linguisticEntries)
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
        double d, double h = 1) =>
        variable.AddFunction(name, new TrapezoidalFunction(name, a, b, c, d, h));

    private static IVariable
        AddLeftTrapezoidFunction(IVariable variable, string name, double a, double b, double h = 1) =>
        variable.AddFunction(name, new LeftTrapezoidalFunction(name, a, b, h));

    private static IVariable AddRightTrapezoidFunction(IVariable variable, string name, double a, double b,
        double h = 1) =>
        variable.AddFunction(name, new LeftTrapezoidalFunction(name, a, b, h));

    private static IVariable AddTriangularFunction(IVariable variable, string name, double a, double b, double c,
        double h = 1) =>
        variable.AddFunction(name, new TriangularFunction(name, a, b, c, h));

    private static IVariable AddGaussianFunction(IVariable variable, string name, double m, double o, double h = 1) =>
        variable.AddFunction(name, new GaussianFunction(name, m, o, h));

    private static IVariable AddCauchyFunction(IVariable variable, string name, double a, double b, double c,
        double h = 1) =>
        variable.AddFunction(name, new CauchyFunction(name, a, b, c, h));

    private static IVariable AddSigmoidFunction(IVariable variable, string name, double a, double c, double h = 1) =>
        variable.AddFunction(name, new SigmoidFunction(name, a, c, h));

    private static IVariable AddFunction(IVariable variable, string name, IMembershipFunction<double> function)
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

    public static void CheckRange(IVariable variable, IMembershipFunction<double> function)
    {
        var (lower, upper) = function is IAsymptoteFunction<double> asymptote
            ? asymptote.ApproximateBoundaryInterval()
            : function.SupportInterval();
        if (upper <= variable.LowerBoundary)
            throw new VariableRangeException(variable.Name,
                (variable.LowerBoundary, variable.UpperBoundary), function.Name, (lower, upper), function.GetType());
    }

    public override string ToString() => Name;
}