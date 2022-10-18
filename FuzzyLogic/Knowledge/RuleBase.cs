using FuzzyLogic.Rule;
using static System.StringComparison;

namespace FuzzyLogic.Knowledge;

public class RuleBase : IRuleBase
{
    protected RuleBase() => ProductionRules = new List<IRule>();

    public ICollection<IRule> ProductionRules { get; set; }

    public IRuleBase AddRule(IRule rule) => AddRule(this, rule);

    public ICollection<IRule> FindRulesWithPremise(string variableName) =>
        ProductionRules
            .Where(e => e.Antecedent != null &&
                        string.Equals(e.Antecedent.LinguisticVariable.Name, variableName, InvariantCultureIgnoreCase))
            .ToList();

    public ICollection<IRule> FindRulesWithConclusion(string variableName) =>
        ProductionRules
            .Where(e => e.Consequent != null &&
                        string.Equals(e.Consequent.LinguisticVariable.Name, variableName, InvariantCultureIgnoreCase))
            .ToList();

    public IRuleBase FilterDuplicatedConclusions(string variableName) => FilterConclusions(this, variableName);

    public IRuleBase FilterInvalidRules() =>
        FilterRules(this, e => e.Antecedent != null && e.Consequent != null);

    public static IRuleBase Create() => new RuleBase();

    public static IRuleBase Initialize(ILinguisticBase linguisticBase) => Create();

    private static IRuleBase AddRule(IRuleBase ruleBase, IRule rule)
    {
        ruleBase.ProductionRules.Add(rule);
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

    private static IRuleBase FilterRules(IRuleBase ruleBase, Predicate<IRule> rulePredicate)
    {
        ruleBase.ProductionRules = ruleBase.ProductionRules.Where(rulePredicate.Invoke).ToList();
        return ruleBase;
    }
}