using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Rule;
using static System.StringComparison;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Knowledge.Rule;

public class RuleBase : IRuleBase
{
    protected RuleBase(ComparingMethod method = ComparingMethod.HighestPriority) =>
        RuleComparer = RuleComparerFactory.CreateInstance(method);

    public ICollection<IRule> ProductionRules { get; } = new List<IRule>();
    public IComparer<IRule> RuleComparer { get; }

    public static IRuleBase Create(ComparingMethod method = ComparingMethod.HighestPriority) =>
        new RuleBase(method);

    public static IRuleBase Initialize(ILinguisticBase linguisticBase, ComparingMethod method = ComparingMethod.HighestPriority) =>
        Create(method);

    public IRuleBase Add(IRule rule) => Add(this, rule);

    public IRuleBase AddAll(ICollection<IRule> rules) => AddAll(this, rules);

    public IRuleBase AddAll(params IRule[] rules) => AddAll(this, rules);

    public ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts) =>
        ProductionRules.Where(e => e.IsApplicable(facts)).ToList();

    public ICollection<IRule> FindRulesWithPremise(string variableName) =>
        ProductionRules.Where(e => e.PremiseContainsVariable(variableName)).ToList();

    public ICollection<IRule> FindRulesWithConclusion(string variableName) =>
        ProductionRules.Where(e => e.ConclusionContainsVariable(variableName)).ToList();

    public ICollection<IRule> FilterByResolutionMethod(string variableName) =>
        FilterByResolutionMethod(variableName, ProductionRules, RuleComparer);

    private static IRuleBase Add(IRuleBase ruleBase, IRule rule)
    {
        if (!rule.IsValid())
            throw new InvalidRuleException();
        ruleBase.ProductionRules.Add(rule);
        return ruleBase;
    }

    private static IRuleBase AddAll(IRuleBase ruleBase, ICollection<IRule> rules)
    {
        if (rules.Any(e => !e.IsValid()))
            throw new InvalidRuleException();
        foreach (var rule in rules)
            ruleBase.ProductionRules.Add(rule);
        return ruleBase;
    }

    public static ICollection<IRule> FilterByResolutionMethod(string variableName, IEnumerable<IRule> rules,
        IComparer<IRule> ruleComparer)
    {
        return rules
            .Where(e => string.Equals(e.Consequent!.LinguisticVariable.Name, variableName, InvariantCultureIgnoreCase))
            .GroupBy(e => e.Consequent!.Function.Name)
            .Select(e => new
            {
                FunctionName = e.Key,
                Rule = e.MaxBy(g => g, ruleComparer)!
            })
            .Select(e => e.Rule)
            .ToList();
    }
}