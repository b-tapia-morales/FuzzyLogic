using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge;

public interface IRuleBase
{
    ICollection<IRule> ProductionRules { get; set; }

    IRuleBase AddRule(IRule rule);

    IRuleBase FilterConclusions(string name);

    IRuleBase FilterInvalidRules();

    static abstract IRuleBase Create();
}