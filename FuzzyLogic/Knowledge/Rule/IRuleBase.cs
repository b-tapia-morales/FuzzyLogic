using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge.Rule;

public interface IRuleBase<T> where T : struct, IFuzzyNumber<T>
{
    ICollection<IRule<T>> ProductionRules { get; }
    IComparer<IRule<T>> RuleComparer { get; }

    static abstract IRuleBase<T> Create(ComparingMethod method = ComparingMethod.HighestPriority);

    static abstract IRuleBase<T> Initialize(ILinguisticBase linguisticBase,
        ComparingMethod method = ComparingMethod.HighestPriority);

    IRuleBase<T> Add(IRule<T> rule);

    IRuleBase<T> AddAll(ICollection<IRule<T>> rules);

    IRuleBase<T> AddAll(params IRule<T>[] rules);

    ICollection<IRule<T>> FindApplicableRules(IDictionary<string, double> facts);

    ICollection<IRule<T>> FindRulesWithPremise(string variableName);

    ICollection<IRule<T>> FindRulesWithConclusion(string variableName);

    ICollection<IRule<T>> FilterByResolutionMethod(string variableName);
}