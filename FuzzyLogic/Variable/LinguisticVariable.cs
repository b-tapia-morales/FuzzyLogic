using FuzzyLogic.Function.Interface;
using FuzzyLogic.Function.Real;

namespace FuzzyLogic.Variable;

public readonly struct LinguisticVariable : IVariable
{
    public double LowerBound { get; }
    public double UpperBound { get; }
    public string Name { get; }

    public IDictionary<string, IMembershipFunction> SemanticalMappings { get; } =
        new Dictionary<string, IMembershipFunction>(StringComparer.InvariantCultureIgnoreCase);

    private LinguisticVariable(string name, double lowerBoundary = double.MinValue, double upperBoundary = double.MaxValue)
    {
        Name = name;
        LowerBound = double.IsNegativeInfinity(lowerBoundary) ? double.MinValue : lowerBoundary;
        UpperBound = double.IsPositiveInfinity(upperBoundary) ? double.MinValue : upperBoundary;
    }

    public static IVariable Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyVariableException();
        return new LinguisticVariable(name);
    }

    public static IVariable Create(string name, double lowerBound, double upperBound)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyVariableException();
        return new LinguisticVariable(name, lowerBound, upperBound);
    }

    public static IVariable Create(string name, double lowerBound, double upperBound, IDictionary<string, IMembershipFunction> semanticalMappings) =>
        VariableExtensions.Create(name, lowerBound, upperBound, semanticalMappings);

    public static IVariable Create(string name, IDictionary<string, IMembershipFunction> semanticalMappings) =>
        VariableExtensions.Create(name, double.NegativeInfinity, double.PositiveInfinity, semanticalMappings);

    public static IVariable Create(string name, double lowerBound, double upperBound, ICollection<IMembershipFunction> functions) =>
        VariableExtensions.Create(name, lowerBound, upperBound, functions);

    public static IVariable Create(string name, ICollection<IMembershipFunction> functions) =>
        VariableExtensions.Create(name, double.NegativeInfinity, double.PositiveInfinity, functions);

    public static IVariable Create(string name, double lowerBound, double upperBound, params IEnumerable<IMembershipFunction> functions) =>
        VariableExtensions.Create(name, lowerBound, upperBound, functions.ToList());

    public static IVariable Create(string name, params IEnumerable<IMembershipFunction> functions) =>
        VariableExtensions.Create(name, double.NegativeInfinity, double.PositiveInfinity, functions.ToList());

    public IVariable AddTrapezoidFunction(string name, double a, double b, double c, double d, double h = 1) =>
        VariableExtensions.AddTrapezoidalFunction(this, name, a, b, c, d, h);

    public IVariable AddLeftTrapezoidFunction(string name, double a, double b, double h = 1) =>
        VariableExtensions.AddLeftTrapezoidalFunction(this, name, a, b, h);

    public IVariable AddRightTrapezoidFunction(string name, double a, double b, double h = 1) =>
        VariableExtensions.AddRightTrapezoidalFunction(this, name, a, b, h);

    public IVariable AddTriangularFunction(string name, double a, double b, double c, double h = 1) =>
        VariableExtensions.AddTriangularFunction(this, name, a, b, c, h);

    public IVariable AddGaussianFunction(string name, double m, double o, double h = 1) =>
        VariableExtensions.AddGaussianFunction(this, name, m, o, h);

    public IVariable AddCauchyFunction(string name, double a, double b, double c, double h = 1) =>
        VariableExtensions.AddGeneralizedBellFunction(this, name, a, b, c, h);

    public IVariable AddSigmoidFunction(string name, double a, double c, double h = 1) =>
        VariableExtensions.AddSigmoidFunction(this, name, a, c, h);

    public IVariable AddFunction(IMembershipFunction function) =>
        VariableExtensions.AddFunction(this, function);

    public bool ContainsFunction(string term) =>
        SemanticalMappings.ContainsKey(term);

    public IMembershipFunction? RetrieveFunction(string term) =>
        SemanticalMappings.TryGetValue(term, out var function) ? function : null;

    public bool TryGetFunction(string term, out IMembershipFunction? function) => SemanticalMappings.TryGetValue(term, out function);

    public override string ToString() => $"""
                                          Linguistic Variable: {Name}
                                          {string.Join(Environment.NewLine, SemanticalMappings.Values)}
                                          """;
}

