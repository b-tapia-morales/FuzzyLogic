using FuzzyLogic.Variable;
using static System.StringComparer;

namespace FuzzyLogic.Knowledge.Linguistic;

public class LinguisticBase : ILinguisticBase
{
    public IDictionary<string, IVariable> LinguisticVariables { get; }

    public LinguisticBase() =>
        LinguisticVariables = new Dictionary<string, IVariable>(InvariantCultureIgnoreCase);

    public ILinguisticBase AddLinguisticVariable(IVariable variable) =>
        AddLinguisticVariable(this, variable);

    public ILinguisticBase AddAllLinguisticVariables(ICollection<IVariable> variables) =>
        AddAllLinguisticVariables(this, variables);

    public bool ContainsLinguisticVariable(string name) =>
        LinguisticVariables.ContainsKey(name);

    public IVariable? RetrieveLinguisticVariable(string name) =>
        LinguisticVariables.TryGetValue(name, out var variable) ? variable : null;

    public static ILinguisticBase Create() => new LinguisticBase();

    public static ILinguisticBase Initialize() => Create();

    private static ILinguisticBase AddLinguisticVariable(ILinguisticBase linguisticBase, IVariable variable)
    {
        if (!linguisticBase.LinguisticVariables.TryAdd(variable.Name, variable))
            throw new InvalidOperationException();
        return linguisticBase;
    }

    private static ILinguisticBase AddAllLinguisticVariables(ILinguisticBase linguisticBase,
        ICollection<IVariable> variables)
    {
        if (variables.Any(e => linguisticBase.ContainsLinguisticVariable(e.Name)))
            throw new InvalidOperationException();

        foreach (var variable in variables)
            linguisticBase.LinguisticVariables.Add(variable.Name, variable);

        return linguisticBase;
    }
}