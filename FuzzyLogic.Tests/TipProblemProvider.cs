using FuzzyLogic.Engine;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Rule;
using FuzzyLogic.Variable;
using Microsoft.Extensions.DependencyInjection;

namespace FuzzyLogic.Tests;

public static class TipProblemProvider
{
    public static ServiceProvider ConfigureTipProblemProvider()
    {
        var serviceCollection = new ServiceCollection();
        var service = LinguisticVariable
            .Create("Food quality")
            .AddTriangularFunction("Bad", 0, 0, 5)
            .AddTriangularFunction("Decent", 0, 5, 10)
            .AddTriangularFunction("Great", 5, 10, 10);
        var food = LinguisticVariable
            .Create("Service quality")
            .AddTriangularFunction("Poor", 0, 0, 5)
            .AddTriangularFunction("Acceptable", 0, 5, 10)
            .AddTriangularFunction("Amazing", 5, 10, 10);
        var tip = LinguisticVariable
            .Create("Tip")
            .AddTriangularFunction("Low", 0, 0, 13)
            .AddTriangularFunction("Medium", 0, 13, 25)
            .AddTriangularFunction("High", 13, 25, 35);
        var linguisticBase = LinguisticBase.Create(service, food, tip);
        serviceCollection.AddTransient<ILinguisticBase>(_ => linguisticBase);
        var r1 = FuzzyRule.Create(linguisticBase)
            .If("food quality", "bad")
            .Or("service quality", "poor")
            .Then("tip", "low");
        var r2 = FuzzyRule.Create(linguisticBase)
            .If("service quality", "acceptable")
            .Then("tip", "medium");
        var r3 = FuzzyRule.Create(linguisticBase)
            .If("food quality", "great")
            .Or("service quality", "amazing")
            .Then("tip", "high");
        var ruleBase = RuleBase.Create(r1, r2, r3);
        serviceCollection.AddTransient<IRuleBase>(_ => ruleBase);
        var workingMemory = WorkingMemory.Create(("food quality", 6), ("service quality", 9.8));
        serviceCollection.AddTransient<IWorkingMemory>(_ => workingMemory);
        var inferenceEngine = InferenceEngine
            .Create(ruleBase, workingMemory);
        serviceCollection.AddTransient<IEngine>(_ => inferenceEngine);
        return serviceCollection.BuildServiceProvider();
    }
}