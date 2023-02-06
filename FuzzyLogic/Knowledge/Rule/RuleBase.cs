using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Rule;
using static System.StringComparison;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Knowledge.Rule;

public class RuleBase<T> : IRuleBase<T> where T : struct, IFuzzyNumber<T>
{
    protected RuleBase(ComparingMethod method = HighestPriority) =>
        RuleComparer = RuleComparerFactory<T>.CreateInstance(method);

    public ICollection<IRule<T>> ProductionRules { get; } = new List<IRule<T>>();
    public IComparer<IRule<T>> RuleComparer { get; }

    public static IRuleBase<T> Create(ComparingMethod method = HighestPriority) => new RuleBase<T>(method);

    public static IRuleBase<T> Initialize(ILinguisticBase linguisticBase, ComparingMethod method = HighestPriority) =>
        Create(method);

    public IRuleBase<T> Add(IRule<T> rule) => Add(this, rule);

    public IRuleBase<T> AddAll(ICollection<IRule<T>> rules) => AddAll(this, rules);

    public IRuleBase<T> AddAll(params IRule<T>[] rules) => AddAll(this, rules);

    public ICollection<IRule<T>> FindApplicableRules(IDictionary<string, double> facts) =>
        ProductionRules.Where(e => e.IsApplicable(facts)).ToList();

    public ICollection<IRule<T>> FindRulesWithPremise(string variableName) =>
        ProductionRules.Where(e => e.PremiseContainsVariable(variableName)).ToList();

    public ICollection<IRule<T>> FindRulesWithConclusion(string variableName) =>
        ProductionRules.Where(e => e.ConclusionContainsVariable(variableName)).ToList();

    public ICollection<IRule<T>> FilterByResolutionMethod(string variableName) =>
        FilterByResolutionMethod(variableName, ProductionRules, RuleComparer);

    private static IRuleBase<T> Add(IRuleBase<T> ruleBase, IRule<T> rule)
    {
        if (!rule.IsValid())
            throw new InvalidRuleException();
        ruleBase.ProductionRules.Add(rule);
        return ruleBase;
    }

    private static IRuleBase<T> AddAll(IRuleBase<T> ruleBase, ICollection<IRule<T>> rules)
    {
        if (rules.Any(e => !e.IsValid()))
            throw new InvalidRuleException();
        foreach (var rule in rules)
            ruleBase.ProductionRules.Add(rule);
        return ruleBase;
    }

    public static ICollection<IRule<T>> FilterByResolutionMethod(string variableName, IEnumerable<IRule<T>> rules,
        IComparer<IRule<T>> ruleComparer)
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