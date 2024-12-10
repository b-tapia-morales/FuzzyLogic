using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge.Rule;

public interface IRuleBase
{
    ICollection<IRule> ProductionRules { get; }
    IComparer<IRule> RuleComparer { get; }

    static abstract IRuleBase Create(ComparingMethod method = ComparingMethod.HighestPriority);

    static abstract IRuleBase Create(ComparingMethod method, params IEnumerable<IRule> rules);

    static abstract IRuleBase Create(params IEnumerable<IRule> rules);

    void Add(IRule rule);

    void AddAll(ICollection<IRule> rules);

    void AddAll(params IEnumerable<IRule> rules);

    bool Remove(IRule rule);

    void RemoveAll(params IEnumerable<IRule> rules);

    void RemoveByDependencies(string premiseVariable, string consequentVariable);

    void RemoveCircularDependencies();

    void RemoveFacts(ICollection<string> variables);

    ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts);

    ICollection<IRule> FindRulesWithPremise(string variableName);

    ICollection<IRule> FindRulesWithConclusion(string variableName);

    ICollection<IRule> FilterByResolutionMethod(string variableName);

    ISet<string> FindVariables();

    ISet<string> FindPremiseDependencies(string variableName);

    IDictionary<string, IList<string>> GetDependencyGraph();
}