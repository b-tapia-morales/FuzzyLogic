using System.Collections.Immutable;
using FuzzyLogic.Rule;
using FuzzyLogic.Utils;
using static System.StringComparison;

namespace FuzzyLogic.Knowledge.Rule;

public class RuleBase : IRuleBase
{
    private RuleBase(ComparingMethod method = ComparingMethod.HighestPriority) =>
        RuleComparer = RuleComparerFactory.CreateInstance(method);

    private RuleBase(ComparingMethod method, params IEnumerable<IRule> rules)
    {
        RuleComparer = RuleComparerFactory.CreateInstance(method);
        foreach (var rule in rules)
            ProductionRules.Add(rule);
    }

    public ICollection<IRule> ProductionRules { get; } = new List<IRule>();
    public IComparer<IRule> RuleComparer { get; }

    public static IRuleBase Create(ComparingMethod method = ComparingMethod.HighestPriority) =>
        new RuleBase(method);

    public static IRuleBase Create(ComparingMethod method, params IEnumerable<IRule> rules) =>
        new RuleBase(method, rules);

    public static IRuleBase Create(params IEnumerable<IRule> rules) =>
        new RuleBase(ComparingMethod.HighestPriority, rules);

    public void Add(IRule rule)
    {
        if (!rule.IsValid())
            throw new InvalidRuleException();
        ProductionRules.Add(rule);
    }

    public void AddAll(ICollection<IRule> rules)
    {
        if (rules.Any(e => !e.IsValid()))
            throw new InvalidRuleException();
        foreach (var rule in rules)
            ProductionRules.Add(rule);
    }

    public void AddAll(params IEnumerable<IRule> rules) => AddAll(rules.ToList());

    public bool Remove(IRule rule) => ProductionRules.Remove(rule);

    public void RemoveAll(params IEnumerable<IRule> rules)
    {
        foreach (var rule in rules)
            ProductionRules.Remove(rule);
    }

    public void RemoveByDependencies(string premiseVariable, string consequentVariable)
    {
        var rules = ProductionRules.Where(rule => rule.PremiseContains(premiseVariable) && rule.ConsequentContains(consequentVariable));
        RemoveAll(rules);
    }

    public void RemoveCircularDependencies()
    {
        var adjacencyList = GetDependencyGraph();
        var backEdges = GraphUtils.FindBackEdges(adjacencyList);
        foreach (var (consequentVariable, premiseVariable) in backEdges)
            RemoveByDependencies(premiseVariable, consequentVariable);
    }

    public void RemoveFacts(ICollection<string> variables)
    {
        foreach (var variable in variables)
        {
            var rules = ProductionRules.Where(rule => rule.ConsequentContains(variable));
            RemoveAll(rules);
        }
    }

    public ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts) =>
        ProductionRules.Where(e => e.IsApplicable(facts)).ToList();

    public ICollection<IRule> FindRulesWithPremise(string variableName) =>
        ProductionRules.Where(e => e.PremiseContains(variableName)).ToList();

    public ICollection<IRule> FindRulesWithConclusion(string variableName) =>
        ProductionRules.Where(e => e.ConsequentContains(variableName)).ToList();

    public ICollection<IRule> FilterByResolutionMethod(string variableName) =>
        FilterByResolutionMethod(ProductionRules, variableName, RuleComparer);

    public ISet<string> FindVariables()
    {
        var antecedents = ProductionRules.Select(rule => rule.Conditional!.VariableName);
        var consequents = ProductionRules.Select(rule => rule.Consequent!.VariableName);
        var connectives = ProductionRules.Where(e => e.Connectives.Count > 0).SelectMany(e => e.Connectives).Select(rule => rule.VariableName);
        return new HashSet<string>(antecedents.Union(connectives).Union(consequents));
    }

    public ISet<string> FindPremiseDependencies(string variableName)
    {
        var rules = FindRulesWithConclusion(variableName);
        if (rules.Count == 0)
            return new HashSet<string>();
        var antecedents = rules.Select(rule => rule.Conditional!.VariableName);
        var connectives = rules.Where(e => e.Connectives.Count > 0).SelectMany(e => e.Connectives).Select(rule => rule.VariableName);
        return new HashSet<string>(antecedents.Union(connectives));
    }

    public IDictionary<string, IList<string>> GetDependencyGraph() =>
        FindVariables().ToDictionary(variable => variable, IList<string> (variable) => FindPremiseDependencies(variable).ToList());

    public static ICollection<IRule> FilterByResolutionMethod(IEnumerable<IRule> rules, string variableName,
        IComparer<IRule> ruleComparer)
    {
        return rules
            .Where(rule => string.Equals(rule.Consequent!.VariableName, variableName, InvariantCultureIgnoreCase))
            .GroupBy(rule => rule.Consequent!.Function.Name)
            .Select(tuple => (Function: tuple.Key, Rule: tuple.MaxBy(g => g, ruleComparer)!))
            .Select(tuple => tuple.Rule)
            .ToList();
    }
}