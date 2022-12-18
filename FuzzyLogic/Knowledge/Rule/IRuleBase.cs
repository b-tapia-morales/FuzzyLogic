using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Rule;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Knowledge.Rule;

public interface IRuleBase
{
    ICollection<IRule> ProductionRules { get; }
    IComparer<IRule> RuleComparer { get; }

    static abstract IRuleBase Create(ComparingMethod comparingMethod = Priority);

    static abstract IRuleBase Initialize(ILinguisticBase linguisticBase,
        ComparingMethod comparingMethod = Priority);

    IRuleBase Add(IRule rule);

    IRuleBase AddAll(ICollection<IRule> rules);

    IRuleBase AddAll(params IRule[] rules);

    ICollection<IRule> FindApplicableRules(IDictionary<string, double> facts);

    ICollection<IRule> FindRulesWithPremise(string variableName);

    ICollection<IRule> FindRulesWithConclusion(string variableName);

    ICollection<IRule> FilterByResolutionMethod(string variableName);
}