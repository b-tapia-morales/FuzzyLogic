using System.Collections;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Number;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FuzzyLogic.Tests.RuleBaseTests;

public class RuleBaseTests
{
    private static readonly ServiceProvider ServiceProvider = TipProblemProvider.ConfigureTipProblemProvider();
    private static readonly IRuleBase RuleBaseExample = ServiceProvider.GetService<IRuleBase>() ?? throw new NullReferenceException();
    private static readonly IWorkingMemory WorkingMemory = ServiceProvider.GetService<IWorkingMemory>() ?? throw new NullReferenceException();

    [Theory]
    [InlineData("food quality", 2)]
    [InlineData("service quality", 3)]
    public void RulesUsingPremiseCountIsCorrect(string variableName, int expectedCount)
    {
        Assert.Equal(expectedCount, RuleBaseExample.FindRulesWithPremise(variableName).Count);
        Assert.Empty(RuleBaseExample.FindRulesWithConclusion(variableName));
        Assert.True(RuleBaseExample.FindVariables().Contains(variableName));
    }

    [Fact]
    public void AllRulesAreApplicable()
    {
        const int expectedRuleCount = 3;
        Assert.All(RuleBaseExample.ProductionRules, rule => rule.IsApplicable(WorkingMemory.Facts));
        Assert.Equal(expectedRuleCount, RuleBaseExample.ProductionRules.Count(e => e.IsApplicable(WorkingMemory.Facts)));
        Assert.Equal(expectedRuleCount, RuleBaseExample.FindApplicableRules(WorkingMemory.Facts).Count);
    }

    [Theory]
    [InlineData("food quality")]
    [InlineData("service quality")]
    public void FactsHaveNoDependencies(string variableName)
    {
        Assert.Empty(RuleBaseExample.FindPremiseDependencies(variableName));
        var graph = RuleBaseExample.GetDependencyGraph();
        Assert.Empty(graph[variableName]);
    }

    [Fact]
    public void ConsequentHasDependencies()
    {
        var premiseDependencies = RuleBaseExample.FindPremiseDependencies("tip");
        Assert.NotEmpty(premiseDependencies);
        Assert.True(premiseDependencies.Contains("food quality"));
        Assert.True(premiseDependencies.Contains("service quality"));
        var graph = RuleBaseExample.GetDependencyGraph();
        Assert.NotEmpty(graph["tip"]);
    }

    [Theory]
    [ClassData(typeof(RandomTestData))]
    public void PremiseWeightIsWithinRange(double foodRating, double serviceRating)
    {
        WorkingMemory.UpdateFact("food quality", foodRating);
        WorkingMemory.UpdateFact("service quality", serviceRating);
        var weights = RuleBaseExample.ProductionRules.Select(rule => rule.EvaluatePremiseWeight(WorkingMemory.Facts));
        Assert.All(weights, weight => Assert.True(weight >= FuzzyNumber.Min && weight <= FuzzyNumber.Max));
    }
}

file class RandomTestData : IEnumerable<object[]>
{
    private static readonly IList<int> Ratings = Enumerable.Range(1, 10).ToList();

    private static readonly IEnumerable<object[]> TestData =
        from x in Ratings from y in Ratings select new object[] {(double) x, (double) y};

    public IEnumerator<object[]> GetEnumerator() => TestData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}