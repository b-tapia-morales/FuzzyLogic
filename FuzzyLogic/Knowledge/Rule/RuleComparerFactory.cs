using FuzzyLogic.Rule;
using static FuzzyLogic.Rule.ComparingMethod;

namespace FuzzyLogic.Knowledge.Rule;

public static class RuleComparerFactory
{
    public static IComparer<IRule> CreateInstance(ComparingMethod method) => method switch
    {
        Priority => new PriorityComparer(),
        ShortestPremise => new ShortestPremiseComparer(),
        LargestPremise => new LargestPremiseComparer(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };
}