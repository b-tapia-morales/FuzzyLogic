using FuzzyLogic.Variable;
using static System.StringComparer;

namespace FuzzyLogic.Knowledge.Linguistic;

public class LinguisticBase : ILinguisticBase
{
    public IDictionary<string, IVariable> LinguisticVariables { get; } =
        new Dictionary<string, IVariable>(InvariantCultureIgnoreCase);

    public static ILinguisticBase Create() => new LinguisticBase();

    public static ILinguisticBase Create(params IEnumerable<IVariable> variables) => Create(variables.ToList());

    public static ILinguisticBase Create(ICollection<IVariable> variables)
    {
        if (new HashSet<string>(variables.Select(e => e.Name)).Count != variables.Count)
            throw new InvalidOperationException();

        var @base = new LinguisticBase();
        @base.AddAll(variables);
        return @base;
    }

    public static ILinguisticBase Initialize() => Create();

    public bool Contains(string name) =>
        LinguisticVariables.ContainsKey(name);

    public IVariable? Retrieve(string name) =>
        LinguisticVariables.TryGetValue(name, out var variable) ? variable : null;

    public void Add(IVariable variable)
    {
        if (!LinguisticVariables.TryAdd(variable.Name, variable))
            throw new InvalidOperationException();
    }

    public void AddAll(params IEnumerable<IVariable> variables) => AddAll(variables.ToList());

    public void AddAll(ICollection<IVariable> variables)
    {
        if (variables.Any(e => Contains(e.Name)))
            throw new InvalidOperationException();

        foreach (var variable in variables)
            LinguisticVariables.Add(variable.Name, variable);
    }
}

file static class LinguisticBaseExt
{
    public static ILinguisticBase AddAll(this ILinguisticBase @base,
        ICollection<IVariable> variables)
    {
        if (variables.Any(e => @base.Contains(e.Name)))
            throw new InvalidOperationException();

        foreach (var variable in variables)
            @base.LinguisticVariables.Add(variable.Name, variable);

        return @base;
    }

    public static ILinguisticBase AddAll(this ILinguisticBase @base, params IVariable[] variables) => AddAll(@base, variables.ToList());
}