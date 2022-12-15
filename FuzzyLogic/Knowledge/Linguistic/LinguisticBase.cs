using FuzzyLogic.Variable;
using static System.StringComparer;

namespace FuzzyLogic.Knowledge.Linguistic;

public class LinguisticBase : ILinguisticBase
{
    public IDictionary<string, IVariable> LinguisticVariables { get; }

    protected LinguisticBase() =>
        LinguisticVariables = new Dictionary<string, IVariable>(InvariantCultureIgnoreCase);

    public static ILinguisticBase Create() => new LinguisticBase();

    public static ILinguisticBase Initialize() => Create();
    
    public ILinguisticBase Add(IVariable variable) =>
        Add(this, variable);

    public ILinguisticBase AddAll(ICollection<IVariable> variables) =>
        AddAll(this, variables);
    
    public ILinguisticBase AddAll(params IVariable[] variables) =>
        AddAll(this, variables);

    public bool Contains(string name) =>
        LinguisticVariables.ContainsKey(name);

    public IVariable? Retrieve(string name) =>
        LinguisticVariables.TryGetValue(name, out var variable) ? variable : null;

    private static ILinguisticBase Add(ILinguisticBase linguisticBase, IVariable variable)
    {
        if (!linguisticBase.LinguisticVariables.TryAdd(variable.Name, variable))
            throw new InvalidOperationException();
        return linguisticBase;
    }

    private static ILinguisticBase AddAll(ILinguisticBase linguisticBase,
        ICollection<IVariable> variables)
    {
        if (variables.Any(e => linguisticBase.Contains(e.Name)))
            throw new InvalidOperationException();

        foreach (var variable in variables)
            linguisticBase.LinguisticVariables.Add(variable.Name, variable);

        return linguisticBase;
    }
}