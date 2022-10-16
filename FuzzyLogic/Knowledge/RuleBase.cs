using FuzzyLogic.Rule;
using static System.StringComparison;

namespace FuzzyLogic.Knowledge;

public class RuleBase : IRuleBase
{
    private RuleBase()
    {
        ProductionRules = new List<IRule>();
    }

    public ICollection<IRule> ProductionRules { get; set; }

    public IRuleBase AddRule(IRule rule) => AddRule(this, rule);

    public IRuleBase FilterConclusions(string name) => FilterConclusions(this, name);

    public IRuleBase FilterInvalidRules() =>
        FilterRules(this, e => e.Antecedent != null && e.Consequent != null);

    public static IRuleBase Create() => new RuleBase();

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