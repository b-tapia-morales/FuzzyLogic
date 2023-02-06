using FuzzyLogic.Number;
using FuzzyLogic.Rule;

namespace FuzzyLogic.Knowledge.Rule;

public static class RuleComparerFactory<T> where T : struct, IFuzzyNumber<T>
{
    // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
    public static IComparer<IRule<T>> CreateInstance(ComparingMethod method) => method switch
    {
        ComparingMethod.HighestPriority => new HighestPriority<T>(),
        ComparingMethod.ShortestPremise => new ShortestPremise<T>(),
        ComparingMethod.LargestPremise => new LargestPremise<T>(),
        _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
    };

    public static IComparer<IRule<T>> CreateInstance(ComparingMethod method, IDictionary<string, double> facts) =>
        method switch
        {
            ComparingMethod.HighestPriority => new HighestPriority<T>(),
            ComparingMethod.ShortestPremise => new ShortestPremise<T>(),
            ComparingMethod.LargestPremise => new LargestPremise<T>(),
            ComparingMethod.MostKnownFacts => new MostKnownFacts<T>(facts),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
}