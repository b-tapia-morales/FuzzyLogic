﻿using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Rule;
using static System.StringComparison;

namespace FuzzyLogic.Knowledge.Rule;

public class RuleBase : IRuleBase
{
    protected RuleBase() => ProductionRules = new List<IRule>();

    public ICollection<IRule> ProductionRules { get; set; }

    public IRuleBase AddRule(IRule rule) => AddRule(this, rule);

    public IRuleBase AddAll(ICollection<IRule> rules) => AddAllRules(this, rules);

    public ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts) =>
        ProductionRules.Where(e => e.IsApplicable(facts)).ToList();

    public ICollection<IRule> FindRulesWithPremise(string variableName) =>
        ProductionRules.Where(e => e.PremiseContainsVariable(variableName)).ToList();

    public ICollection<IRule> FindRulesWithConclusion(string variableName) =>
        ProductionRules.Where(e => e.ConclusionContainsVariable(variableName)).ToList();

    public IRuleBase FilterDuplicatedConclusions(string variableName) => FilterConclusions(this, variableName);

    public static IRuleBase Create() => new RuleBase();

    public static IRuleBase Initialize(ILinguisticBase linguisticBase) => Create();

    private static IRuleBase AddRule(IRuleBase ruleBase, IRule rule)
    {
        if (!rule.IsValid()) throw new InvalidRuleException();
        ruleBase.ProductionRules.Add(rule);
        return ruleBase;
    }

    private static IRuleBase AddAllRules(IRuleBase ruleBase, ICollection<IRule> rules)
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