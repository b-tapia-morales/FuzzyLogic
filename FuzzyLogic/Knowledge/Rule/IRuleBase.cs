using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge.Rule;

public interface IRuleBase
{
    ICollection<IRule> ProductionRules { get; set; }
    IComparer<IRule> RuleComparer { get; set; }

    IRuleBase Add(IRule rule);

    IRuleBase AddAll(ICollection<IRule> rules);
    
    IRuleBase AddAll(params IRule[] rules);

    ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts);
    
    ICollection<IRule> FindRulesWithPremise(string variableName);

    ICollection<IRule> FindRulesWithConclusion(string variableName);

    IRuleBase FilterDuplicatedConclusions(string variableName);

    static abstract IRuleBase Create();

    static abstract IRuleBase Initialize(ILinguisticBase linguisticBase);
}