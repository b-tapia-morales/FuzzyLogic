using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge;

public interface IRuleBase
{
    ICollection<IRule> ProductionRules { get; set; }

    IRuleBase AddRule(IRule rule);

    ICollection<IRule> FindRulesWithPremise(string variableName);

    ICollection<IRule> FindRulesWithConclusion(string variableName);

    IRuleBase FilterInvalidRules();

    IRuleBase FilterDuplicatedConclusions(string variableName);

    static abstract IRuleBase Create();

    static abstract IRuleBase Initialize(ILinguisticBase linguisticBase);
}