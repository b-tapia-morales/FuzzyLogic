using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Rule;
using static System.StringComparison;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Knowledge.Rule;

public class RuleBase : IRuleBase
{
    protected RuleBase(ComparingMethod method = Priority)
    {
        ProductionRules = new List<IRule>();
        RuleComparer = RuleComparerFactory.CreateInstance(method);
    }

    public ICollection<IRule> ProductionRules { get; set; }
    public IComparer<IRule> RuleComparer { get; set; }

    public static IRuleBase Create() => new RuleBase();

    public static IRuleBase Initialize(ILinguisticBase linguisticBase) => Create();

    public IRuleBase Add(IRule rule) => Add(this, rule);

    public IRuleBase AddAll(ICollection<IRule> rules) => AddAll(this, rules);

    public IRuleBase AddAll(params IRule[] rules) => AddAll(this, rules);

    public ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts) =>
        ProductionRules.Where(e => e.IsApplicable(facts)).ToList();

    public ICollection<IRule> FindRulesWithPremise(string variableName) =>
        ProductionRules.Where(e => e.PremiseContainsVariable(variableName)).ToList();

    public ICollection<IRule> FindRulesWithConclusion(string variableName) =>
        ProductionRules.Where(e => e.ConclusionContainsVariable(variableName)).ToList();

    public IRuleBase FilterDuplicatedConclusions(string variableName) => FilterConclusions(this, variableName);

    private static IRuleBase Add(IRuleBase ruleBase, IRule rule)
    {
        if (!rule.IsValid()) throw new InvalidRuleException();
        ruleBase.ProductionRules.Add(rule);
        return ruleBase;
    }

    private static IRuleBase AddAll(IRuleBase ruleBase, ICollection<IRule> rules)
    {
        if (rules.Any(e => !e.IsValid())) throw new InvalidRuleException();
        foreach (var rule in rules)
        {
            ruleBase.ProductionRules.Add(rule);
        }

        return ruleBase;
    }

    private static IRuleBase FilterConclusions(IRuleBase ruleBase, string name)
    {
        ruleBase.ProductionRules = ruleBase.ProductionRules
            .Where(e => e.Consequent != null &&
                        string.Equals(e.Consequent.LinguisticVariable.Name, name, InvariantCultureIgnoreCase))
            .ToList();
        return ruleBase;
    }
}