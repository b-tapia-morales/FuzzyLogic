using System.Collections;
using FuzzyLogic.Engine;
using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace FuzzyLogic.Tests.InferenceEngineTests;

public class InferenceEngineTests(ITestOutputHelper outputHelper)
{
    private static readonly ServiceProvider ServiceProvider = TipProblemProvider.ConfigureTipProblemProvider();
    private static readonly IEngine InferenceEngineExample = ServiceProvider.GetService<IEngine>() ?? throw new NullReferenceException();

    [Theory]
    [ClassData(typeof(MassiveUnionData))]
    public void DefuzzifiedValueIsInRange(CanonicalType canonicalType, ImplicationMethod implicationMethod, DefuzzificationMethod defuzzificationMethod, double foodRating, double serviceRating)
    {
        var workingMemory = WorkingMemory.Create(("food quality", foodRating), ("service quality", serviceRating));
        var ruleBase = InferenceEngineExample.RuleBase;
        var engine = InferenceEngine
            .Create(ruleBase, workingMemory)
            .UseCanonicalFamily(canonicalType)
            .UseImplicationMethod(implicationMethod)
            .UseDefuzzificationMethod(defuzzificationMethod);
        var value = engine.Defuzzify("tip");
        outputHelper.WriteLine(string.Join(", ", InferenceEngineExample.RuleBase.ProductionRules.Select(rule => rule.EvaluatePremiseWeight(InferenceEngineExample.WorkingMemory.Facts))));
        outputHelper.WriteLine(value.ToString());
        Assert.NotNull(value);
        Assert.InRange(value.Value, 0, 35);
    }
}

file class MassiveUnionData : IEnumerable<object[]>
{
    private static readonly IList<int> Ratings = Enumerable.Range(1, 10).ToList();
    private static readonly IReadOnlyList<CanonicalType> OperatorFamilies = System.Enum.GetValues<CanonicalType>();
    private static readonly IReadOnlyList<ImplicationMethod> ImplicationMethods = System.Enum.GetValues<ImplicationMethod>();
    private static readonly IReadOnlyList<DefuzzificationMethod> DefuzzificationMethods = System.Enum.GetValues<DefuzzificationMethod>();

    private static readonly IEnumerable<object[]> Union =
        from x in OperatorFamilies
        from y in ImplicationMethods
        from z in DefuzzificationMethods
        from a in Ratings
        from b in Ratings
        select new object[] {x, y, z, (double) a, (double) b};

    public IEnumerator<object[]> GetEnumerator() => Union.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}