file static class VariableExtensions
{
    public static IVariable Create(string name, double lowerBound, double upperBound, IDictionary<string, IMembershipFunction> semanticalMappings)
    {
        ArgumentNullException.ThrowIfNull(semanticalMappings);

        var emptyEntry = semanticalMappings.Keys.FirstOrDefault(string.IsNullOrWhiteSpace);
        if (emptyEntry != null)
            throw new EmptyEntryException();

        var variable = LinguisticVariable.Create(name, lowerBound, upperBound);
        foreach (var entry in semanticalMappings)
            variable.SemanticalMappings.Add(entry.Key, entry.Value);
        return variable;
    }

    public static IVariable Create(string name, double lowerBound, double upperBound, ICollection<IMembershipFunction> functions)
    {
        var grouping = functions.GroupBy(func => func.Name).ToList();

        var emptyEntry = grouping.FirstOrDefault(group => string.IsNullOrWhiteSpace(group.Key));
        if (emptyEntry != null)
            throw new EmptyEntryException();

        var collidingEntry = grouping.FirstOrDefault(group => group.Count() > 1);
        if (collidingEntry != null)
            throw new InvalidOperationException();

        var variable = LinguisticVariable.Create(name, lowerBound, upperBound);
        foreach (var group in grouping)
            variable.SemanticalMappings.Add(group.Key, group.First());
        return variable;
    }

    public static IVariable AddTrapezoidalFunction(this IVariable variable, string name, double a, double b, double c,
        double d, double h = 1) =>
        variable.AddFunction(new TrapezoidalFunction(name, a, b, c, d, h));

    public static IVariable AddLeftTrapezoidalFunction(this IVariable variable, string name, double a, double b, double h = 1) =>
        variable.AddFunction(new LeftTrapezoidalFunction(name, a, b, h));

    public static IVariable AddRightTrapezoidalFunction(this IVariable variable, string name, double a, double b,
        double h = 1) =>
        variable.AddFunction(new LeftTrapezoidalFunction(name, a, b, h));

    public static IVariable AddTriangularFunction(this IVariable variable, string name, double a, double b, double c,
        double h = 1) =>
        variable.AddFunction(new TriangularFunction(name, a, b, c, h));

    public static IVariable AddGaussianFunction(this IVariable variable, string name, double mu, double sigma, double h = 1) =>
        variable.AddFunction(new GaussianFunction(name, mu, sigma, h));

    public static IVariable AddGeneralizedBellFunction(this IVariable variable, string name, double a, double b, double c,
        double h = 1) =>
        variable.AddFunction(new GeneralizedBellFunction(name, a, b, c, h));

    public static IVariable AddSigmoidFunction(this IVariable variable, string name, double a, double c, double h = 1) =>
        variable.AddFunction(new SigmoidFunction(name, a, c, h));

    public static IVariable AddFunction(this IVariable variable, IMembershipFunction function)
    {
        CheckLinguisticEntry(variable, function.Name);
        CheckRange(variable, function);
        variable.SemanticalMappings[function.Name] = function;
        return variable;
    }

    private static void CheckLinguisticEntry(this IVariable variable, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new EmptyEntryException();
        if (variable.ContainsFunction(name))
            throw new DuplicatedEntryException(variable.Name, name);
    }

    private static void CheckRange(this IVariable variable, IMembershipFunction function)
    {
        var (lower, upper) = function is AsymptoteFunction asymptote
            ? asymptote.ApproxSupportInterval()
            : function.SupportInterval();
        if (upper <= variable.LowerBound)
            throw new VariableRangeException(variable.Name,
                (variable.LowerBound, variable.UpperBound), function.Name, (lower, upper), function.GetType());
    }
}