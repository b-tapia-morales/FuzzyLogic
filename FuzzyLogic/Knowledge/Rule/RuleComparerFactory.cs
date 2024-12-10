using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge.Rule;

public static class RuleComparerFactory
{
    public static IComparer<IRule> CreateInstance(ComparingMethod method) => method switch
    {
        ComparingMethod.HighestPriority => new HighestPriority(),
        ComparingMethod.ShortestPremise => new ShortestPremise(),
        ComparingMethod.LargestPremise => new LargestPremise(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };

    public static IComparer<IRule> CreateInstance(ComparingMethod method, IDictionary<string, double> facts) =>
        method switch
        {
            ComparingMethod.HighestPriority => new HighestPriority(),
            ComparingMethod.ShortestPremise => new ShortestPremise(),
            ComparingMethod.LargestPremise => new LargestPremise(),
            ComparingMethod.MostKnownFacts => new MostKnownFacts(facts),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
}